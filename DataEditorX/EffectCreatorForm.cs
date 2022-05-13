using System.Text;

namespace DataEditorX
{
    public partial class EffectCreatorForm : Form
    {
        public EffectCreatorForm()
        {
            InitializeComponent();
        }

        class EffectCreatorItem
        {
            public string Key;
            public string Value;
            public string Hint;
            public bool IsSelected;

            public EffectCreatorItem(string key, string value)
            {
                Key = key ?? throw new ArgumentNullException(nameof(key));
                Value = value;
            }

            public EffectCreatorItem(string key, string value, string hint) : this(key, value)
            {
                Hint = hint;
            }

            public override string ToString()
            {
                return Value;
            }
        }
        readonly Dictionary<string, List<EffectCreatorItem>> itemDic = new();
        private void EffectCreatorForm_Load(object sender, EventArgs e)
        {
            string config = $"data{Path.DirectorySeparatorChar}effect_creator_settings.txt";
            if (!File.Exists(config))
            {
                return;
            }
            char[] sepChars = new char[] { ' ', '\t', '　' };
            FileStream fs = new(config, FileMode.Open);
            StreamReader sr = new(fs);
            string nowType = "";
            for (string line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                line = line.Trim();
                if (line.StartsWith("!"))
                {
                    nowType = line[1..];
                    if (!itemDic.ContainsKey(nowType))
                    {
                        itemDic.Add(nowType, new List<EffectCreatorItem>());
                    }
                    continue;
                }
                if (line.StartsWith("#"))
                {
                    continue;
                }
                string[] split = line.Split(sepChars, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 2)
                {
                    itemDic[nowType].Add(new EffectCreatorItem(split[0], split[1]));
                }
                if (split.Length == 3)
                {
                    itemDic[nowType].Add(new EffectCreatorItem(split[0], split[1], split[2]));
                }
            }
            sr.Close();
            fs.Close();
            listEffectCode.Items.AddRange(itemDic["EFFECT_CODES"].ToArray());
            listEffectCode.Items.AddRange(itemDic["EVENT_CODES"].ToArray());
            listEffectCategory.Items.AddRange(itemDic["CATEGORY"].ToArray());
            listEffectProperty.Items.AddRange(itemDic["EFFECT_PROPERTY"].ToArray());
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new();
            _ = sb.Append(ProcessDescription());
            _ = sb.Append(ProcessSpecialOptions());
            _ = sb.Append(ProcessEffectType());
            _ = sb.Append(ProcessEffectCategory());
            _ = sb.Append(ProcessEffectCountLimit());
            _ = sb.Append(ProcessEffectProperty());
            _ = sb.Append(ProcessEffectCode());
            txtOutput.Text = sb.ToString();
        }
        private static string LinkStrings(List<string> list)
        {
            if (list.Count == 0)
            {
                return "";
            }
            string result = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                result += $"+{list[i]}";
            }
            return result;
        }
        private string ProcessEffectCountLimit()
        {
            if (!checkCountLimit.Checked || countLimit == null)
            {
                return "";
            }
            List<string> extraOptions = new();
            if (countLimit.IsHasCode)
            {
                extraOptions.Add(countLimit.Code.ToString());
                if (countLimit.Offset > 0)
                {
                    extraOptions.Add(countLimit.Offset.ToString());
                }
            }
            if (countLimit.IsInDuel)
            {
                extraOptions.Add("EFFECT_COUNT_CODE_DUEL");
            }
            if (countLimit.IsOath)
            {
                extraOptions.Add("EFFECT_COUNT_CODE_OATH");
            }
            if (countLimit.IsSingle)
            {
                extraOptions.Add("EFFECT_COUNT_CODE_SINGLE");
            }
            if (extraOptions.Count > 0)
            {
                return $"e{numEffectNum.Value}:SetCountLimit({countLimit.Count},{LinkStrings(extraOptions)})";
            }
            return $"e{numEffectNum.Value}:SetCountLimit({countLimit.Count})";
        }
        private string ProcessEffectProperty()
        {
            var selected = (from EffectCreatorItem item in listEffectProperty.Items.Cast<EffectCreatorItem>()
                            where item.IsSelected
                            select item).ToArray();
            if (selected.Length < 1)
            {
                return "";
            }
            string property = selected[0].Key;
            if (selected.Length > 1)
            {
                for (int i = 1; i < selected.Length; i++)
                {
                    property += $"+{selected[i].Key}";
                }
            }
            return $"e{numEffectNum.Value}:SetCategory({property})";
        }

        private string ProcessEffectCategory()
        {
            var selected = (from EffectCreatorItem item in listEffectCategory.Items.Cast<EffectCreatorItem>()
                            where item.IsSelected
                            select item).ToArray();
            if (selected.Length < 1)
            {
                return "";
            }
            string category = selected[0].Key;
            if (selected.Length > 1)
            {
                for (int i = 1; i < selected.Length; i++)
                {
                    category += $"+{selected[i].Key}";
                }
            }
            return $"e{numEffectNum.Value}:SetCategory({category})";
        }

        private string ProcessEffectCode()
        {
            var selected = (from EffectCreatorItem item in listEffectCode.Items.Cast<EffectCreatorItem>()
                            where item.IsSelected
                            select item).ToArray();
            if (selected.Length < 1)
            {
                return "";
            }
            return $"e{numEffectNum.Value}:SetCode({selected[0].Key})";
        }

        private string ProcessDescription()
        {
            if (numDescription.Value >= 0)
            {
                return $"e{numEffectNum.Value}:SetDescription(aux.Stringid({numCardCode.Value},{numDescription.Value}))";
            }
            return "";
        }

        private string ProcessEffectType()
        {
            string effectType = "";
            foreach (RadioButton radio in gbEffectType.Controls)
            {
                if (radio.Name.StartsWith("radioEffectType"))
                {
                    AddEffectTypeByCheckRadio(radio, ref effectType);
                }
            }
            foreach (RadioButton radio in gbEffectType2.Controls)
            {
                if (radio.Name.StartsWith("radioEffectType"))
                {
                    AddEffectTypeByCheckRadio(radio, ref effectType);
                }
            }
            return $"e{numEffectNum.Value}:SetType({effectType})";
        }

        private static void AddEffectTypeByCheckRadio(RadioButton radio, ref string effectType)
        {
            if (radio.Checked)
            {
                if (effectType != "")
                {
                    effectType += "+";
                }
                effectType += $"EFFECT_TYPE_{radio.Name[15..].ToUpper()}";
            }
        }

        private string ProcessSpecialOptions()
        {
            StringBuilder sb = new();
            if (checkEnableReviveLimit.Checked)
            {
                _ = sb.AppendLine("c:EnableReviveLimit()");
            }
            if (checkRegisterToPlayer.Checked)
            {
                _ = sb.AppendLine($"local e{numEffectNum.Value}=Effect.CreateEffect(c)");
            }
            else
            {
                _ = sb.AppendLine($"local e{numEffectNum.Value}=Effect.GlobalEffect()");
            }
            return sb.ToString();
        }

        private static void SearchListBoxWithTextBox(ref CheckedListBox clb, TextBox tb)
        {
            if (tb.Text == "")
            {
                return;
            }
            var selected = (from EffectCreatorItem item in clb.Items.Cast<EffectCreatorItem>()
                            where item.IsSelected
                            select item).ToArray();
            var searched = (from EffectCreatorItem item in clb.Items.Cast<EffectCreatorItem>()
                            where item.Value.Contains(tb.Text) && !selected.Contains(item)
                            select item).ToArray();
            var notSearched = (from EffectCreatorItem item in clb.Items.Cast<EffectCreatorItem>()
                               where !searched.Contains(item) && !selected.Contains(item)
                               select item).ToArray();
            searched = selected.Concat(searched).Concat(notSearched).ToArray();
            clb.Items.Clear();
            clb.Items.AddRange(searched);
            for (int i = 0; i < clb.Items.Count; i++)
            {
                if ((clb.Items[i] as EffectCreatorItem).IsSelected)
                {
                    clb.SetItemChecked(i, true);
                }
            }
        }

        private void txtSearchEffectCode_TextChanged(object sender, EventArgs e)
        {
            SearchListBoxWithTextBox(ref listEffectCode, txtSearchEffectCode);
        }

        private void listEffectCode_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < ((CheckedListBox)sender).Items.Count; i++)
                {
                    if (((CheckedListBox)sender).GetItemChecked(i) && i != e.Index)
                    {
                        ((CheckedListBox)sender).SetItemChecked(i, false);
                        (((CheckedListBox)sender).Items[i] as EffectCreatorItem).IsSelected = false;
                    }
                }
            }
            (listEffectCode.Items[e.Index] as EffectCreatorItem).IsSelected = e.NewValue == CheckState.Checked;
        }

        private void txtSearchEffectCategory_TextChanged(object sender, EventArgs e)
        {
            SearchListBoxWithTextBox(ref listEffectCategory, txtSearchEffectCategory);
        }

        private void txtSearchProperty_TextChanged(object sender, EventArgs e)
        {
            SearchListBoxWithTextBox(ref listEffectProperty, txtSearchProperty);
        }

        EffectCountLimit countLimit = null;
        private void checkCountLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCountLimit.Checked)
            {
                if (countLimit == null)
                {
                    countLimit = new EffectCountLimit(numCardCode.Value);
                }
                CountLimitForm form = new(countLimit);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    countLimit = form.CountLimit;
                }
            }
        }

        private void checkReset_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCountLimit.Checked)
            {
                if (countLimit == null)
                {
                    countLimit = new EffectCountLimit(numCardCode.Value);
                }
                CountLimitForm form = new(countLimit);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    countLimit = form.CountLimit;
                }
            }
        }

        private void checkHintTiming_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

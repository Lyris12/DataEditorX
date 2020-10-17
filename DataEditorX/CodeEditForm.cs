/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-22
 * 时间: 19:16
 * 
 */
using DataEditorX.Config;
using DataEditorX.Controls;
using DataEditorX.Core;
using DataEditorX.Language;
using FastColoredTextBoxNS;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DataEditorX
{
    /// <summary>
    /// Description of CodeEditForm.
    /// </summary>
    public partial class CodeEditForm : DockContent, IEditForm
    {
        #region Style
        SortedDictionary<long, string> cardlist;
        readonly MarkerStyle sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.White)));
        #endregion

        #region init 函数提示菜单
        //自动完成
        AutocompleteMenu popupMenu;
        string nowFile;
        string title;
        string oldtext;
        SortedList<string, string> tooltipDic;
        AutocompleteItem[] items;
        bool tabisspaces = false;
        string nowcdb;
        public CodeEditForm()
        {
            this.InitForm();
        }

        void InitForm()
        {
            this.cardlist = new SortedDictionary<long, string>();
            this.tooltipDic = new SortedList<string, string>();
            this.InitializeComponent();
            //设置字体，大小
            string fontname = MyConfig.ReadString(MyConfig.TAG_FONT_NAME);
            float fontsize = MyConfig.ReadFloat(MyConfig.TAG_FONT_SIZE, this.fctb.Font.Size);
            this.fctb.Font = new Font(fontname, fontsize);
            if (MyConfig.ReadBoolean(MyConfig.TAG_IME))
            {
                this.fctb.ImeMode = ImeMode.On;
            }

            if (MyConfig.ReadBoolean(MyConfig.TAG_WORDWRAP))
            {
                this.fctb.WordWrap = true;
            }
            else
            {
                this.fctb.WordWrap = false;
            }

            if (MyConfig.ReadBoolean(MyConfig.TAG_TAB2SPACES))
            {
                this.tabisspaces = true;
            }
            else
            {
                this.tabisspaces = false;
            }

            Font ft = new Font(this.fctb.Font.Name, this.fctb.Font.Size / 1.2f, FontStyle.Regular);
            this.popupMenu = new AutocompleteMenu(this.fctb)
            {
                MinFragmentLength = 2
            };
            this.fctb.TextChanged += this.Fctb_TextChanged;
            this.popupMenu.ToolTip.Popup += this.ToolTip_Popup;
            this.popupMenu.Items.Font = ft;
            this.popupMenu.AutoSize = true;
            this.popupMenu.MinimumSize = new Size(300, 0);
            this.popupMenu.BackColor = this.fctb.BackColor;
            this.popupMenu.ForeColor = this.fctb.ForeColor;
            this.popupMenu.Closed += new ToolStripDropDownClosedEventHandler(this.popupMenu_Closed);
            this.popupMenu.SelectedColor = Color.LightGray;
            this.popupMenu.VisibleChanged += this.PopupMenu_VisibleChanged;
            this.popupMenu.Opened += this.PopupMenu_VisibleChanged;
            this.popupMenu.Items.FocussedItemIndexChanged += this.Items_FocussedItemIndexChanged;
            this.title = this.Text;
        }

        private void Fctb_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.PopupMenu_VisibleChanged(null, null);
        }

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            e.Cancel = true;
        }

        private void PopupMenu_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.popupMenu.Visible)
            {
                this.AdjustPopupMenuSize();
                return;
            }
            if (this.popupMenu.Items.FocussedItem == null)
            {
                if (this.popupMenu.Items.Count == 0)
                {
                    return;
                }
                this.popupMenu.Items.FocussedItemIndex = 0;
            }
            this.fctb.ShowTooltipWithLabel(this.popupMenu.Items.FocussedItem.ToolTipTitle,
                this.popupMenu.Items.FocussedItem.ToolTipText);
            this.AdjustPopupMenuSize();
        }
        private void AdjustPopupMenuSize()
        {
            if (!this.popupMenu.Visible || this.popupMenu.Items.FocussedItem == null)
            {
                this.popupMenu.Size = new Size(300, 0);
                this.popupMenu.MinimumSize = new Size(300, 0);
                return;
            }
            Size s = TextRenderer.MeasureText(this.popupMenu.Items.FocussedItem.ToolTipTitle,
                this.popupMenu.Items.Font, new Size(0, 0), TextFormatFlags.NoPadding);
            s = new Size(s.Width + 50, this.popupMenu.Size.Height);
            if (this.popupMenu.Size.Width < s.Width)
            {
                this.popupMenu.Size = s;
                this.popupMenu.MinimumSize = s;
            }
        }
        private void Items_FocussedItemIndexChanged(object sender, EventArgs e)
        {
            if (this.popupMenu.Items.FocussedItem == null)
            {
                return;
            }
            this.AdjustPopupMenuSize();
            this.fctb.ShowTooltipWithLabel(this.popupMenu.Items.FocussedItem.ToolTipTitle,
                this.popupMenu.Items.FocussedItem.ToolTipText);
        }

        void popupMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.popupMenu.Items.SetAutocompleteItems(this.items);
        }
        #endregion

        #region IEditForm接口
        public void SetActived()
        {
            this.Activate();
        }
        public bool CanOpen(string file)
        {
            return YGOUtil.IsScript(file);
        }
        public string GetOpenFile()
        {
            return this.nowFile;
        }
        public bool Create(string file)
        {
            return this.Open(file);
        }
        public bool Save()
        {
            return this.savefile(string.IsNullOrEmpty(this.nowFile));
        }
        public bool Open(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    FileStream fs = new FileStream(file, FileMode.Create);
                    fs.Close();
                }
                this.nowFile = file;
                FileInfo fi = new FileInfo(file);
                if (fi.Name.ToUpper().EndsWith(".LUA"))
                {
                    (this.fctb.SyntaxHighlighter as MySyntaxHighlighter).cCode
                        = fi.Name.Substring(0, fi.Name.Length - 4);
                }
                string cdb = MyPath.Combine(
                    Path.GetDirectoryName(file), "../cards.cdb");
                this.SetCardDB(cdb);//后台加载卡片数据
                this.fctb.OpenFile(this.nowFile, new UTF8Encoding(false));
                this.oldtext = this.fctb.Text;
                this.SetTitle();
                return true;
            }
            return false;
        }

        #endregion

        #region 文档视图
        //文档视图
        void ShowMapToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this.menuitem_showmap.Checked)
            {
                this.documentMap1.Visible = false;
                this.menuitem_showmap.Checked = false;
                this.fctb.Width += this.documentMap1.Width;
            }
            else
            {
                this.documentMap1.Visible = true;
                this.menuitem_showmap.Checked = true;
                this.fctb.Width -= this.documentMap1.Width;
            }
        }
        #endregion

        #region 设置标题
        void SetTitle()
        {
            string str;
            if (string.IsNullOrEmpty(this.nowFile))
            {
                str = this.title;
            }
            else
            {
                str = new FileInfo(this.nowFile).Name;
            }

            if (this.MdiParent != null)//如果父容器不为空
            {
                if (string.IsNullOrEmpty(this.nowFile))
                {
                    this.Text = this.title;
                    this.TabText = this.title;
                }
                else
                {
                    this.Text = Path.GetFileName(this.nowFile);
                }
                this.MdiParent.Text = str;
            }
            else
            {
                this.Text = str;
                this.TabText = str;
            }
        }

        void CodeEditFormEnter(object sender, EventArgs e)
        {
            this.SetTitle();
        }
        #endregion

        #region 自动完成
        public void LoadXml(string xmlfile)
        {
            this.fctb.DescriptionFile = xmlfile;
        }
        public void InitTooltip(CodeConfig codeconfig)
        {
            this.tooltipDic = codeconfig.TooltipDic;
            this.items = codeconfig.Items;
            this.popupMenu.Items.SetAutocompleteItems(this.items);
        }
        #endregion

        #region 悬停的函数说明
        //查找函数说明
        string FindTooltip(string word)
        {
            string desc = "";
            foreach (string v in this.tooltipDic.Keys)
            {
                int t = v.IndexOf(".");
                string k = v;
                if (t > 0)
                {
                    k = v.Substring(t + 1);
                }

                if (word == k)
                {
                    desc = this.tooltipDic[v];
                }
            }
            return desc;
        }

        //悬停的函数说明
        void FctbToolTipNeeded(object sender, ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                long tl = 0;
                string name = e.HoveredWord;
                string desc = "";
                if (!name.StartsWith("0x") && name.Length <= 9)
                {
                    name = name.Replace("c", "");
                    long.TryParse(name, out tl);
                }

                if (tl > 0)
                {
                    //获取卡片信息
                    if (this.cardlist.ContainsKey(tl))
                    {
                        desc = this.cardlist[tl];
                    }
                }
                else
                {
                    desc = this.FindTooltip(e.HoveredWord);
                }

                if (!string.IsNullOrEmpty(desc))
                {
                    e.ToolTipTitle = e.HoveredWord;
                    e.ToolTipText = desc;
                }
            }
        }
        #endregion

        #region 保存文件
        bool savefile(bool saveas)
        {
            string alltext = this.fctb.Text;
            if (!this.tabisspaces)
            {
                alltext = alltext.Replace("    ", "\t");
            }

            if (saveas)
            {
                using (SaveFileDialog sfdlg = new SaveFileDialog())
                {
                    try
                    {
                        sfdlg.Filter = LanguageHelper.GetMsg(LMSG.ScriptFilter);
                    }
                    catch { }
                    if (sfdlg.ShowDialog() == DialogResult.OK)
                    {
                        this.nowFile = sfdlg.FileName;
                        this.SetTitle();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            this.oldtext = this.fctb.Text;
            File.WriteAllText(this.nowFile, alltext, new UTF8Encoding(false));
            return true;
        }

        public bool SaveAs()
        {
            return this.savefile(true);
        }

        void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Save();
        }
        void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.SaveAs();
        }
        #endregion

        #region 菜单
        //显示/隐藏输入框
        void Menuitem_showinputClick(object sender, EventArgs e)
        {
            if (this.menuitem_showinput.Checked)
            {
                this.menuitem_showinput.Checked = false;
                this.tb_input.Visible = false;
            }
            else
            {
                this.menuitem_showinput.Checked = true;
                this.tb_input.Visible = true;
            }
        }
        //如果是作为mdi，则隐藏菜单
        void HideMenu()
        {
            if (this.MdiParent == null)
            {
                return;
            }

            this.mainMenu.Visible = false;
            this.menuitem_file.Visible = false;
            this.menuitem_file.Enabled = false;
        }

        void CodeEditFormLoad(object sender, EventArgs e)
        {
            this.HideMenu();
            this.fctb.OnTextChangedDelayed(this.fctb.Range);
        }
        void Menuitem_findClick(object sender, EventArgs e)
        {
            this.fctb.ShowFindDialog();
        }

        void Menuitem_replaceClick(object sender, EventArgs e)
        {
            this.fctb.ShowReplaceDialog();
        }

        void QuitToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Close();
        }

        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            MyMsg.Show(
                LanguageHelper.GetMsg(LMSG.About) + "\t" + Application.ProductName + "\n"
                + LanguageHelper.GetMsg(LMSG.Version) + "\t1.1.0.0\n"
                + LanguageHelper.GetMsg(LMSG.Author) + "\tNanahira & JoyJ");
        }

        void Menuitem_openClick(object sender, EventArgs e)
        {
            using (OpenFileDialog sfdlg = new OpenFileDialog())
            {
                try
                {
                    sfdlg.Filter = LanguageHelper.GetMsg(LMSG.ScriptFilter);
                }
                catch { }
                if (sfdlg.ShowDialog() == DialogResult.OK)
                {
                    this.nowFile = sfdlg.FileName;
                    this.fctb.OpenFile(this.nowFile, new UTF8Encoding(false));
                }
            }
        }

        #endregion

        #region 搜索函数
        //搜索函数
        void Tb_inputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string key = this.tb_input.Text;
                List<AutocompleteItem> list =new List<AutocompleteItem>();
                foreach (AutocompleteItem item in this.items)
                {
                    if (item.ToolTipText.Contains(key))
                    {
                        list.Add(item);
                    }
                }
                this.popupMenu.Items.SetAutocompleteItems(list.ToArray());
                this.popupMenu.Show(true);
            }
        }
        #endregion

        #region 提示保存
        void CodeEditFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.oldtext))
            {
                if (this.fctb.Text != this.oldtext)
                {
                    if (MyMsg.Question(LMSG.IfSaveScript))
                    {
                        this.Save();
                    }
                }
            }
            else if (this.fctb.Text.Length > 0)
            {
                if (MyMsg.Question(LMSG.IfSaveScript))
                {
                    this.Save();
                }
            }
        }
        #endregion

        #region 卡片提示
        public void SetCDBList(string[] cdbs)
        {
            if (cdbs == null)
            {
                return;
            }

            foreach (string cdb in cdbs)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(cdb);
                tsmi.Click += this.MenuItem_Click;
                this.menuitem_setcard.DropDownItems.Add(tsmi);
            }
        }
        void MenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem tsmi)
            {
                string file = tsmi.Text;
                this.SetCardDB(file);
            }
        }
        public void SetCardDB(string name)
        {
            this.nowcdb = name;
            if (!this.backgroundWorker1.IsBusy)
            {
                this.backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (this.nowcdb != null && File.Exists(this.nowcdb))
            {
                this.SetCards(DataBase.Read(this.nowcdb, true, ""));
            }
        }
        public void SetCards(Card[] cards)
        {
            if (cards == null)
            {
                return;
            }

            this.cardlist.Clear();
            foreach (Card c in cards)
            {
                this.cardlist.Add(c.id, c.ToString());
            }
        }
        #endregion

        #region 选择高亮
        void FctbSelectionChangedDelayed(object sender, EventArgs e)
        {
            this.tb_input.Text = this.fctb.SelectedText;
            this.fctb.VisibleRange.ClearStyle(this.sameWordsStyle);
            if (!this.fctb.Selection.IsEmpty)
            {
                return;//user selected diapason
            }

            //get fragment around caret
            var fragment = this.fctb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
            {
                return;
            }
            //highlight same words
            var ranges = this.fctb.Range.GetRanges("\\b" + text + "\\b");
            foreach (var r in ranges)
            {
                r.SetStyle(this.sameWordsStyle);
            }
        }
        #endregion

        #region 调转函数
        void FctbMouseClick(object sender, MouseEventArgs e)
        {
            var fragment = this.fctb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
            {
                return;
            }

            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.Control)
            {
                List<int> linenums = this.fctb.FindLines(@"function\s+?\S+?\." + text + @"\(", RegexOptions.Singleline);
                if (linenums.Count > 0)
                {
                    this.fctb.Navigate(linenums[0]);
                    //MessageBox.Show(linenums[0].ToString());
                }
            }
        }
        #endregion

        private void menuitem_testlua_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(this.nowFile);
            string fn = fi.Name;
            if (!fn.ToUpper().EndsWith(".LUA"))
            {
                return;
            }
            string cCode = fn.Substring(0,fn.Length - 4);
            bool error=false;
            try
            {
                Directory.SetCurrentDirectory(fi.DirectoryName);
                Lua lua = new Lua();
                var env = lua.CreateEnvironment();
                string pre = "Duel={} Effect={} Card={} aux={} Auxiliary={} " + cCode + "={} Duel.LoadScript=function(str) end ";
                env.DoChunk(pre + this.fctb.Text, "test.lua");
            }
            catch (LuaException ex)
            {
                MessageBox.Show($"LINE{ex.Line} - {ex.Message}");
                error = true;
            }
            if (!error)
            {
                MyMsg.Show(LMSG.syntaxCheckPassed);
            }
        }

        private void effectCreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EffectCreatorForm form = new EffectCreatorForm();
            form.Show();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void OnDragDtop(object sender, DragEventArgs e)
        {
            string[] drops = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (drops == null)
            {
                return;
            }
            List<string> files = new List<string>();
            foreach (string file in drops)
            {
                if (Directory.Exists(file))
                {
                    files.AddRange(Directory.EnumerateFiles(file, "*.cdb", SearchOption.AllDirectories));
                    files.AddRange(Directory.EnumerateFiles(file, "*.lua", SearchOption.AllDirectories));
                }
                files.Add(file);
            }
            if (files.Count > 5)
            {
                if (!MyMsg.Question(LMSG.IfOpenLotsOfFile))
                {
                    return;
                }
            }
            foreach (string file in files)
            {
                (this.DockPanel.Parent as MainForm).Open(file);
            }
        }
    }
}

/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 20:22
 * 
 */
using DataEditorX.Common;
using DataEditorX.Config;
using DataEditorX.Core;
using DataEditorX.Core.Mse;
using DataEditorX.Language;
using System.Globalization;
using WeifenLuo.WinFormsUI.Docking;
using Microsoft.Data.Sqlite;
using System.Text.RegularExpressions;
using System.Text;

namespace DataEditorX
{
    public partial class DataEditForm : DockContent, IDataForm
    {
        private string addrequire_str;

        public string Addrequire
        {
            get
            {
                if (!string.IsNullOrEmpty(addrequire_str))
                {
                    return addrequire_str;
                }
                else
                {
                    string cdbName = Path.GetFileNameWithoutExtension(nowCdbFile);
                    if (cdbName.Length > 0 && File.Exists(GetPath().GetModuleScript(cdbName)))
                    {
                        return cdbName;
                    }
                }
                return "";
            }
            set
            {
                addrequire_str = value;
            }
        }

        #region 成员变量/构造
        TaskHelper tasker = null;
        string taskname;
        //目录
        YgoPath ygopath;
        /// <summary>当前卡片</summary>
        Card oldCard = new(0);
        /// <summary>搜索条件</summary>
        Card srcCard = new(0);
        //卡片编辑
        CardEdit cardedit;
        string[] strs = null;
        /// <summary>
        /// 对比的id集合
        /// </summary>
        List<string> tmpCodes;
        //初始标题
        string title;
        string nowCdbFile = "";
        int maxRow = 37;
        int page = 1, pageNum = 1;
        /// <summary>
        /// 卡片总数
        /// </summary>
        int cardcount;

        /// <summary>
        /// 搜索结果
        /// </summary>
        readonly List<Card> cardlist = new();

        //setcode正在输入
        readonly bool[] setcodeIsedit = new bool[5];
        readonly CommandManager cmdManager = new();

        Image cover;
        MSEConfig msecfg;

        string datapath, confcover;

        public DataEditForm(string datapath, string cdbfile, DataConfig datacfg = null)
        {
            Initialize(datapath);
            if (datacfg != null && File.Exists(cdbfile) && !cdbfile.EndsWith(".cdb", StringComparison.OrdinalIgnoreCase))
            {
                Dictionary<long, string> d = datacfg.dicSetnames;
                if (!d.ContainsKey(0)) d.Add(0L, "Archetype");
                using SqliteConnection con = new(@"Data Source=" + cdbfile);
                con.Open();
                using SqliteCommand cmd = con.CreateCommand();
                cmd.CommandText = "select officialcode,betacode,name from setcodes;";
                using SqliteDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        int c = reader.GetInt32(reader.GetOrdinal("officialcode"));
                        if (c == 0)
                            c = reader.GetInt32(reader.GetOrdinal("betacode"));
                        string n = reader.GetString(reader.GetOrdinal("name"));
                        if (c > 0 && d.ContainsKey(c))
                            d[c] = n;
                        else d.Add(c, n);
                    }
                }
                catch { }
                con.Close();
                SqliteConnection.ClearAllPools();
            }
            nowCdbFile = cdbfile;
        }

        public DataEditForm(string datapath)
        {
            Initialize(datapath);
        }
        public DataEditForm()
        {//默认启动
            string dir = DEXConfig.ReadString(DEXConfig.TAG_DATA);
            if (string.IsNullOrEmpty(dir))
            {
                Application.Exit();
            }
            datapath = MyPath.Combine(Application.StartupPath, dir);

            Initialize(datapath);
        }
        void Initialize(string datapath)
        {
            cardedit = new CardEdit(this);
            tmpCodes = new List<string>();
            ygopath = new YgoPath(Application.StartupPath);
            InitPath(datapath);
            InitializeComponent();
            title = Text;
            nowCdbFile = "";
            cmdManager.UndoStateChanged += delegate (bool val)
            {
                if (val)
                {
                    btn_undo.Enabled = true;
                }
                else
                {
                    btn_undo.Enabled = false;
                }
            };
        }

        #endregion

        #region 接口
        public void SetActived()
        {
            Activate();
        }
        public string GetOpenFile()
        {
            return nowCdbFile;
        }
        public bool CanOpen(string file)
        {
            return YGOUtil.IsDataBase(file);
        }
        public bool Create(string file)
        {
            return Open(file);
        }
        public bool Save(bool shift)
        {
            if (shift)
                using (SaveFileDialog dlg = new())
                {
                    dlg.Title = LanguageHelper.GetMsg(LMSG.NewFile);
                    try
                    {
                        dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
                    }
                    catch { }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        string file = dlg.FileName;
                        if (DataBase.Create(file))
                        {
                            Card[] cl = GetCardList(false);
                            SetCDB(file);
                            SaveCards(cl);
                        }
                        else
                        {
                            File.Delete(file);
                            File.Delete(file + "-journal");
                            return false;
                        }
                    }
                    else return false;
                }
            return true;
        }
        #endregion

        #region 窗体
        //窗体第一次加载
        void DataEditFormLoad(object sender, EventArgs e)
        {
            //InitListRows();//调整卡片列表的函数
            HideMenu();//是否需要隐藏菜单
            SetTitle();//设置标题
                            //加载
            msecfg = new MSEConfig(datapath);
            tasker = new TaskHelper(datapath, bgWorker1, msecfg);
            //设置空白卡片
            oldCard = new Card(0);
            SetCard(oldCard);
            //删除资源
            menuitem_operacardsfile.Checked = DEXConfig.ReadBoolean(DEXConfig.TAG_DELETE_WITH);
            //用CodeEditor打开脚本
            menuitem_openfileinthis.Checked = DEXConfig.ReadBoolean(DEXConfig.TAG_OPEN_IN_THIS);
            //自动检查更新
            menuitem_autocheckupdate.Checked = DEXConfig.ReadBoolean(DEXConfig.TAG_AUTO_CHECK_UPDATE);
            //add require automatically
            Addrequire = DEXConfig.ReadString(DEXConfig.TAG_ADD_REQUIRE);
            menuitem_addrequire.Checked = (Addrequire.Length > 0);
            if (nowCdbFile != null && File.Exists(nowCdbFile))
            {
                _ = Open(nowCdbFile);
            }
            //获取MSE配菜单
            AddMenuItemFormMSE();
            //
            GetLanguageItem();
            //   CheckUpdate(false);//检查更新
        }
        //窗体关闭
        void DataEditFormFormClosing(object sender, FormClosingEventArgs e)
        {
            //当前有任务执行，是否结束
            if (tasker != null && tasker.IsRuning())
            {
                if (!CancelTask())
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (nowCdbFile.EndsWith(".db", StringComparison.OrdinalIgnoreCase)
                || nowCdbFile.EndsWith(".bytes", StringComparison.OrdinalIgnoreCase) && DockPanel.Parent is MainForm)
            {
                MainForm mf = (MainForm)DockPanel.Parent;
                Dictionary<long, string> d = mf.GetDataConfig().dicSetnames;
                Dictionary<long, string> o = mf.GetDataConfig(true).dicSetnames;
                foreach (long setcode in d.Keys)
                {
                    if (o.ContainsKey(setcode)) continue;
                    _ = DataBase.Command(nowCdbFile, new string[] { $"insert or ignore into setcodes values({setcode},{setcode},'{d[setcode].Replace("'", "''")}',0);" });
                }
            }
        }
        //窗体激活
        void DataEditFormEnter(object sender, EventArgs e)
        {
            SetTitle();
        }
        #endregion

        #region 初始化设置
        //隐藏菜单
        void HideMenu()
        {
            if (MdiParent == null)
            {
                return;
            }

            mainMenu.Visible = false;
            //this.SuspendLayout();
            ResumeLayout(true);
            foreach (Control c in Controls)
            {
                if (c.GetType() == typeof(MenuStrip))
                {
                    continue;
                }

                Point p = c.Location;
                c.Location = new Point(p.X, p.Y - 25);
            }
            ResumeLayout(false);
            //this.PerformLayout();
        }

        //移除Tag
        static string RemoveTag(string text)
        {
            int t = text.LastIndexOf(" (");
            if (t > 0)
            {
                return text[..t];
            }
            return text;
        }
        //设置标题
        void SetTitle()
        {
            string str = title;
            string str2 = RemoveTag(title);
            if (!string.IsNullOrEmpty(nowCdbFile))
            {
                str = nowCdbFile + "-" + str;
                str2 = Path.GetFileName(nowCdbFile);
            }
            if (MdiParent != null) //父容器不为空
            {
                Text = str2;
                if (tasker != null && tasker.IsRuning())
                {
                    if (DockPanel.ActiveContent == this)
                    {
                        MdiParent.Text = str;
                    }
                }
                else
                {
                    MdiParent.Text = str;
                }
            }
            else
            {
                Text = str;
            }
        }
        //按cdb路径设置目录
        void SetCDB(string cdb)
        {
            nowCdbFile = cdb;
            SetTitle();
            string path = Application.StartupPath;
            if (cdb.Length > 0)
            {
                path = Path.GetDirectoryName(cdb);
            }
            ygopath.SetPath(path);
        }
        //初始化文件路径
        void InitPath(string datapath)
        {
            this.datapath = datapath;
            confcover = MyPath.Combine(datapath, "cover.jpg");
            if (File.Exists(confcover))
            {
                cover = MyBitmap.ReadImage(confcover);
            }
            else
            {
                cover = null;
            }
        }
        #endregion

        #region 界面控件
        //初始化控件
        public void InitControl(DataConfig datacfg)
        {
            if (datacfg == null)
            {
                return;
            }

            List<long> setcodes = DataManager.GetKeys(datacfg.dicSetnames);
            string[] setnames = DataManager.GetValues(datacfg.dicSetnames);
            try
            {
                InitComboBox(cb_cardrace, datacfg.dicCardRaces);
                InitComboBox(cb_cardattribute, datacfg.dicCardAttributes);
                InitComboBox(cb_cardrule, datacfg.dicCardRules);
                InitComboBox(cb_cardlevel, datacfg.dicCardLevels);
                InitCheckPanel(pl_cardtype, datacfg.dicCardTypes);
                InitCheckPanel(pl_markers, datacfg.dicLinkMarkers);
                InitCheckPanel(pl_category, datacfg.dicCardcategorys);
                InitCheckPanel(pl_flags, datacfg.dicCardflags);
                InitComboBox(cb_setname1, setcodes, setnames);
                InitComboBox(cb_setname2, setcodes, setnames);
                InitComboBox(cb_setname3, setcodes, setnames);
                InitComboBox(cb_setname4, setcodes, setnames);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.ToString(), "启动错误");
            }
        }

        //初始化FlowLayoutPanel
        static void InitCheckPanel(FlowLayoutPanel fpanel, Dictionary<long, string> dic)
        {
            fpanel.SuspendLayout();
            fpanel.Controls.Clear();
            foreach (long key in dic.Keys)
            {
                string value = dic[key];
                if (value != null && value.StartsWith("NULL"))
                {
                    Label lab = new();
                    string[] sizes = value.Split(',');
                    if (sizes.Length >= 3)
                    {
                        lab.Size = new Size(int.Parse(sizes[1]), int.Parse(sizes[2]));
                    }
                    lab.AutoSize = false;
                    lab.Margin = fpanel.Margin;
                    fpanel.Controls.Add(lab);
                }
                else
                {
                    CheckBox _cbox = new()
                    {
                        //_cbox.Name = fpanel.Name + key.ToString("x");
                        Tag = key,//绑定值
                        Text = value,
                        AutoSize = true,
                        Margin = fpanel.Margin
                    };
                    //_cbox.Click += PanelOnCheckClick;
                    fpanel.Controls.Add(_cbox);
                }
            }
            fpanel.ResumeLayout(false);
            fpanel.PerformLayout();
        }

        //初始化ComboBox
        void InitComboBox(ComboBox cb, Dictionary<long, string> tempdic)
        {
            InitComboBox(cb, DataManager.GetKeys(tempdic),
                         DataManager.GetValues(tempdic));
        }
        //初始化ComboBox
        void InitComboBox(ComboBox cb, List<long> keys, string[] values)
        {
            cb.Items.Clear();
            cb.Tag = keys;
            cb.Items.AddRange(values);
            if (cb.Items.Count > 1 && (cb == cb_setname1
                || cb == cb_setname2 || cb == cb_setname3
                || cb == cb_setname4))
                cb.SelectedIndex = 1;
            else if (cb.Items.Count > 0)
                cb.SelectedIndex = 0;
        }
        //计算list最大行数
        void InitListRows()
        {
            bool addTest = lv_cardlist.Items.Count == 0;
            if (addTest)
            {
                ListViewItem item = new()
                {
                    Text = "Test"
                };
                _ = lv_cardlist.Items.Add(item);
            }
            int headH = lv_cardlist.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Y;
            int itemH = lv_cardlist.Items[0].GetBounds(ItemBoundsPortion.ItemOnly).Height;
            if (itemH > 0)
            {
                int n = (lv_cardlist.Height - headH) / itemH;
                if (n > 0)
                {
                    maxRow = n;
                }
                //MessageBox.Show("height="+lv_cardlist.Height+",item="+itemH+",head="+headH+",max="+MaxRow);
            }
            if (addTest)
            {
                lv_cardlist.Items.Clear();
            }
            if (maxRow < 10)
            {
                maxRow = 20;
            }
        }

        //设置checkbox
        static void SetCheck(FlowLayoutPanel fpl, long number)
        {
            long temp;
            //string strType = "";
            foreach (Control c in fpl.Controls)
            {
                if (c is CheckBox cbox)
                {
                    if (cbox.Tag == null)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = (long)cbox.Tag;
                    }

                    if ((temp & number) == temp && temp != 0)
                    {
                        cbox.Checked = true;
                        //strType += "/" + c.Text;
                    }
                    else
                    {
                        cbox.Checked = false;
                    }
                }
            }
            //return strType;
        }

        static void SetEnabled(FlowLayoutPanel fpl, bool set)
        {
            foreach (Control c in fpl.Controls)
            {
                if (c is CheckBox cbox)
                {
                    cbox.Enabled = set;
                }
            }
        }

        //设置combobox
        static void SetSelect(ComboBox cb, long k)
        {
            if (cb.Tag == null)
            {
                cb.SelectedIndex = 0;
                return;
            }
            List<long> keys = (List<long>)cb.Tag;
            int index = keys.IndexOf(k);
            if (index >= 0 && index < cb.Items.Count)
            {
                cb.SelectedIndex = index;
            }
        }

        //得到所选值
        static long GetSelect(ComboBox cb)
        {
            if (cb.Tag == null)
            {
                return 0;
            }
            List<long> keys = (List<long>)cb.Tag;
            int index = cb.SelectedIndex;
            if (index >= keys.Count || index < 0)
            {
                return 0;
            }
            else
            {
                return keys[index];
            }
        }

        //得到checkbox的总值
        static long GetCheck(FlowLayoutPanel fpl)
        {
            long number = 0;
            long temp;
            foreach (Control c in fpl.Controls)
            {
                if (c is CheckBox cbox)
                {
                    if (cbox.Tag == null)
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = (long)cbox.Tag;
                    }

                    if (cbox.Checked)
                    {
                        number += temp;
                    }
                }
            }
            return number;
        }
        //添加列表行
        void AddListView(int p)
        {
            int i, j, istart, iend;

            if (p <= 0)
            {
                p = 1;
            }
            else if (p >= pageNum)
            {
                p = pageNum;
            }

            istart = (p - 1) * maxRow;
            iend = p * maxRow;
            if (iend > cardcount)
            {
                iend = cardcount;
            }

            page = p;
            lv_cardlist.BeginUpdate();
            lv_cardlist.Items.Clear();
            if ((iend - istart) > 0)
            {
                ListViewItem[] items = new ListViewItem[iend - istart];
                Card mcard;
                for (i = istart, j = 0; i < iend; i++, j++)
                {
                    mcard = cardlist[i];
                    items[j] = new ListViewItem
                    {
                        Tag = i,
                        Text = mcard.id.ToString()
                    };
                    if (mcard.id == oldCard.id)
                    {
                        items[j].Checked = true;
                    }

                    if (i % 2 == 0)
                    {
                        items[j].BackColor = Color.GhostWhite;
                    }
                    else
                    {
                        items[j].BackColor = Color.White;
                    }

                    _ = items[j].SubItems.Add(mcard.name);
                }
                lv_cardlist.Items.AddRange(items);
            }
            lv_cardlist.EndUpdate();
            tb_page.Text = page.ToString();

        }
        #endregion

        #region 设置卡片
        public YgoPath GetPath()
        {
            return ygopath;
        }
        public Card GetOldCard()
        {
            return oldCard;
        }

        private void SetLinkMarks(long mark, bool setCheck = false)
        {
            if (setCheck)
            {
                SetCheck(pl_markers, mark);
            }
            tb_link.Text = Convert.ToString(mark, 2).PadLeft(9, '0');
        }

        public void SetCard(Card c)
        {
            oldCard = c;

            tb_cardname.Text = c.name;
            tb_cardtext.Text = c.desc;

            strs = new string[c.Str.Length];
            Array.Copy(c.Str, strs, Card.STR_MAX);
            lb_scripttext.Items.Clear();
            lb_scripttext.Items.AddRange(c.Str);
            tb_edittext.Text = "";
            //data
            SetSelect(cb_cardrule, c.ot);
            SetSelect(cb_cardattribute, c.attribute);
            SetSelect(cb_cardlevel, (c.level & 0xff));
            SetSelect(cb_cardrace, c.race);
            //setcode
            long[] setcodes = c.GetSetCode();
            tb_setcode1.Text = setcodes[0].ToString("x");
            tb_setcode2.Text = setcodes[1].ToString("x");
            tb_setcode3.Text = setcodes[2].ToString("x");
            tb_setcode4.Text = setcodes[3].ToString("x");
            //type,category
            SetCheck(pl_cardtype, c.type);
            if (c.IsType(Core.Info.CardType.TYPE_LINK))
            {
                SetLinkMarks(c.def, true);
            }
            else
            {
                tb_link.Text = "";
                SetCheck(pl_markers, 0);
            }
            SetCheck(pl_category, c.category);
            //Omega-exclusive
            if (GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase) || GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase))
            {
                SetCheck(pl_flags, (long)c.omega[1]);
                tb_support.Text = c.omega[2].ToString("x");
                tb_odate.Text = c.GetDate();
                tb_tdate.Text = c.GetDate(1);
            }
            else
            {
                SetCheck(pl_flags, 0);
                tb_support.Text = "0";
                tb_odate.Text = "9999-12-30 22:00:00";
                tb_tdate.Text = "9999-12-30 22:00:00";
            }
            //Pendulum
            tb_pleft.Text = ((c.level >> 24) & 0xff).ToString();
            tb_pright.Text = ((c.level >> 16) & 0xff).ToString();
            //atk，def
            tb_atk.Text = (c.atk < 0) ? "?" : c.atk.ToString();
            if (c.IsType(Core.Info.CardType.TYPE_LINK))
            {
                tb_def.Text = "0";
            }
            else
            {
                tb_def.Text = (c.def < 0) ? "?" : c.def.ToString();
            }

            tb_cardcode.Text = c.id.ToString();
            tb_cardalias.Text = c.alias.ToString();
            SetImage(c.id.ToString());
        }
        #endregion

        #region 获取卡片
        public Card GetCard()
        {
            Card c = new(0)
            {
                name = tb_cardname.Text,
                desc = tb_cardtext.Text
            };

            Array.Copy(strs, c.Str, Card.STR_MAX);

            c.ot = (int)GetSelect(cb_cardrule);
            c.attribute = (int)GetSelect(cb_cardattribute);
            c.level = (int)GetSelect(cb_cardlevel);
            c.race = (int)GetSelect(cb_cardrace);
            //系列
            c.SetSetCode(
                tb_setcode1.Text,
                tb_setcode2.Text,
                tb_setcode3.Text,
                tb_setcode4.Text);

            c.type = GetCheck(pl_cardtype);
            c.category = GetCheck(pl_category);

            c.omega = new long[5];
            if (GetOpenFile().EndsWith(".db", StringComparison.OrdinalIgnoreCase) || GetOpenFile().EndsWith(".bytes", StringComparison.OrdinalIgnoreCase)) {
                c.script = oldCard.script;
                c.omega[0] = 1L;
                c.omega[1] = GetCheck(pl_flags);
                c.SetSupport(tb_support.Text);
                c.SetDate(tb_odate.Text);
                c.SetDate(tb_tdate.Text, 1);
            } else for (byte i = 0; i < 5; i++) c.omega[i] = i < 3 ? 0L : 253402207200L;

            _ = int.TryParse(tb_pleft.Text, out int temp);
            c.level += (temp << 24);
            _ = int.TryParse(tb_pright.Text, out temp);
            c.level += (temp << 16);
            if (tb_atk.Text == "?" || tb_atk.Text == "？")
            {
                c.atk = -2;
            }
            else if (tb_atk.Text == ".")
            {
                c.atk = -1;
            }
            else
            {
                _ = int.TryParse(tb_atk.Text, out c.atk);
            }

            if (c.IsType(Core.Info.CardType.TYPE_LINK))
            {
                c.def = (int)GetCheck(pl_markers);
            }
            else
            {
                if (tb_def.Text == "?" || tb_def.Text == "？")
                {
                    c.def = -2;
                }
                else if (tb_def.Text == ".")
                {
                    c.def = -1;
                }
                else
                {
                    _ = int.TryParse(tb_def.Text, out c.def);
                }
            }
            _ = long.TryParse(tb_cardcode.Text, out c.id);
            _ = long.TryParse(tb_cardalias.Text, out c.alias);

            return c;
        }
        #endregion

        #region 卡片列表
        //列表选择
        void Lv_cardlistSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_cardlist.SelectedItems.Count > 0)
            {
                int sel = lv_cardlist.SelectedItems[0].Index;
                int index = (page - 1) * maxRow + sel;
                if (index < cardlist.Count)
                {
                    Card c = cardlist[index];
                    SetCard(c);
                }
            }
        }
        //列表按键
        void Lv_cardlistKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    cmdManager.ExcuteCommand(cardedit.delCard, menuitem_operacardsfile.Checked);
                    break;
                case Keys.Right:
                    Btn_PageDownClick(null, null);
                    break;
                case Keys.Left:
                    Btn_PageUpClick(null, null);
                    break;
            }
        }
        //上一页
        void Btn_PageUpClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            page--;
            AddListView(page);
        }
        //下一页
        void Btn_PageDownClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            page++;
            AddListView(page);
        }
        //跳转到指定页数
        void Tb_pageKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _ = int.TryParse(tb_page.Text, out int p);
                if (p > 0)
                {
                    AddListView(p);
                }
            }
        }
        #endregion

        #region 卡片搜索，打开
        //检查是否打开数据库
        public bool CheckOpen()
        {
            if (File.Exists(nowCdbFile))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //打开数据库
        public bool Open(string file, string name = "")
        {
            SetCDB(file);
            if (!File.Exists(file))
            {
                MyMsg.Error(LMSG.FileIsNotExists);
                return false;
            }
            //清空
            tmpCodes.Clear();
            cardlist.Clear();
            //检查表是否存在
            _ = DataBase.CheckTable(file);
            srcCard = new Card();
            SetCards(DataBase.Read(file, true, ""), false);
            return true;
        }
        //setcode的搜索
        public static bool CardFilter(Card c, Card sc)
        {
            bool res = true;
            if (sc.setcode != 0)
            {
                res &= c.IsSetCode(sc.setcode & 0xffff);
            }

            return res;
        }
        //设置卡片列表的结果
        public void SetCards(Card[] cards, bool isfresh)
        {
            if (cards != null)
            {
                cardlist.Clear();
                foreach (Card c in cards)
                {
                    if (CardFilter(c, srcCard))
                    {
                        cardlist.Add(c);
                    }
                }
                cardcount = cardlist.Count;
                pageNum = cardcount / maxRow;
                if (cardcount % maxRow > 0)
                {
                    pageNum++;
                }
                else if (cardcount == 0)
                {
                    pageNum = 1;
                }

                tb_pagenum.Text = pageNum.ToString();

                if (isfresh)//是否跳到之前页数
                {
                    AddListView(page);
                }
                else
                {
                    AddListView(1);
                }
            }
            else
            {//结果为空
                cardcount = 0;
                page = 1;
                pageNum = 1;
                tb_page.Text = page.ToString();
                tb_pagenum.Text = pageNum.ToString();
                cardlist.Clear();
                lv_cardlist.Items.Clear();
            }
        }
        //搜索卡片
        public void Search(bool isfresh)
        {
            Search(srcCard, isfresh);
        }
        void Search(Card c, bool isfresh)
        {
            if (!CheckOpen())
            {
                return;
            }
            //如果临时卡片不为空，则更新，这个在搜索的时候清空
            if (tmpCodes.Count > 0)
            {
                _ = DataBase.Read(nowCdbFile,
                                              true, tmpCodes.ToArray());
                SetCards(GetCompCards(), true);
            }
            else
            {
                srcCard = c;
                string sql = c.omega != null && c.omega[0] > 0 || !nowCdbFile.EndsWith(".cdb")
                    ? DataBase.OmegaGetSelectSQL(c) : DataBase.GetSelectSQL(c);
                SetCards(DataBase.Read(nowCdbFile, true, sql), isfresh);
            }
            if (lv_cardlist.Items.Count > 0)
            {
                lv_cardlist.SelectedIndices.Clear();
                _ = lv_cardlist.SelectedIndices.Add(0);
            }
        }
        //更新临时卡片
        public void Reset()
        {
            oldCard = new Card(0);
            SetCard(oldCard);
        }
        #endregion

        #region 按钮
        //搜索卡片
        void Btn_serachClick(object sender, EventArgs e)
        {
            tmpCodes.Clear();//清空临时的结果
            Search(GetCard(), false);
        }
        //重置卡片
        void Btn_resetClick(object sender, EventArgs e)
        {
            Reset();
        }
        //添加
        void Btn_addClick(object sender, EventArgs e)
        {
            if (cardedit != null)
            {
                cmdManager.ExcuteCommand(cardedit.addCard);
            }
        }
        //修改
        void Btn_modClick(object sender, EventArgs e)
        {
            if (cardedit != null)
            {
                cmdManager.ExcuteCommand(cardedit.modCard, menuitem_operacardsfile.Checked);
            }
        }
        //打开脚本
        void Btn_luaClick(object sender, EventArgs e)
        {
            if (cardedit != null)
            {
                _ = cardedit.OpenScript(menuitem_openfileinthis.Checked, Addrequire);
            }
        }
        //删除
        void Btn_delClick(object sender, EventArgs e)
        {
            if (cardedit != null)
            {
                cmdManager.ExcuteCommand(cardedit.delCard, menuitem_operacardsfile.Checked);
            }
        }
        //撤销
        void Btn_undoClick(object sender, EventArgs e)
        {
            if (!MyMsg.Question(LMSG.UndoConfirm))
            {
                return;
            }
            if (cardedit != null)
            {
                cmdManager.Undo();
                Search(true);
            }
        }
        //导入卡图
        void Btn_imgClick(object sender, EventArgs e)
        {
            ImportImageFromSelect();
        }
        #endregion

        #region 文本框
        //卡片密码搜索
        void Tb_cardcodeKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Card c = new(0);
                _ = long.TryParse(tb_cardcode.Text, out c.id);
                if (c.id > 0)
                {
                    tmpCodes.Clear();//清空临时的结果
                    Search(c, false);
                }
            }
        }
        //卡片名称搜索、编辑
        void Tb_cardnameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Card c = new(0)
                {
                    name = tb_cardname.Text
                };
                if (c.name.Length > 0)
                {
                    tmpCodes.Clear();//清空临时的结果
                    Search(c, false);
                }
            }
            if (e.KeyCode == Keys.R && e.Control)
            {
                Btn_resetClick(null, null);
            }
        }
        //卡片描述编辑
        void Setscripttext(string str)
        {
            int index;
            try
            {
                index = lb_scripttext.SelectedIndex;
            }
            catch
            {
                index = -1;
                MyMsg.Error(LMSG.NotSelectScriptText);
            }
            if (index >= 0)
            {
                strs[index] = str;

                lb_scripttext.Items.Clear();
                lb_scripttext.Items.AddRange(strs);
                lb_scripttext.SelectedIndex = index;
            }
        }

        string Getscripttext()
        {
            int index;
            try
            {
                index = lb_scripttext.SelectedIndex;
            }
            catch
            {
                index = -1;
                MyMsg.Error(LMSG.NotSelectScriptText);
            }
            if (index >= 0)
            {
                return strs[index];
            }
            else
            {
                return "";
            }
        }
        //脚本文本
        void Lb_scripttextSelectedIndexChanged(object sender, EventArgs e)
        {
            tb_edittext.Text = Getscripttext();
        }

        //脚本文本
        void Tb_edittextTextChanged(object sender, EventArgs e)
        {
            Setscripttext(tb_edittext.Text);
        }
        #endregion

        #region 帮助菜单
        void Menuitem_aboutClick(object sender, EventArgs e)
        {
            MyMsg.Show(
                LanguageHelper.GetMsg(LMSG.About) + "\t" + Application.ProductName + "\n"
                + LanguageHelper.GetMsg(LMSG.Version) + "\t" + Application.ProductVersion + "\n"
                + LanguageHelper.GetMsg(LMSG.Author) + "\tLyris");
        }

        void Menuitem_checkupdateClick(object sender, EventArgs e)
        {
            CheckUpdate(true);
        }
        public void CheckUpdate(bool showNew)
        {
            if (!IsRun())
            {
                tasker.SetTask(MyTask.CheckUpdate, null, showNew.ToString());
                Run(LanguageHelper.GetMsg(LMSG.checkUpdate));
            }
        }
        bool CancelTask()
        {
            bool bl = false;
            if (tasker != null && tasker.IsRuning())
            {
                bl = MyMsg.Question(LMSG.IfCancelTask);
                if (bl)
                {
                    if (tasker != null)
                    {
                        tasker.Cancel();
                    }

                    if (bgWorker1.IsBusy)
                    {
                        bgWorker1.CancelAsync();
                    }
                }
            }
            return bl;
        }
        void Menuitem_cancelTaskClick(object sender, EventArgs e)
        {
            _ = CancelTask();
        }
        void Menuitem_githubClick(object sender, EventArgs e)
        {
            _ = System.Diagnostics.Process.Start(DEXConfig.ReadString(DEXConfig.TAG_SOURCE_URL));
        }
        #endregion

        #region 文件菜单
        //打开文件
        void Menuitem_openClick(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _ = Open(dlg.FileName);
            }
        }
        //新建文件
        void Menuitem_newClick(object sender, EventArgs e)
        {
            using SaveFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (DataBase.Create(dlg.FileName))
                {
                    if (MyMsg.Question(LMSG.IfOpenDataBase))
                    {
                        _ = Open(dlg.FileName);
                    }
                }
            }
        }
        //读取ydk
        void Menuitem_readydkClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            using OpenFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.SelectYdkPath);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.ydkType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tmpCodes.Clear();
                string[] ids = YGOUtil.ReadYDK(dlg.FileName);
                if (MessageBox.Show("Show cards outside of this YDK?", null, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    SetCards(DataBase.Read(nowCdbFile, true, "SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id and datas.id not in ("
                        + string.Join(",", ids) + ");"), false);
                    foreach (Card c in cardlist)
                        tmpCodes.Add(c.id.ToString());
                } else {
                    tmpCodes.AddRange(ids);
                    SetCards(DataBase.Read(nowCdbFile, true,
                                           ids), false);
                }
            }
        }
        void Menuitem_readlistClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            using OpenFileDialog dlg = new();
            dlg.Title = "Select decklist file";
            dlg.Filter = "Plain text files (*.txt)|*.txt|all files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tmpCodes.Clear();
                List<string> IDs = new();
                string ydkfile = dlg.FileName;
                string str;
                if (File.Exists(ydkfile))
                {
                    using FileStream f = new(ydkfile, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new(f, Encoding.Default);
                    str = sr.ReadLine();
                    while (str != null)
                    {
                        if (str.Length > 0)
                            if (IDs.IndexOf(str) < 0)
                                IDs.Add(str.Replace("\"", "\"\"").ToLowerInvariant());
                        str = sr.ReadLine();
                    }
                    sr.Close();
                    f.Close();
                }
                if (IDs.Count == 0)
                    return;
                string[] names = IDs.ToArray();
                if (MessageBox.Show("Show cards outside of this YDK?", null, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    SetCards(DataBase.Read(nowCdbFile, true, "SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id and lower(name) not in (\"" + string.Join("\",\"", names) + "\");"), false);
                } else {
                    SetCards(DataBase.Read(nowCdbFile, true, "SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id and lower(name) in (\"" + string.Join("\",\"", names) + "\");"), false);
                }
                foreach (Card c in cardlist)
                    tmpCodes.Add(c.id.ToString());
            }
        }
        //从图片文件夹读取
        void Menuitem_readimagesClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            using FolderBrowserDialog fdlg = new();
            fdlg.Description = LanguageHelper.GetMsg(LMSG.SelectImagePath);
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                tmpCodes.Clear();
                string[] ids = YGOUtil.ReadImage(fdlg.SelectedPath);
                if (MessageBox.Show("Show cards without an image?", null, MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    SetCards(DataBase.Read(nowCdbFile, true, "SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id and alias=0 and datas.id not in (" + string.Join(",", ids) + ");"), false);
                    foreach(Card c in cardlist)
                        tmpCodes.Add(c.id.ToString());
                } else {
                    tmpCodes.AddRange(ids);
                    SetCards(DataBase.Read(nowCdbFile, true,
                                           ids.OrderBy(int.Parse).ToArray()), false);
                }
            }
        }
        void Menuitem_readscriptsClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            using FolderBrowserDialog fdlg = new();
            fdlg.Description = "Select script folder";
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                tmpCodes.Clear();
                string[] ids = YGOUtil.ReadScript(fdlg.SelectedPath);
                if (MessageBox.Show("Show cards without a script?", null,
                    MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    SetCards(DataBase.Read(nowCdbFile, true, "SELECT datas.*,texts.* FROM datas,texts WHERE datas.id=texts.id and type & 17 != 17 and alias = 0 and datas.id not in (4005,4006,4007,4010,4011,4012,4017,4018,4019,10000050,10000060,10000070," + string.Join(",", ids) + ");"), false);
                    foreach (Card c in cardlist)
                        tmpCodes.Add(c.id.ToString());
                } else {
                    tmpCodes.AddRange(ids);
                    SetCards(DataBase.Read(nowCdbFile, true,
                                       ids.OrderBy(int.Parse).ToArray()), false);
                }
            }
        }
        //关闭
        void Menuitem_quitClick(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 线程
        //是否在执行
        bool IsRun()
        {
            if (tasker != null && tasker.IsRuning())
            {
                MyMsg.Warning(LMSG.RunError);
                return true;
            }
            return false;
        }
        //执行任务
        void Run(string name)
        {
            if (IsRun())
            {
                return;
            }

            taskname = name;
            title = title + " (" + taskname + ")";
            SetTitle();
            bgWorker1.RunWorkerAsync();
        }
        //线程任务
        void BgWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            tasker.Run();
        }
        void BgWorker1ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            title = string.Format("{0} ({1}-{2})",
                                  RemoveTag(title),
                                  taskname,
                                  // e.ProgressPercentage,
                                  e.UserState);
            SetTitle();
        }
        //任务完成
        void BgWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //还原标题
            int t = title.LastIndexOf(" (");
            if (t > 0)
            {
                title = title[..t];
                SetTitle();
            }
            if (e.Error != null)
            {//出错
                if (tasker != null)
                {
                    tasker.Cancel();
                }

                if (bgWorker1.IsBusy)
                {
                    bgWorker1.CancelAsync();
                }

                MyMsg.Show(LanguageHelper.GetMsg(LMSG.TaskError) + "\n" + e.Error);
            }
            else if (tasker.IsCancel() || e.Cancelled)
            {//取消任务
                MyMsg.Show(LMSG.CancelTask);
            }
            else
            {
                MyTask mt = tasker.GetLastTask();
                switch (mt)
                {
                    case MyTask.CheckUpdate:
                        break;
                    case MyTask.ExportData:
                        MyMsg.Show(LMSG.ExportDataOK);
                        break;
                    case MyTask.CutImages:
                        MyMsg.Show(LMSG.CutImageOK);
                        break;
                    case MyTask.SaveAsMSE:
                        MyMsg.Show(LMSG.SaveMseOK);
                        break;
                    case MyTask.ConvertImages:
                        MyMsg.Show(LMSG.ConvertImageOK);
                        break;
                    case MyTask.ReadMSE:
                        //保存读取的卡片
                        SaveCards(tasker.CardList);
                        MyMsg.Show(LMSG.ReadMSEisOK);
                        break;
                }
            }
        }
        #endregion

        #region 复制卡片
        //得到卡片列表，是否是选中的
        public Card[] GetCardList(bool onlyselect)
        {
            if (!CheckOpen())
            {
                return null;
            }

            List<Card> cards = new();
            if (onlyselect)
            {
                foreach (ListViewItem lvitem in lv_cardlist.SelectedItems)
                {
                    int index;
                    if (lvitem.Tag != null)
                    {
                        index = (int)lvitem.Tag;
                    }
                    else
                    {
                        index = lvitem.Index + (page - 1) * maxRow;
                    }

                    if (index >= 0 && index < cardlist.Count)
                    {
                        cards.Add(cardlist[index]);
                    }
                }
            }
            else
            {
                cards.AddRange(cardlist.ToArray());
            }

            if (cards.Count == 0)
            {
                //MyMsg.Show(LMSG.NoSelectCard);
            }
            return cards.ToArray();
        }
        void Menuitem_copytoClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            CopyTo(GetCardList(false));
        }

        void Menuitem_copyselecttoClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            CopyTo(GetCardList(true));
        }
        //保存卡片到当前数据库
        public void SaveCards(Card[] cards)
        {
            cmdManager.ExcuteCommand(cardedit.copyCard, cards);
            Search(srcCard, true);
        }

        //卡片另存为
        static void CopyTo(Card[] cards)
        {
            if (cards == null || cards.Length == 0)
            {
                return;
            }
            //select file
            bool replace = false;
            string filename = null;
            using (OpenFileDialog dlg = new())
            {
                dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
                try
                {
                    dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
                }
                catch { }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filename = dlg.FileName;
                    replace = MyMsg.Question(LMSG.IfReplaceExistingCard);
                }
            }
            if (!string.IsNullOrEmpty(filename))
            {
                _ = DataBase.CopyDB(filename, !replace, cards);
                MyMsg.Show(LMSG.CopyCardsToDBIsOK);
            }

        }
        #endregion

        #region MSE存档/裁剪图片
        //裁剪图片
        void Menuitem_cutimagesClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            if (IsRun())
            {
                return;
            }

            bool isreplace = MyMsg.Question(LMSG.IfReplaceExistingImage);
            tasker.SetTask(MyTask.CutImages, cardlist.ToArray(),
                           ygopath.picpath, isreplace.ToString());
            Run(LanguageHelper.GetMsg(LMSG.CutImage));
        }
        void Menuitem_saveasmse_selectClick(object sender, EventArgs e)
        {
            //选择
            SaveAsMSE(true);
        }

        void Menuitem_saveasmseClick(object sender, EventArgs e)
        {
            //全部
            SaveAsMSE(false);
        }
        void SaveAsMSE(bool onlyselect)
        {
            if (!CheckOpen())
            {
                return;
            }

            if (IsRun())
            {
                return;
            }

            Card[] cards = GetCardList(onlyselect);
            if (cards == null)
            {
                return;
            }
            //select save mse-set
            using SaveFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.selectMseset);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.MseType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tasker.SetTask(MyTask.SaveAsMSE, cards, dlg.FileName, "");
                Run(LanguageHelper.GetMsg(LMSG.SaveMse));
            }
        }
        #endregion

        #region 导入卡图
        void ImportImageFromSelect()
        {
            string tid = tb_cardcode.Text;
            if (tid == "0" || tid.Length == 0)
            {
                return;
            }

            using OpenFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.SelectImage) + "-" + tb_cardname.Text;
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.ImageType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //dlg.FileName;
                ImportImage(dlg.FileName, tid);
            }
        }
        private void Pl_image_DoubleClick(object sender, EventArgs e)
        {
            ImportImageFromSelect();
        }
        void Pl_imageDragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (File.Exists(files[0]))
            {
                ImportImage(files[0], tb_cardcode.Text);
            }
        }

        void Pl_imageDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link; //重要代码：表明是链接类型的数据，比如文件路径
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void Menuitem_importmseimg_Click(object sender, EventArgs e)
        {
            string tid = tb_cardcode.Text;
            menuitem_importmseimg.Checked = !menuitem_importmseimg.Checked;
            SetImage(tid);
        }
        void ImportImage(string file, string tid)
        {
            string f;
            if (pl_image.BackgroundImage != null
                && pl_image.BackgroundImage != cover)
            {//释放图片资源
                pl_image.BackgroundImage.Dispose();
                pl_image.BackgroundImage = cover;
            }
            if (menuitem_importmseimg.Checked)
            {
                if (!Directory.Exists(tasker.MSEImagePath))
                {
                    _ = Directory.CreateDirectory(tasker.MSEImagePath);
                }

                f = MyPath.Combine(tasker.MSEImagePath, tid + ".jpg");
                File.Copy(file, f, true);
            }
            else
            {
                //	tasker.ToImg(file, ygopath.GetImage(tid),
                //				 ygopath.GetImageThum(tid));
                tasker.ToImg(file, ygopath.GetImage(tid));
            }
            SetImage(tid);
        }
        public void SetImage(string id)
        {
            _ = long.TryParse(id, out long t);
            SetImage(t);
        }
        public void SetImage(long id)
        {
            string pic = ygopath.GetImage(id);
            if (menuitem_importmseimg.Checked)//显示MSE图片
            {
                string msepic = MseMaker.GetCardImagePath(tasker.MSEImagePath, oldCard);
                if (File.Exists(msepic))
                {
                    pl_image.BackgroundImage = MyBitmap.ReadImage(msepic);
                }
            }
            else if (File.Exists(pic))
            {
                pl_image.BackgroundImage = MyBitmap.ReadImage(pic);
            }
            else
            {
                pl_image.BackgroundImage = cover;
            }
        }
        void Menuitem_convertimageClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            if (IsRun())
            {
                return;
            }

            using FolderBrowserDialog fdlg = new();
            fdlg.Description = LanguageHelper.GetMsg(LMSG.SelectImagePath);
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                bool isreplace = MyMsg.Question(LMSG.IfReplaceExistingImage);
                tasker.SetTask(MyTask.ConvertImages, null,
                               fdlg.SelectedPath, ygopath.gamepath, isreplace.ToString());
                Run(LanguageHelper.GetMsg(LMSG.ConvertImage));
            }
        }
        #endregion

        #region 导出数据包
        void Menuitem_exportdataClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            if (IsRun())
            {
                return;
            }

            using SaveFileDialog dlg = new();
            dlg.InitialDirectory = ygopath.gamepath;
            try
            {
                dlg.Filter = "Zip|(*.zip|All Files(*.*)|*.*";
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tasker.SetTask(MyTask.ExportData,
                               GetCardList(false),
                               ygopath.gamepath,
                               dlg.FileName,
                               GetOpenFile(),
                               Addrequire);
                Run(LanguageHelper.GetMsg(LMSG.ExportData));
            }

        }
        #endregion

        #region 对比数据
        /// <summary>
        /// 数据一致，返回true，不存在和数据不同，则返回false
        /// </summary>
        static bool CheckCard(Card[] cards, Card card, bool checkinfo)
        {
            foreach (Card c in cards)
            {
                if (c.id != card.id)
                {
                    continue;
                }
                //data数据不一样
                if (checkinfo)
                {
                    return card.EqualsData(c);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        //读取将要对比的数据
        Card[] GetCompCards()
        {
            if (tmpCodes.Count == 0)
            {
                return null;
            }

            if (!CheckOpen())
            {
                return null;
            }

            return DataBase.Read(nowCdbFile, true, tmpCodes.ToArray());
        }
        public void CompareCards(string cdbfile, bool checktext)
        {
            if (!CheckOpen())
            {
                return;
            }

            tmpCodes.Clear();
            srcCard = new Card();
            Card[] mcards = DataBase.Read(nowCdbFile, true, "");
            Card[] cards = DataBase.Read(cdbfile, true, "");
            foreach (Card card in mcards)
            {
                if (!CheckCard(cards, card, checktext))//添加到id集合
                {
                    tmpCodes.Add(card.id.ToString());
                }
            }
            if (tmpCodes.Count == 0)
            {
                SetCards(null, false);
                return;
            }
            SetCards(GetCompCards(), false);
        }
        #endregion

        #region MSE配置菜单
        //把文件添加到菜单
        void AddMenuItemFormMSE()
        {
            if (!Directory.Exists(datapath))
            {
                return;
            }

            menuitem_mseconfig.DropDownItems.Clear();//清空
            string[] files = Directory.GetFiles(datapath);
            foreach (string file in files)
            {
                string name = MyPath.GetFullFileName(MSEConfig.TAG, file);
                //是否是MSE配置文件
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }
                //菜单文字是语言
                ToolStripMenuItem tsmi = new(name)
                {
                    ToolTipText = file//提示文字为真实路径
                };
                tsmi.Click += SetMseConfig_Click;
                if (msecfg.configName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    tsmi.Checked = true;//如果是当前，则打勾
                }

                _ = menuitem_mseconfig.DropDownItems.Add(tsmi);
            }
        }
        void SetMseConfig_Click(object sender, EventArgs e)
        {
            if (IsRun())//正在执行任务
            {
                return;
            }

            if (sender is ToolStripMenuItem tsmi)
            {
                //读取新的配置
                msecfg.SetConfig(tsmi.ToolTipText, datapath);
                //刷新菜单
                AddMenuItemFormMSE();
                //保存配置
                XMLReader.Save(DEXConfig.TAG_MSE, tsmi.Text);
            }
        }
        #endregion

        #region 查找lua函数
        private void Menuitem_findluafunc_Click(object sender, EventArgs e)
        {
            string funtxt = MyPath.Combine(datapath, DEXConfig.FILE_FUNCTION);
            using FolderBrowserDialog fd = new();
            fd.Description = "Folder Name: ocgcore";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                LuaFunction.Read(funtxt);//先读取旧函数列表
                _ = LuaFunction.Find(fd.SelectedPath);//查找新函数，并保存
                _ = MessageBox.Show("OK");
            }
        }

        #endregion

        #region 系列名textbox
        //系列名输入时
        void SetCode_InputText(int index, ComboBox cb, TextBox tb)
        {
            if (index >= 0 && index < setcodeIsedit.Length)
            {
                if (setcodeIsedit[index])//如果正在编辑
                {
                    return;
                }

                setcodeIsedit[index] = true;
                _ = int.TryParse(tb.Text, NumberStyles.HexNumber, null, out int temp);
                //tb.Text = temp.ToString("x");
                if (temp == 0 && (tb.Text != "0" || tb.Text.Length == 0))
                {
                    temp = -1;
                }

                SetSelect(cb, temp);
                setcodeIsedit[index] = false;
            }
        }
        private void Tb_setcode1_TextChanged(object sender, EventArgs e)
        {
            SetCode_InputText(1, cb_setname1, tb_setcode1);
        }

        private void Tb_setcode2_TextChanged(object sender, EventArgs e)
        {
            SetCode_InputText(2, cb_setname2, tb_setcode2);
        }

        private void Tb_setcode3_TextChanged(object sender, EventArgs e)
        {
            SetCode_InputText(3, cb_setname3, tb_setcode3);
        }

        private void Tb_setcode4_TextChanged(object sender, EventArgs e)
        {
            SetCode_InputText(4, cb_setname4, tb_setcode4);
        }
        #endregion

        #region 系列名comobox
        //系列选择框 选择时
        void SetCode_Selected(int index, ComboBox cb, TextBox tb)
        {
            if (index >= 0 && index < setcodeIsedit.Length)
            {
                if (setcodeIsedit[index])//如果正在编辑
                {
                    return;
                }

                setcodeIsedit[index] = true;
                long tmp = GetSelect(cb);
                tb.Text = tmp.ToString("x");
                setcodeIsedit[index] = false;
            }
        }
        private void Cb_setname1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCode_Selected(1, cb_setname1, tb_setcode1);
        }

        private void Cb_setname2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCode_Selected(2, cb_setname2, tb_setcode2);
        }

        private void Cb_setname3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCode_Selected(3, cb_setname3, tb_setcode3);
        }

        private void Cb_setname4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCode_Selected(4, cb_setname4, tb_setcode4);
        }
        #endregion

        #region 读取MSE存档
        private void Menuitem_readmse_Click(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            if (IsRun())
            {
                return;
            }
            //select open mse-set
            using OpenFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.selectMseset);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.MseType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bool isUpdate = MyMsg.Question(LMSG.IfReplaceExistingImage);
                tasker.SetTask(MyTask.ReadMSE, null,
                               dlg.FileName, isUpdate.ToString());
                Run(LanguageHelper.GetMsg(LMSG.ReadMSE));
            }
        }
        #endregion

        #region 压缩数据库
        private void Menuitem_compdb_Click(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            DataBase.Compression(nowCdbFile);
            MyMsg.Show(LMSG.CompDBOK);
        }
        #endregion

        #region 设置
        //删除卡片的时候，是否要删除图片和脚本
        private void Menuitem_deletecardsfile_Click(object sender, EventArgs e)
        {
            menuitem_operacardsfile.Checked = !menuitem_operacardsfile.Checked;
            XMLReader.Save(DEXConfig.TAG_DELETE_WITH, menuitem_operacardsfile.Checked.ToString().ToLower());
        }
        //用CodeEditor打开lua
        private void Menuitem_openfileinthis_Click(object sender, EventArgs e)
        {
            menuitem_openfileinthis.Checked = !menuitem_openfileinthis.Checked;
            XMLReader.Save(DEXConfig.TAG_OPEN_IN_THIS, menuitem_openfileinthis.Checked.ToString().ToLower());
        }
        //自动检查更新
        private void Menuitem_autocheckupdate_Click(object sender, EventArgs e)
        {
            menuitem_autocheckupdate.Checked = !menuitem_autocheckupdate.Checked;
            XMLReader.Save(DEXConfig.TAG_AUTO_CHECK_UPDATE, menuitem_autocheckupdate.Checked.ToString().ToLower());
        }
        //add require automatically
        private void Menuitem_addrequire_Click(object sender, EventArgs e)
        {
            Addrequire = Microsoft.VisualBasic.Interaction.InputBox("Module script?\n\nPress \"Cancel\" to remove module script.", "", Addrequire);
            menuitem_addrequire.Checked = (Addrequire.Length > 0);
            XMLReader.Save(DEXConfig.TAG_ADD_REQUIRE, Addrequire);
        }
        #endregion

        #region 语言菜单
        void GetLanguageItem()
        {
            if (!Directory.Exists(datapath))
            {
                return;
            }

            menuitem_language.DropDownItems.Clear();
            string[] files = Directory.GetFiles(datapath);
            foreach (string file in files)
            {
                string name = MyPath.GetFullFileName(DEXConfig.TAG_LANGUAGE, file);
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                TextInfo txinfo = new CultureInfo(CultureInfo.InstalledUICulture.Name).TextInfo;
                ToolStripMenuItem tsmi = new(txinfo.ToTitleCase(name))
                {
                    ToolTipText = file
                };
                tsmi.Click += SetLanguage_Click;
                if (DEXConfig.ReadString(DEXConfig.TAG_LANGUAGE).Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    tsmi.Checked = true;
                }

                _ = menuitem_language.DropDownItems.Add(tsmi);
            }
        }
        void SetLanguage_Click(object sender, EventArgs e)
        {
            if (IsRun())
            {
                return;
            }

            if (sender is ToolStripMenuItem tsmi)
            {
                XMLReader.Save(DEXConfig.TAG_LANGUAGE, tsmi.Text);
                GetLanguageItem();
                MyMsg.Show(LMSG.PlzRestart);
            }
        }
        #endregion

        //把mse存档导出为图片
        void Menuitem_exportMSEimageClick(object sender, EventArgs e)
        {
            if (IsRun())
            {
                return;
            }

            string msepath = MyPath.GetRealPath(DEXConfig.ReadString(DEXConfig.TAG_MSE_PATH));
            if (!File.Exists(msepath))
            {
                MyMsg.Error(LMSG.exportMseImagesErr);
                menuitem_exportMSEimage.Checked = false;
                return;
            }
            else
            {
                if (MseMaker.MseIsRunning())
                {
                    MseMaker.MseStop();
                    menuitem_exportMSEimage.Checked = false;
                    return;
                }
                else
                {

                }
            }
            //select open mse-set
            using OpenFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.selectMseset);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.MseType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string mseset = dlg.FileName;
                string exportpath = MyPath.GetRealPath(DEXConfig.ReadString(DEXConfig.TAG_MSE_EXPORT));
                MseMaker.ExportSet(msepath, mseset, exportpath, delegate
                {
                    menuitem_exportMSEimage.Checked = false;
                });
                menuitem_exportMSEimage.Checked = true;
            }
            else
            {
                menuitem_exportMSEimage.Checked = false;
            }
        }
        void Menuitem_testPendulumTextClick(object sender, EventArgs e)
        {
            Card c = GetCard();
            if (c != null)
            {
                tasker.TestPendulumText(c.desc);
            }
        }
        void Menuitem_export_select_sqlClick(object sender, EventArgs e)
        {
            using SaveFileDialog dlg = new();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataBase.ExportSql(dlg.FileName, GetCardList(true));
                MyMsg.Show("OK");
            }
        }
        void Menuitem_export_all_sqlClick(object sender, EventArgs e)
        {
            using SaveFileDialog dlg = new();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataBase.ExportSql(dlg.FileName, GetCardList(false));
                MyMsg.Show("OK");
            }
        }
        void Menuitem_autoreturnClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            using SaveFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Card[] cards = DataBase.Read(nowCdbFile, true, "");
                int count = cards.Length;
                if (cards == null || cards.Length == 0)
                {
                    return;
                }

                if (DataBase.Create(dlg.FileName))
                {
                    //
                    int len = DEXConfig.ReadInteger(DEXConfig.TAG_AUTO_LEN, 30);
                    for (int i = 0; i < count; i++)
                    {
                        if (cards[i].desc != null)
                        {
                            cards[i].desc = StrUtil.AutoEnter(cards[i].desc, len, ' ');
                        }
                    }
                    _ = DataBase.CopyDB(dlg.FileName, false, cards);
                    MyMsg.Show(LMSG.CopyCardsToDBIsOK);
                }
            }
        }

        void Menuitem_replaceClick(object sender, EventArgs e)
        {
            if (!CheckOpen())
            {
                return;
            }

            using SaveFileDialog dlg = new();
            dlg.Title = LanguageHelper.GetMsg(LMSG.SelectDataBasePath);
            try
            {
                dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
            }
            catch { }
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Card[] cards = DataBase.Read(nowCdbFile, true, "");
                int count = cards.Length;
                if (cards == null || cards.Length == 0)
                {
                    return;
                }

                if (DataBase.Create(dlg.FileName))
                {
                    //
                    _ = DEXConfig.ReadInteger(DEXConfig.TAG_AUTO_LEN, 30);
                    _ = DataBase.CopyDB(dlg.FileName, false, cards);
                    MyMsg.Show(LMSG.CopyCardsToDBIsOK);
                }
            }
        }

        private void Text2LinkMarks(string text)
        {
            try
            {
                long mark = Convert.ToInt64(text, 2);
                SetLinkMarks(mark, true);
            }
            catch
            {
                //
            }
        }

        void Tb_linkTextChanged(object sender, EventArgs e)
        {
            Text2LinkMarks(tb_link.Text);
        }

        private void DataEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                _ = tb_cardname.Focus();
                tb_cardname.SelectAll();
            }
        }

        private void Tb_cardtext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
            {
                Btn_resetClick(null, null);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {
                _ = tb_cardname.Focus();
            }
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            string[] drops = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<string> files = new();
            if (drops == null)
            {
                string file = (string)e.Data.GetData(DataFormats.Text);
                drops = new string[1] { file };
            }
            foreach (string file in drops)
            {
                if (Directory.Exists(file))
                {
                    files.AddRange(Directory.EnumerateFiles(file, "*.cdb", SearchOption.AllDirectories));
                    files.AddRange(Directory.EnumerateFiles(file, "*.lua", SearchOption.AllDirectories));
                }
                else if (File.Exists(file))
                {
                    files.Add(file);
                }
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
                (DockPanel.Parent as MainForm).Open(file);
            }
        }
        private static void GetTexts(string txt, out string[] tmp)
        {
            tmp = new string[2];
            bool mf = false;
            foreach (string l in txt.Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                string n = l.Replace("\r", "");
                if (n.StartsWith("[") || n.StartsWith("---")) continue;
                if (n.Contains('●') || (!string.IsNullOrEmpty(tmp[1])
                    && tmp[1].Contains("FLIP:", StringComparison.OrdinalIgnoreCase)))
                    tmp[mf ? 1 : 0] += n;
                else
                {
                    if (mf)
                    {
                        if (string.IsNullOrEmpty(tmp[1]))
                            tmp[1] = n;
                        else
                        {
                            tmp[0] += n;
                            tmp[1] = tmp[1].Replace(n, "");
                        }
                    }
                    else
                    {
                        tmp[mf ? 1 : 0] = n;
                        mf = true;
                    }
                }
            }
        }
        void TextToPendulum(object sender, EventArgs e)
        {
            string txt = tb_cardtext.Text;
            if ((GetCheck(pl_cardtype) & 0x1000000) > 0)
            {
                if (GetOpenFile().EndsWith(".cdb") && !(Regex.IsMatch(txt, "^Pendulum Scale = \\d*",
                    RegexOptions.Multiline) || txt.StartsWith("[")))
                {
                    GetTexts(txt, out string[] tmp);
                    tb_cardtext.Text = "Pendulum Scale = " + tb_pleft.Text + (tb_pleft.Text != tb_pright.Text
                        ? "/" + tb_pright.Text : "") + "\r\n[ Pendulum Effect ]\r\n" + (((GetCheck(pl_flags)
                        & 0x800000) > 0) ? "" : tmp[0])
                        + "\r\n----------------------------------------\r\n[ Monster Effect ]\r\n"
                        + (((GetCheck(pl_flags) & 0x800000) > 0) ? tmp[0] + "\r\n" : "") + tmp[1];
                }
                else if (!txt.StartsWith("←"))
                {
                    GetTexts(txt, out string[] tmp);
                    tb_cardtext.Text = "←" + tb_pleft.Text + " 【Pendulum Effect】 " + tb_pright.Text
                        + "→\r\n" + (((GetCheck(pl_flags) & 0x800000) > 0) ? "" : tmp[0])
                        + "\r\n【Monster Effect】\r\n" + (((GetCheck(pl_flags) & 0x800000) > 0) ? tmp[0] + "\r\n" : "")
                        + tmp[1];
                }
                string pregx = msecfg.regx_pendulum.Replace("\n", "\r\n");
                if (Regex.IsMatch(txt, pregx))
                {
                    string tmp = Regex.Replace(pregx
                        + msecfg.regx_monster.Replace("\n", "\r\n"), Regex.Escape("([\\S\\s]*?)"), "");
                    tmp = Regex.Replace(tmp, Regex.Escape("([\\S\\s]*)"), Regex.Escape(txt));
                    tb_cardtext.Text = tmp;
                }
            }
            else
            {
                txt = Regex.Replace(txt, msecfg.regx_pendulum, "$1");
                txt = Regex.Replace(txt, "(\\r?\\n)*---*\\r?\\n.*", "");
                txt = Regex.Replace(txt, msecfg.regx_monster, "$1");
                txt = Regex.Replace(txt, "^(\\r?\\n)*", "", RegexOptions.Multiline);
                txt = Regex.Replace(txt, "(?<!\\r)\\n", "\r\n");
                txt = Regex.Replace(txt, "←" + tb_pleft.Text + " 【Pendulum Effect】 " + tb_pright.Text + "→(\r?\n)+",
                    ""); txt = Regex.Replace(txt, "【Monster Effect】\r?\n", "");
                txt = Regex.Replace(txt, "Pendulum Scale = \\d*\\r?\\n", "");
                txt = Regex.Replace(txt, "\\[ Pendulum Effect \\]\\r?\\n", "");
                tb_cardtext.Text = Regex.Replace(txt, "\\[ Monster Effect \\]\\r?\\n", "");
            }
        }
        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void Tb_linkKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != 1 && e.KeyChar != 22 && e.KeyChar != 3 && e.KeyChar != 8)
            {
                //				MessageBox.Show("key="+(int)e.KeyChar);
                e.Handled = true;
            }
            else
            {
                Text2LinkMarks(tb_link.Text);
            }
        }
        void DataEditFormSizeChanged(object sender, EventArgs e)
        {
            InitListRows();
            AddListView(page);
            tmpCodes.Clear();//清空临时的结果
            Search(true);
        }
        private void AddArchetypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm mf;
            try
            {
                mf = DockPanel.Parent as MainForm;
            }
            catch
            {
                return;
            }
            DataConfig datacfg = mf.GetDataConfig();
            Dictionary<long, string> d = datacfg.dicSetnames;
            AddArchetypeForm form = new(d);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int setcode = form.code;
                string setname = form.name;
                if (!d.ContainsKey(setcode)) d.Add(setcode, setname);
                mf.GetCodeConfig().SetNames(d);
                InitControl(datacfg);
                DataBase.Command(GetOpenFile(), "insert or ignore into setcodes values(" + setcode + ", 0, '"
                    + setname + "', 0);");
            }
        }
        private void Pl_categoryScroll(object sender, MouseEventArgs e)
        {
            int d = e.Delta;
            int c = pl_category.VerticalScroll.Value;
            pl_category.VerticalScroll.Value = Math.Max(0, c + d / 6);
        }
        private void Pl_flagsScroll(object sender, MouseEventArgs e)
        {
            int d = e.Delta;
            int c = pl_flags.VerticalScroll.Value;
            pl_flags.VerticalScroll.Value = Math.Max(0, c + d / 6);
        }
    }
}

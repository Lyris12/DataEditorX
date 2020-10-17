/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-20
 * 时间: 9:19
 * 
 */
using DataEditorX.Config;
using DataEditorX.Controls;
using DataEditorX.Core;
using DataEditorX.Language;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DataEditorX
{
    public partial class MainForm : Form, IMainForm
    {
        #region member
        //历史
        History history;
        //数据目录
        string datapath;
        //语言配置
        string conflang;
        //数据库对比
        DataEditForm compare1, compare2;
        //临时卡片
        Card[] tCards;
        //编辑器配置
        DataConfig datacfg = null;
        CodeConfig codecfg = null;
        //将要打开的文件
        string openfile;
        #endregion

        #region 设置界面，消息语言
        public MainForm()
        {
            //初始化控件
            this.InitializeComponent();
        }
        public void SetDataPath(string datapath)
        {
            //判断是否合法
            if (string.IsNullOrEmpty(datapath))
            {
                return;
            }

            this.tCards = null;
            //数据目录
            this.datapath = datapath;
            if (MyConfig.ReadBoolean(MyConfig.TAG_ASYNC))
            {
                //后台加载数据
                this.bgWorker1.RunWorkerAsync();
            }
            else
            {
                this.Init();
                this.InitForm();
            }
        }
        void CheckUpdate()
        {
            TaskHelper.CheckVersion(false);
        }
        public void SetOpenFile(string file)
        {
            this.openfile = file;
        }
        void Init()
        {
            //文件路径
            this.conflang = MyConfig.GetLanguageFile(this.datapath);
            //游戏数据,MSE数据
            this.datacfg = new DataConfig(MyConfig.GetCardInfoFile(this.datapath));
            //初始化YGOUtil的数据
            YGOUtil.SetConfig(this.datacfg);

            //代码提示
            string funtxt = MyPath.Combine(this.datapath, MyConfig.FILE_FUNCTION);
            string conlua = MyPath.Combine(this.datapath, MyConfig.FILE_CONSTANT);
            string confstring = MyPath.Combine(this.datapath, MyConfig.FILE_STRINGS);
            this.codecfg = new CodeConfig();
            //添加函数
            this.codecfg.AddFunction(funtxt);
            //添加指示物
            this.codecfg.AddStrings(confstring);
            //添加常量
            this.codecfg.AddConstant(conlua);
            this.codecfg.SetNames(this.datacfg.dicSetnames);
            //生成菜单
            this.codecfg.InitAutoMenus();
            this.history = new History(this);
            //读取历史记录
            this.history.ReadHistory(MyPath.Combine(this.datapath, MyConfig.FILE_HISTORY));
            //加载多语言
            LanguageHelper.LoadFormLabels(this.conflang);
        }
        void InitForm()
        {
            LanguageHelper.SetFormLabel(this);

            //设置所有窗口的语言
            DockContentCollection contents = this.dockPanel.Contents;
            foreach (DockContent dc in contents)
            {
                if (dc is Form)
                {
                    LanguageHelper.SetFormLabel(dc);
                }
            }
            //添加历史菜单
            this.history.MenuHistory();

            //如果没有将要打开的文件，则打开一个空数据库标签
            if (string.IsNullOrEmpty(this.openfile))
            {
                this.OpenDataBase(null);
            }
            else
            {
                this.Open(this.openfile);
            }
        }
        #endregion

        #region 打开历史
        //清除cdb历史
        public void CdbMenuClear()
        {
            this.menuitem_history.DropDownItems.Clear();
        }
        //清除lua历史
        public void LuaMenuClear()
        {
            this.menuitem_shistory.DropDownItems.Clear();
        }
        //添加cdb历史
        public void AddCdbMenu(ToolStripItem item)
        {
            this.menuitem_history.DropDownItems.Add(item);
        }
        //添加lua历史
        public void AddLuaMenu(ToolStripItem item)
        {
            this.menuitem_shistory.DropDownItems.Add(item);
        }
        #endregion

        #region 处理窗口消息
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MyConfig.WM_OPEN://处理消息
                    string file = MyPath.Combine(Application.StartupPath, MyConfig.FILE_TEMP);
                    if (File.Exists(file))
                    {
                        this.Activate();
                        string openfile = File.ReadAllText(file);
                        //获取需要打开的文件路径
                        this.Open(openfile);
                        //File.Delete(file);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
        #endregion

        #region 打开文件
        //打开脚本
        void OpenScript(string file)
        {
            CodeEditForm cf = new CodeEditForm();
            //设置界面语言
            LanguageHelper.SetFormLabel(cf);
            //设置cdb列表
            cf.SetCDBList(this.history.GetcdbHistory());
            //初始化函数提示
            cf.InitTooltip(this.codecfg);
            //打开文件
            cf.Open(file);
            cf.Show(this.dockPanel, DockState.Document);
        }
        //打开数据库
        void OpenDataBase(string file)
        {
            DataEditForm def;
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                def = new DataEditForm(this.datapath);
            }
            else
            {
                def = new DataEditForm(this.datapath, file);
            }
            //设置语言
            LanguageHelper.SetFormLabel(def);
            //初始化界面数据
            def.InitControl(this.datacfg);
            def.Show(this.dockPanel, DockState.Document);
        }
        //打开文件
        public void Open(string file)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                return;
            }
            //添加历史
            this.history.AddHistory(file);
            //检查是否已经打开
            if (this.FindEditForm(file, true))
            {
                return;
            }
            //检查可用的
            if (this.FindEditForm(file, false))
            {
                return;
            }

            if (YGOUtil.IsScript(file))
            {
                this.OpenScript(file);
            }
            else if (YGOUtil.IsDataBase(file))
            {
                this.OpenDataBase(file);
            }
        }
        //检查是否打开
        bool FindEditForm(string file, bool isOpen)
        {
            DockContentCollection contents = this.dockPanel.Contents;
            //遍历所有标签
            foreach (DockContent dc in contents)
            {
                IEditForm edform = (IEditForm)dc;
                if (edform == null)
                {
                    continue;
                }

                if (isOpen)//是否检查打开
                {
                    if (file != null && file.Equals(edform.GetOpenFile()))
                    {
                        edform.SetActived();
                        return true;
                    }
                }
                else//检查是否空白，如果为空，则打开文件
                {
                    if (string.IsNullOrEmpty(edform.GetOpenFile()) && edform.CanOpen(file))
                    {
                        edform.Open(file);
                        edform.SetActived();
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 窗口管理
        //关闭当前
        void CloseToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this.dockPanel.ActiveContent.DockHandler != null)
            {
                this.dockPanel.ActiveContent.DockHandler.Close();
            }
        }
        //打开脚本编辑
        void Menuitem_codeeditorClick(object sender, EventArgs e)
        {
            this.OpenScript(null);
        }

        //新建DataEditorX
        void DataEditorToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.OpenDataBase(null);
        }
        //关闭其他或者所有
        void CloseMdi(bool isall)
        {
            DockContentCollection contents = this.dockPanel.Contents;
            int num = contents.Count - 1;
            try
            {
                while (num >= 0)
                {
                    if (contents[num].DockHandler.DockState == DockState.Document)
                    {
                        if (isall)
                        {
                            contents[num].DockHandler.Close();
                        }
                        else if (this.dockPanel.ActiveContent != contents[num])
                        {
                            contents[num].DockHandler.Close();
                        }
                    }
                    num--;
                }
            }
            catch { }
        }
        //关闭其他
        void CloseOtherToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.CloseMdi(false);
        }
        //关闭所有
        void CloseAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.CloseMdi(true);
        }
        #endregion

        #region 文件菜单
        //得到当前的数据编辑
        DataEditForm GetActive()
        {
            DataEditForm df = this.dockPanel.ActiveContent as DataEditForm;
            return df;
        }
        //打开文件
        void Menuitem_openClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = LanguageHelper.GetMsg(LMSG.OpenFile);
                if (this.GetActive() != null || this.dockPanel.Contents.Count == 0)//判断当前窗口是不是DataEditor
                {
                    try
                    {
                        dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        dlg.Filter = LanguageHelper.GetMsg(LMSG.ScriptFilter);
                    }
                    catch { }
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string file = dlg.FileName;
                    this.Open(file);
                }
            }
        }

        //退出
        void QuitToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Close();
        }
        //新建文件
        void Menuitem_newClick(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = LanguageHelper.GetMsg(LMSG.NewFile);
                if (this.GetActive() != null)//判断当前窗口是不是DataEditor
                {
                    try
                    {
                        dlg.Filter = LanguageHelper.GetMsg(LMSG.CdbType);
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        dlg.Filter = LanguageHelper.GetMsg(LMSG.ScriptFilter);
                    }
                    catch { }
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string file = dlg.FileName;
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    //是否是数据库
                    if (YGOUtil.IsDataBase(file))
                    {
                        if (DataBase.Create(file))//是否创建成功
                        {
                            if (MyMsg.Question(LMSG.IfOpenDataBase))//是否打开新建的数据库
                            {
                                this.Open(file);
                            }
                        }
                    }
                    else
                    {
                        this.Open(file);
                    }
                }
            }
        }
        //保存文件
        void Menuitem_saveClick(object sender, EventArgs e)
        {
            if (this.dockPanel.ActiveContent is IEditForm cf)
            {
                if (cf.Save())//是否保存成功
                {
                    MyMsg.Show(LMSG.SaveFileOK);
                }
            }
        }
        #endregion

        #region 卡片复制粘贴
        //复制选中
        void Menuitem_copyselecttoClick(object sender, EventArgs e)
        {
            DataEditForm df = this.GetActive();//获取当前的数据库编辑
            if (df != null)
            {
                this.tCards = df.GetCardList(true); //获取选中的卡片
                if (this.tCards != null)
                {
                    this.SetCopyNumber(this.tCards.Length);//显示复制卡片的数量
                    MyMsg.Show(LMSG.CopyCards);
                }
            }
        }
        //复制当前结果
        void Menuitem_copyallClick(object sender, EventArgs e)
        {
            DataEditForm df = this.GetActive();//获取当前的数据库编辑
            if (df != null)
            {
                this.tCards = df.GetCardList(false);//获取结果的所有卡片
                if (this.tCards != null)
                {
                    this.SetCopyNumber(this.tCards.Length);//显示复制卡片的数量
                    MyMsg.Show(LMSG.CopyCards);
                }
            }
        }
        //显示复制卡片的数量
        void SetCopyNumber(int c)
        {
            string tmp = this.menuitem_pastecards.Text;
            int t = tmp.LastIndexOf(" (");
            if (t > 0)
            {
                tmp = tmp.Substring(0, t);
            }

            tmp = tmp + " (" + c.ToString() + ")";
            this.menuitem_pastecards.Text = tmp;
        }
        //粘贴卡片
        void Menuitem_pastecardsClick(object sender, EventArgs e)
        {
            if (this.tCards == null)
            {
                return;
            }

            DataEditForm df = this.GetActive();
            if (df == null)
            {
                return;
            }

            df.SaveCards(this.tCards);//保存卡片
            MyMsg.Show(LMSG.PasteCards);
        }

        #endregion

        #region 数据对比
        //设置数据库1
        void Menuitem_comp1Click(object sender, EventArgs e)
        {
            this.compare1 = this.GetActive();
            if (this.compare1 != null && !string.IsNullOrEmpty(this.compare1.GetOpenFile()))
            {
                this.menuitem_comp2.Enabled = true;
                this.CompareDB();
            }
        }
        //设置数据库2
        void Menuitem_comp2Click(object sender, EventArgs e)
        {
            this.compare2 = this.GetActive();
            if (this.compare2 != null && !string.IsNullOrEmpty(this.compare2.GetOpenFile()))
            {
                this.CompareDB();
            }
        }
        //对比数据库
        void CompareDB()
        {
            if (this.compare1 == null || this.compare2 == null)
            {
                return;
            }

            string cdb1 = this.compare1.GetOpenFile();
            string cdb2 = this.compare2.GetOpenFile();
            if (string.IsNullOrEmpty(cdb1)
               || string.IsNullOrEmpty(cdb2)
               || cdb1 == cdb2)
            {
                return;
            }

            bool checktext = MyMsg.Question(LMSG.CheckText);
            //分别对比数据库
            this.compare1.CompareCards(cdb2, checktext);
            this.compare2.CompareCards(cdb1, checktext);
            MyMsg.Show(LMSG.CompareOK);
            this.menuitem_comp2.Enabled = false;
            this.compare1 = null;
            this.compare2 = null;
        }

        #endregion

        #region 后台加载数据
        private void bgWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.Init();
        }

        private void bgWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //更新UI
            this.InitForm();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //检查更新
            if (MyConfig.ReadBoolean(MyConfig.TAG_AUTO_CHECK_UPDATE))
            {
                Thread th = new Thread(this.CheckUpdate)
                {
                    IsBackground = true//如果exe结束，则线程终止
                };
                th.Start();
            }
        }

    }
}

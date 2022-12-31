/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 5月18 星期日
 * 时间: 20:22
 * 
 */
namespace DataEditorX
{
    partial class DataEditForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menu_image = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_mseconfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_readmse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_saveasmse_select = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_saveasmse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_exportMSEimage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_testpendulumtext = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_importmseimg = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_data = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_operacardsfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_openfileinthis = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_addrequire = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_readydk = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_readlist = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_readimages = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_readscripts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_compdb = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_export_select_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_export_all_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_findluafunc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_exportdata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_cutimages = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_convertimage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitem_cancelTask = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_autoreturn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_replace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_language = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_checkupdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_autocheckupdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_github = new System.Windows.Forms.ToolStripMenuItem();
            this.cb_cardattribute = new System.Windows.Forms.ComboBox();
            this.tb_cardname = new System.Windows.Forms.TextBox();
            this.cb_cardrule = new System.Windows.Forms.ComboBox();
            this.cb_cardlevel = new System.Windows.Forms.ComboBox();
            this.cb_cardrace = new System.Windows.Forms.ComboBox();
            this.cb_setname2 = new System.Windows.Forms.ComboBox();
            this.cb_setname1 = new System.Windows.Forms.ComboBox();
            this.cb_setname4 = new System.Windows.Forms.ComboBox();
            this.cb_setname3 = new System.Windows.Forms.ComboBox();
            this.tb_cardtext = new System.Windows.Forms.TextBox();
            this.tb_edittext = new System.Windows.Forms.TextBox();
            this.lb_pleft_right = new System.Windows.Forms.Label();
            this.tb_pleft = new System.Windows.Forms.TextBox();
            this.tb_pright = new System.Windows.Forms.TextBox();
            this.lb_atkdef = new System.Windows.Forms.Label();
            this.lb4 = new System.Windows.Forms.Label();
            this.tb_page = new System.Windows.Forms.TextBox();
            this.tb_pagenum = new System.Windows.Forms.TextBox();
            this.btn_PageUp = new System.Windows.Forms.Button();
            this.btn_PageDown = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.lb5 = new System.Windows.Forms.Label();
            this.tb_atk = new System.Windows.Forms.TextBox();
            this.tb_def = new System.Windows.Forms.TextBox();
            this.tb_cardcode = new System.Windows.Forms.TextBox();
            this.lb_cardalias = new System.Windows.Forms.Label();
            this.tb_cardalias = new System.Windows.Forms.TextBox();
            this.btn_mod = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_lua = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_serach = new System.Windows.Forms.Button();
            this.lb_categorys = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.pl_image = new System.Windows.Forms.Panel();
            this.lb_types = new System.Windows.Forms.Label();
            this.lb_tiptexts = new System.Windows.Forms.Label();
            this.bgWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_undo = new System.Windows.Forms.Button();
            this.btn_img = new System.Windows.Forms.Button();
            this.tb_setcode1 = new System.Windows.Forms.TextBox();
            this.tb_setcode2 = new System.Windows.Forms.TextBox();
            this.tb_setcode3 = new System.Windows.Forms.TextBox();
            this.tb_setcode4 = new System.Windows.Forms.TextBox();
            this.lb_cardcode = new System.Windows.Forms.Label();
            this.lb_markers = new System.Windows.Forms.Label();
            this.tb_link = new System.Windows.Forms.TextBox();
            this.pl_bottom = new System.Windows.Forms.Panel();
            this.pl_main = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lv_cardlist = new DataEditorX.DListView();
            this.ch_cardcode = new System.Windows.Forms.ColumnHeader();
            this.ch_cardname = new System.Windows.Forms.ColumnHeader();
            this.tb_support = new System.Windows.Forms.TextBox();
            this.pl_markers = new DataEditorX.DFlowLayoutPanel();
            this.pl_cardtype = new DataEditorX.DFlowLayoutPanel();
            this.pl_flags = new DataEditorX.DFlowLayoutPanel();
            this.pl_category = new DataEditorX.DFlowLayoutPanel();
            this.lb_scripttext = new DataEditorX.DListBox();
            this.lb_flags = new System.Windows.Forms.Label();
            this.tb_odate = new System.Windows.Forms.TextBox();
            this.tb_tdate = new System.Windows.Forms.TextBox();
            this.lb_date = new System.Windows.Forms.Label();
            this.lb_odate = new System.Windows.Forms.Label();
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_support = new System.Windows.Forms.Label();
            this.lb_tdate = new System.Windows.Forms.Label();
            this.menuitem_addarchetype = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.pl_bottom.SuspendLayout();
            this.pl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_image,
            this.menu_data,
            this.menuitem_help,
            this.menuitem_addarchetype});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(991, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // menu_image
            // 
            this.menu_image.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_mseconfig,
            this.tsep3,
            this.menuitem_readmse,
            this.menuitem_saveasmse_select,
            this.menuitem_saveasmse,
            this.menuitem_exportMSEimage,
            this.menuitem_testpendulumtext,
            this.tsep7,
            this.menuitem_importmseimg});
            this.menu_image.Name = "menu_image";
            this.menu_image.Size = new System.Drawing.Size(63, 20);
            this.menu_image.Text = "Image(&I)";
            // 
            // menuitem_mseconfig
            // 
            this.menuitem_mseconfig.Name = "menuitem_mseconfig";
            this.menuitem_mseconfig.Size = new System.Drawing.Size(210, 22);
            this.menuitem_mseconfig.Text = "MSE config";
            // 
            // tsep3
            // 
            this.tsep3.Name = "tsep3";
            this.tsep3.Size = new System.Drawing.Size(207, 6);
            // 
            // menuitem_readmse
            // 
            this.menuitem_readmse.Name = "menuitem_readmse";
            this.menuitem_readmse.Size = new System.Drawing.Size(210, 22);
            this.menuitem_readmse.Text = "Read from MSE";
            this.menuitem_readmse.Click += new System.EventHandler(this.Menuitem_readmse_Click);
            // 
            // menuitem_saveasmse_select
            // 
            this.menuitem_saveasmse_select.Name = "menuitem_saveasmse_select";
            this.menuitem_saveasmse_select.Size = new System.Drawing.Size(210, 22);
            this.menuitem_saveasmse_select.Text = "Select Save As MSE";
            this.menuitem_saveasmse_select.Click += new System.EventHandler(this.Menuitem_saveasmse_selectClick);
            // 
            // menuitem_saveasmse
            // 
            this.menuitem_saveasmse.Name = "menuitem_saveasmse";
            this.menuitem_saveasmse.Size = new System.Drawing.Size(210, 22);
            this.menuitem_saveasmse.Text = "All Now Save As MSE";
            this.menuitem_saveasmse.Click += new System.EventHandler(this.Menuitem_saveasmseClick);
            // 
            // menuitem_exportMSEimage
            // 
            this.menuitem_exportMSEimage.Name = "menuitem_exportMSEimage";
            this.menuitem_exportMSEimage.Size = new System.Drawing.Size(210, 22);
            this.menuitem_exportMSEimage.Text = "Export MSE-Set to Images";
            this.menuitem_exportMSEimage.Click += new System.EventHandler(this.Menuitem_exportMSEimageClick);
            // 
            // menuitem_testpendulumtext
            // 
            this.menuitem_testpendulumtext.Name = "menuitem_testpendulumtext";
            this.menuitem_testpendulumtext.Size = new System.Drawing.Size(210, 22);
            this.menuitem_testpendulumtext.Text = "Test Pendulum Text";
            this.menuitem_testpendulumtext.Click += new System.EventHandler(this.Menuitem_testPendulumTextClick);
            // 
            // tsep7
            // 
            this.tsep7.Name = "tsep7";
            this.tsep7.Size = new System.Drawing.Size(207, 6);
            // 
            // menuitem_importmseimg
            // 
            this.menuitem_importmseimg.Name = "menuitem_importmseimg";
            this.menuitem_importmseimg.Size = new System.Drawing.Size(210, 22);
            this.menuitem_importmseimg.Text = "Drop Image to MSE";
            this.menuitem_importmseimg.Click += new System.EventHandler(this.Menuitem_importmseimg_Click);
            // 
            // menu_data
            // 
            this.menu_data.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_operacardsfile,
            this.menuitem_openfileinthis,
            this.menuitem_addrequire,
            this.tsep2,
            this.menuitem_readydk,
            this.menuitem_readlist,
            this.menuitem_readimages,
            this.menuitem_readscripts,
            this.tsep6,
            this.menuitem_compdb,
            this.menuitem_export_select_sql,
            this.menuitem_export_all_sql,
            this.menuitem_findluafunc,
            this.menuitem_exportdata,
            this.tsep5,
            this.menuitem_cutimages,
            this.menuitem_convertimage,
            this.tsep1,
            this.menuitem_cancelTask,
            this.menuitem_autoreturn,
            this.menuitem_replace});
            this.menu_data.Name = "menu_data";
            this.menu_data.Size = new System.Drawing.Size(57, 20);
            this.menu_data.Text = "Data(&T)";
            // 
            // menuitem_operacardsfile
            // 
            this.menuitem_operacardsfile.Name = "menuitem_operacardsfile";
            this.menuitem_operacardsfile.Size = new System.Drawing.Size(222, 22);
            this.menuitem_operacardsfile.Text = "Opera Card\'s Files";
            this.menuitem_operacardsfile.Click += new System.EventHandler(this.Menuitem_deletecardsfile_Click);
            // 
            // menuitem_openfileinthis
            // 
            this.menuitem_openfileinthis.Name = "menuitem_openfileinthis";
            this.menuitem_openfileinthis.Size = new System.Drawing.Size(222, 22);
            this.menuitem_openfileinthis.Text = "Open File in This";
            this.menuitem_openfileinthis.Click += new System.EventHandler(this.Menuitem_openfileinthis_Click);
            // 
            // menuitem_addrequire
            // 
            this.menuitem_addrequire.Name = "menuitem_addrequire";
            this.menuitem_addrequire.Size = new System.Drawing.Size(222, 22);
            this.menuitem_addrequire.Text = "Add REQUIRE Automatically";
            this.menuitem_addrequire.Click += new System.EventHandler(this.Menuitem_addrequire_Click);
            // 
            // tsep2
            // 
            this.tsep2.Name = "tsep2";
            this.tsep2.Size = new System.Drawing.Size(219, 6);
            // 
            // menuitem_readydk
            // 
            this.menuitem_readydk.Name = "menuitem_readydk";
            this.menuitem_readydk.Size = new System.Drawing.Size(222, 22);
            this.menuitem_readydk.Text = "Cards Form ydk file(&Y)";
            this.menuitem_readydk.Click += new System.EventHandler(this.Menuitem_readydkClick);
            this.menuitem_readlist.Name = "menuitem_readlist";
            this.menuitem_readlist.Size = new System.Drawing.Size(222, 22);
            this.menuitem_readlist.Text = "Cards Form deck list";
            this.menuitem_readlist.Click += new System.EventHandler(this.Menuitem_readlistClick);
            // 
            // menuitem_readimages
            // 
            this.menuitem_readimages.Name = "menuitem_readimages";
            this.menuitem_readimages.Size = new System.Drawing.Size(222, 22);
            this.menuitem_readimages.Text = "Cards From Images(&I)";
            this.menuitem_readimages.Click += new System.EventHandler(this.Menuitem_readimagesClick);
            // 
            // menuitem_readimages
            // 
            this.menuitem_readscripts.Name = "menuitem_readscripts";
            this.menuitem_readscripts.Size = new System.Drawing.Size(222, 22);
            this.menuitem_readscripts.Text = "Read Cards From Scripts";
            this.menuitem_readscripts.Click += new System.EventHandler(this.Menuitem_readscriptsClick);
            // 
            // tsep6
            // 
            this.tsep6.Name = "tsep6";
            this.tsep6.Size = new System.Drawing.Size(219, 6);
            // 
            // menuitem_compdb
            // 
            this.menuitem_compdb.Name = "menuitem_compdb";
            this.menuitem_compdb.Size = new System.Drawing.Size(222, 22);
            this.menuitem_compdb.Text = "Compression DataBase";
            this.menuitem_compdb.Click += new System.EventHandler(this.Menuitem_compdb_Click);
            // 
            // menuitem_export_select_sql
            // 
            this.menuitem_export_select_sql.Name = "menuitem_export_select_sql";
            this.menuitem_export_select_sql.Size = new System.Drawing.Size(222, 22);
            this.menuitem_export_select_sql.Text = "Export select to Sql";
            this.menuitem_export_select_sql.Click += new System.EventHandler(this.Menuitem_export_select_sqlClick);
            // 
            // menuitem_export_all_sql
            // 
            this.menuitem_export_all_sql.Name = "menuitem_export_all_sql";
            this.menuitem_export_all_sql.Size = new System.Drawing.Size(222, 22);
            this.menuitem_export_all_sql.Text = "Export all to Sql";
            this.menuitem_export_all_sql.Click += new System.EventHandler(this.Menuitem_export_all_sqlClick);
            // 
            // menuitem_findluafunc
            // 
            this.menuitem_findluafunc.Name = "menuitem_findluafunc";
            this.menuitem_findluafunc.Size = new System.Drawing.Size(222, 22);
            this.menuitem_findluafunc.Text = "Find Lua Function";
            this.menuitem_findluafunc.Click += new System.EventHandler(this.Menuitem_findluafunc_Click);
            // 
            // menuitem_exportdata
            // 
            this.menuitem_exportdata.Name = "menuitem_exportdata";
            this.menuitem_exportdata.Size = new System.Drawing.Size(222, 22);
            this.menuitem_exportdata.Text = "Export Data";
            this.menuitem_exportdata.Click += new System.EventHandler(this.Menuitem_exportdataClick);
            // 
            // tsep5
            // 
            this.tsep5.Name = "tsep5";
            this.tsep5.Size = new System.Drawing.Size(219, 6);
            // 
            // menuitem_cutimages
            // 
            this.menuitem_cutimages.Name = "menuitem_cutimages";
            this.menuitem_cutimages.Size = new System.Drawing.Size(222, 22);
            this.menuitem_cutimages.Text = "Cut Images";
            this.menuitem_cutimages.Click += new System.EventHandler(this.Menuitem_cutimagesClick);
            // 
            // menuitem_convertimage
            // 
            this.menuitem_convertimage.Name = "menuitem_convertimage";
            this.menuitem_convertimage.Size = new System.Drawing.Size(222, 22);
            this.menuitem_convertimage.Text = "Import Images";
            this.menuitem_convertimage.Click += new System.EventHandler(this.Menuitem_convertimageClick);
            // 
            // tsep1
            // 
            this.tsep1.Name = "tsep1";
            this.tsep1.Size = new System.Drawing.Size(219, 6);
            // 
            // menuitem_cancelTask
            // 
            this.menuitem_cancelTask.Name = "menuitem_cancelTask";
            this.menuitem_cancelTask.Size = new System.Drawing.Size(222, 22);
            this.menuitem_cancelTask.Text = "Cancel Task";
            this.menuitem_cancelTask.Click += new System.EventHandler(this.Menuitem_cancelTaskClick);
            // 
            // menuitem_autoreturn
            // 
            this.menuitem_autoreturn.Name = "menuitem_autoreturn";
            this.menuitem_autoreturn.Size = new System.Drawing.Size(222, 22);
            this.menuitem_autoreturn.Text = "*Auto return";
            this.menuitem_autoreturn.Click += new System.EventHandler(this.Menuitem_autoreturnClick);
            // 
            // menuitem_replace
            // 
            this.menuitem_replace.Name = "menuitem_replace";
            this.menuitem_replace.Size = new System.Drawing.Size(222, 22);
            this.menuitem_replace.Text = "*Auto replace";
            this.menuitem_replace.Click += new System.EventHandler(this.Menuitem_replaceClick);
            // 
            // menuitem_help
            // 
            this.menuitem_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_about,
            this.menuitem_language,
            this.menuitem_checkupdate,
            this.menuitem_autocheckupdate,
            this.menuitem_github});
            this.menuitem_help.Name = "menuitem_help";
            this.menuitem_help.Size = new System.Drawing.Size(61, 20);
            this.menuitem_help.Text = "Help(&H)";
            // 
            // menuitem_about
            // 
            this.menuitem_about.Name = "menuitem_about";
            this.menuitem_about.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuitem_about.Size = new System.Drawing.Size(177, 22);
            this.menuitem_about.Text = "About";
            this.menuitem_about.Click += new System.EventHandler(this.Menuitem_aboutClick);
            // 
            // menuitem_language
            // 
            this.menuitem_language.Name = "menuitem_language";
            this.menuitem_language.Size = new System.Drawing.Size(177, 22);
            this.menuitem_language.Text = "Language";
            // 
            // menuitem_checkupdate
            // 
            this.menuitem_checkupdate.Name = "menuitem_checkupdate";
            this.menuitem_checkupdate.Size = new System.Drawing.Size(177, 22);
            this.menuitem_checkupdate.Text = "Check Update";
            this.menuitem_checkupdate.Click += new System.EventHandler(this.Menuitem_checkupdateClick);
            // 
            // menuitem_autocheckupdate
            // 
            this.menuitem_autocheckupdate.Name = "menuitem_autocheckupdate";
            this.menuitem_autocheckupdate.Size = new System.Drawing.Size(177, 22);
            this.menuitem_autocheckupdate.Text = "Auto Check Update";
            this.menuitem_autocheckupdate.Click += new System.EventHandler(this.Menuitem_autocheckupdate_Click);
            // 
            // menuitem_github
            // 
            this.menuitem_github.Name = "menuitem_github";
            this.menuitem_github.Size = new System.Drawing.Size(177, 22);
            this.menuitem_github.Text = "Source Code";
            this.menuitem_github.Click += new System.EventHandler(this.Menuitem_githubClick);
            // 
            // cb_cardattribute
            // 
            this.cb_cardattribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_cardattribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardattribute.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_cardattribute.FormattingEnabled = true;
            this.cb_cardattribute.Location = new System.Drawing.Point(194, 68);
            this.cb_cardattribute.Name = "cb_cardattribute";
            this.cb_cardattribute.Size = new System.Drawing.Size(150, 20);
            this.cb_cardattribute.TabIndex = 2;
            // 
            // tb_cardname
            // 
            this.tb_cardname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_cardname.Location = new System.Drawing.Point(5, 17);
            this.tb_cardname.Name = "tb_cardname";
            this.tb_cardname.Size = new System.Drawing.Size(339, 21);
            this.tb_cardname.TabIndex = 1;
            this.tb_cardname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_cardname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_cardnameKeyDown);
            // 
            // cb_cardrule
            // 
            this.cb_cardrule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_cardrule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardrule.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_cardrule.FormattingEnabled = true;
            this.cb_cardrule.Location = new System.Drawing.Point(194, 44);
            this.cb_cardrule.Name = "cb_cardrule";
            this.cb_cardrule.Size = new System.Drawing.Size(150, 20);
            this.cb_cardrule.TabIndex = 2;
            // 
            // cb_cardlevel
            // 
            this.cb_cardlevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_cardlevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardlevel.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_cardlevel.FormattingEnabled = true;
            this.cb_cardlevel.Location = new System.Drawing.Point(194, 93);
            this.cb_cardlevel.Name = "cb_cardlevel";
            this.cb_cardlevel.Size = new System.Drawing.Size(150, 20);
            this.cb_cardlevel.TabIndex = 2;
            // 
            // cb_cardrace
            // 
            this.cb_cardrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_cardrace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cardrace.DropDownWidth = 107;
            this.cb_cardrace.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_cardrace.FormattingEnabled = true;
            this.cb_cardrace.Location = new System.Drawing.Point(194, 118);
            this.cb_cardrace.Name = "cb_cardrace";
            this.cb_cardrace.Size = new System.Drawing.Size(150, 20);
            this.cb_cardrace.TabIndex = 2;
            // 
            // cb_setname2
            // 
            this.cb_setname2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_setname2.DropDownHeight = 320;
            this.cb_setname2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname2.DropDownWidth = 140;
            this.cb_setname2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_setname2.FormattingEnabled = true;
            this.cb_setname2.IntegralHeight = false;
            this.cb_setname2.ItemHeight = 12;
            this.cb_setname2.Location = new System.Drawing.Point(195, 170);
            this.cb_setname2.Name = "cb_setname2";
            this.cb_setname2.Size = new System.Drawing.Size(113, 20);
            this.cb_setname2.TabIndex = 2;
            this.cb_setname2.SelectedIndexChanged += new System.EventHandler(this.Cb_setname2_SelectedIndexChanged);
            // 
            // cb_setname1
            // 
            this.cb_setname1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_setname1.DropDownHeight = 320;
            this.cb_setname1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname1.DropDownWidth = 140;
            this.cb_setname1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_setname1.FormattingEnabled = true;
            this.cb_setname1.IntegralHeight = false;
            this.cb_setname1.ItemHeight = 12;
            this.cb_setname1.Location = new System.Drawing.Point(195, 144);
            this.cb_setname1.Name = "cb_setname1";
            this.cb_setname1.Size = new System.Drawing.Size(113, 20);
            this.cb_setname1.TabIndex = 2;
            this.cb_setname1.SelectedIndexChanged += new System.EventHandler(this.Cb_setname1_SelectedIndexChanged);
            // 
            // cb_setname4
            // 
            this.cb_setname4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_setname4.DropDownHeight = 320;
            this.cb_setname4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname4.DropDownWidth = 140;
            this.cb_setname4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_setname4.FormattingEnabled = true;
            this.cb_setname4.IntegralHeight = false;
            this.cb_setname4.ItemHeight = 12;
            this.cb_setname4.Location = new System.Drawing.Point(195, 222);
            this.cb_setname4.Name = "cb_setname4";
            this.cb_setname4.Size = new System.Drawing.Size(113, 20);
            this.cb_setname4.TabIndex = 2;
            this.cb_setname4.SelectedIndexChanged += new System.EventHandler(this.Cb_setname4_SelectedIndexChanged);
            // 
            // cb_setname3
            // 
            this.cb_setname3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_setname3.DropDownHeight = 320;
            this.cb_setname3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_setname3.DropDownWidth = 140;
            this.cb_setname3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cb_setname3.FormattingEnabled = true;
            this.cb_setname3.IntegralHeight = false;
            this.cb_setname3.ItemHeight = 12;
            this.cb_setname3.Location = new System.Drawing.Point(195, 196);
            this.cb_setname3.Name = "cb_setname3";
            this.cb_setname3.Size = new System.Drawing.Size(113, 20);
            this.cb_setname3.TabIndex = 2;
            this.cb_setname3.SelectedIndexChanged += new System.EventHandler(this.Cb_setname3_SelectedIndexChanged);
            // 
            // tb_cardtext
            // 
            this.tb_cardtext.AcceptsReturn = true;
            this.tb_cardtext.AcceptsTab = true;
            this.tb_cardtext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_cardtext.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tb_cardtext.Location = new System.Drawing.Point(4, 375);
            this.tb_cardtext.MaxLength = 5000;
            this.tb_cardtext.Multiline = true;
            this.tb_cardtext.Name = "tb_cardtext";
            this.tb_cardtext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_cardtext.Size = new System.Drawing.Size(340, 179);
            this.tb_cardtext.TabIndex = 121;
            this.tb_cardtext.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_cardtext_KeyDown);
            // 
            // tb_edittext
            // 
            this.tb_edittext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_edittext.HideSelection = false;
            this.tb_edittext.Location = new System.Drawing.Point(348, 513);
            this.tb_edittext.MaxLength = 2000;
            this.tb_edittext.Multiline = true;
            this.tb_edittext.Name = "tb_edittext";
            this.tb_edittext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_edittext.Size = new System.Drawing.Size(301, 40);
            this.tb_edittext.TabIndex = 4;
            this.tb_edittext.WordWrap = false;
            this.tb_edittext.TextChanged += new System.EventHandler(this.Tb_edittextTextChanged);
            // 
            // lb_pleft_right
            // 
            this.lb_pleft_right.AutoSize = true;
            this.lb_pleft_right.BackColor = System.Drawing.SystemColors.Control;
            this.lb_pleft_right.Location = new System.Drawing.Point(10, 311);
            this.lb_pleft_right.Name = "lb_pleft_right";
            this.lb_pleft_right.Size = new System.Drawing.Size(41, 12);
            this.lb_pleft_right.TabIndex = 7;
            this.lb_pleft_right.Text = "PScale";
            // 
            // tb_pleft
            // 
            this.tb_pleft.Location = new System.Drawing.Point(63, 306);
            this.tb_pleft.MaxLength = 12;
            this.tb_pleft.Name = "tb_pleft";
            this.tb_pleft.Size = new System.Drawing.Size(40, 21);
            this.tb_pleft.TabIndex = 115;
            this.tb_pleft.Text = "0";
            this.tb_pleft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_pright
            // 
            this.tb_pright.Location = new System.Drawing.Point(119, 306);
            this.tb_pright.MaxLength = 12;
            this.tb_pright.Name = "tb_pright";
            this.tb_pright.Size = new System.Drawing.Size(40, 21);
            this.tb_pright.TabIndex = 116;
            this.tb_pright.Text = "0";
            this.tb_pright.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_atkdef
            // 
            this.lb_atkdef.AutoSize = true;
            this.lb_atkdef.Location = new System.Drawing.Point(10, 335);
            this.lb_atkdef.Name = "lb_atkdef";
            this.lb_atkdef.Size = new System.Drawing.Size(47, 12);
            this.lb_atkdef.TabIndex = 7;
            this.lb_atkdef.Text = "ATK/DEF";
            // 
            // lb4
            // 
            this.lb4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb4.AutoSize = true;
            this.lb4.Location = new System.Drawing.Point(107, 11);
            this.lb4.Name = "lb4";
            this.lb4.Size = new System.Drawing.Size(11, 12);
            this.lb4.TabIndex = 7;
            this.lb4.Text = "/";
            // 
            // tb_page
            // 
            this.tb_page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_page.Location = new System.Drawing.Point(71, 7);
            this.tb_page.MaxLength = 12;
            this.tb_page.Name = "tb_page";
            this.tb_page.Size = new System.Drawing.Size(34, 21);
            this.tb_page.TabIndex = 8;
            this.tb_page.Text = "1";
            this.tb_page.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_page.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_pageKeyPress);
            // 
            // tb_pagenum
            // 
            this.tb_pagenum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_pagenum.Location = new System.Drawing.Point(121, 7);
            this.tb_pagenum.MaxLength = 12;
            this.tb_pagenum.Name = "tb_pagenum";
            this.tb_pagenum.ReadOnly = true;
            this.tb_pagenum.Size = new System.Drawing.Size(34, 21);
            this.tb_pagenum.TabIndex = 8;
            this.tb_pagenum.Text = "1";
            this.tb_pagenum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_PageUp
            // 
            this.btn_PageUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_PageUp.Location = new System.Drawing.Point(6, 3);
            this.btn_PageUp.Name = "btn_PageUp";
            this.btn_PageUp.Size = new System.Drawing.Size(64, 28);
            this.btn_PageUp.TabIndex = 5;
            this.btn_PageUp.Text = "< <";
            this.btn_PageUp.UseVisualStyleBackColor = true;
            this.btn_PageUp.Click += new System.EventHandler(this.Btn_PageUpClick);
            // 
            // btn_PageDown
            // 
            this.btn_PageDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_PageDown.Location = new System.Drawing.Point(157, 3);
            this.btn_PageDown.Name = "btn_PageDown";
            this.btn_PageDown.Size = new System.Drawing.Size(64, 28);
            this.btn_PageDown.TabIndex = 5;
            this.btn_PageDown.Text = "> >";
            this.btn_PageDown.UseVisualStyleBackColor = true;
            this.btn_PageDown.Click += new System.EventHandler(this.Btn_PageDownClick);
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add.Location = new System.Drawing.Point(683, 3);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 28);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "&Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.Btn_addClick);
            // 
            // lb5
            // 
            this.lb5.AutoSize = true;
            this.lb5.Location = new System.Drawing.Point(105, 333);
            this.lb5.Name = "lb5";
            this.lb5.Size = new System.Drawing.Size(11, 12);
            this.lb5.TabIndex = 7;
            this.lb5.Text = "/";
            // 
            // tb_atk
            // 
            this.tb_atk.Location = new System.Drawing.Point(63, 330);
            this.tb_atk.MaxLength = 12;
            this.tb_atk.Name = "tb_atk";
            this.tb_atk.Size = new System.Drawing.Size(40, 21);
            this.tb_atk.TabIndex = 117;
            this.tb_atk.Text = "0";
            this.tb_atk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_def
            // 
            this.tb_def.Location = new System.Drawing.Point(119, 330);
            this.tb_def.MaxLength = 12;
            this.tb_def.Name = "tb_def";
            this.tb_def.Size = new System.Drawing.Size(40, 21);
            this.tb_def.TabIndex = 118;
            this.tb_def.Text = "0";
            this.tb_def.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_cardcode
            // 
            this.tb_cardcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_cardcode.Location = new System.Drawing.Point(277, 330);
            this.tb_cardcode.MaxLength = 12;
            this.tb_cardcode.Name = "tb_cardcode";
            this.tb_cardcode.Size = new System.Drawing.Size(67, 21);
            this.tb_cardcode.TabIndex = 120;
            this.tb_cardcode.Text = "0";
            this.tb_cardcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_cardcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_cardcodeKeyPress);
            // 
            // lb_cardalias
            // 
            this.lb_cardalias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_cardalias.AutoSize = true;
            this.lb_cardalias.Location = new System.Drawing.Point(212, 311);
            this.lb_cardalias.Name = "lb_cardalias";
            this.lb_cardalias.Size = new System.Drawing.Size(65, 12);
            this.lb_cardalias.TabIndex = 7;
            this.lb_cardalias.Text = "Alias Card";
            // 
            // tb_cardalias
            // 
            this.tb_cardalias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_cardalias.Location = new System.Drawing.Point(277, 306);
            this.tb_cardalias.MaxLength = 12;
            this.tb_cardalias.Name = "tb_cardalias";
            this.tb_cardalias.Size = new System.Drawing.Size(67, 21);
            this.tb_cardalias.TabIndex = 119;
            this.tb_cardalias.Text = "0";
            this.tb_cardalias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_mod
            // 
            this.btn_mod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_mod.Location = new System.Drawing.Point(764, 3);
            this.btn_mod.Name = "btn_mod";
            this.btn_mod.Size = new System.Drawing.Size(68, 28);
            this.btn_mod.TabIndex = 5;
            this.btn_mod.Text = "&Modify";
            this.btn_mod.UseVisualStyleBackColor = true;
            this.btn_mod.Click += new System.EventHandler(this.Btn_modClick);
            // 
            // btn_del
            // 
            this.btn_del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_del.ForeColor = System.Drawing.Color.DarkRed;
            this.btn_del.Location = new System.Drawing.Point(914, 3);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(72, 28);
            this.btn_del.TabIndex = 5;
            this.btn_del.Text = "&Delete";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.Btn_delClick);
            // 
            // btn_lua
            // 
            this.btn_lua.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_lua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btn_lua.Location = new System.Drawing.Point(587, 3);
            this.btn_lua.Name = "btn_lua";
            this.btn_lua.Size = new System.Drawing.Size(80, 28);
            this.btn_lua.TabIndex = 5;
            this.btn_lua.Text = "&Lua Script";
            this.btn_lua.UseVisualStyleBackColor = true;
            this.btn_lua.Click += new System.EventHandler(this.Btn_luaClick);
            // 
            // btn_reset
            // 
            this.btn_reset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_reset.Location = new System.Drawing.Point(422, 3);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(80, 28);
            this.btn_reset.TabIndex = 5;
            this.btn_reset.Text = "&Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.Btn_resetClick);
            // 
            // btn_serach
            // 
            this.btn_serach.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_serach.Location = new System.Drawing.Point(340, 3);
            this.btn_serach.Name = "btn_serach";
            this.btn_serach.Size = new System.Drawing.Size(80, 28);
            this.btn_serach.TabIndex = 0;
            this.btn_serach.Text = "&Search";
            this.btn_serach.UseVisualStyleBackColor = true;
            this.btn_serach.Click += new System.EventHandler(this.Btn_serachClick);
            // 
            // lb_categorys
            // 
            this.lb_categorys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_categorys.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_categorys.Location = new System.Drawing.Point(348, 148);
            this.lb_categorys.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lb_categorys.Name = "lb_categorys";
            this.lb_categorys.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lb_categorys.Size = new System.Drawing.Size(303, 20);
            this.lb_categorys.TabIndex = 11;
            this.lb_categorys.Text = "Card Categorys";
            this.lb_categorys.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(105, 309);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(11, 12);
            this.lb2.TabIndex = 7;
            this.lb2.Text = "/";
            // 
            // pl_image
            // 
            this.pl_image.AllowDrop = true;
            this.pl_image.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pl_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pl_image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pl_image.Location = new System.Drawing.Point(5, 40);
            this.pl_image.Name = "pl_image";
            this.pl_image.Size = new System.Drawing.Size(184, 264);
            this.pl_image.TabIndex = 14;
            this.pl_image.DragDrop += new System.Windows.Forms.DragEventHandler(this.Pl_imageDragDrop);
            this.pl_image.DragEnter += new System.Windows.Forms.DragEventHandler(this.Pl_imageDragEnter);
            this.pl_image.DoubleClick += new System.EventHandler(this.Pl_image_DoubleClick);
            // 
            // lb_types
            // 
            this.lb_types.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_types.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_types.Location = new System.Drawing.Point(348, 3);
            this.lb_types.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lb_types.Name = "lb_types";
            this.lb_types.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lb_types.Size = new System.Drawing.Size(305, 20);
            this.lb_types.TabIndex = 11;
            this.lb_types.Text = "Card Types";
            this.lb_types.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_tiptexts
            // 
            this.lb_tiptexts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_tiptexts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_tiptexts.Location = new System.Drawing.Point(348, 409);
            this.lb_tiptexts.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lb_tiptexts.Name = "lb_tiptexts";
            this.lb_tiptexts.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lb_tiptexts.Size = new System.Drawing.Size(301, 20);
            this.lb_tiptexts.TabIndex = 11;
            this.lb_tiptexts.Text = "Tips Texts";
            this.lb_tiptexts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bgWorker1
            // 
            this.bgWorker1.WorkerReportsProgress = true;
            this.bgWorker1.WorkerSupportsCancellation = true;
            this.bgWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker1DoWork);
            this.bgWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker1ProgressChanged);
            this.bgWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker1RunWorkerCompleted);
            // 
            // btn_undo
            // 
            this.btn_undo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_undo.Enabled = false;
            this.btn_undo.Location = new System.Drawing.Point(836, 3);
            this.btn_undo.Name = "btn_undo";
            this.btn_undo.Size = new System.Drawing.Size(75, 28);
            this.btn_undo.TabIndex = 5;
            this.btn_undo.Text = "&Undo";
            this.btn_undo.UseVisualStyleBackColor = true;
            this.btn_undo.Click += new System.EventHandler(this.Btn_undoClick);
            // 
            // btn_img
            // 
            this.btn_img.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_img.Location = new System.Drawing.Point(505, 3);
            this.btn_img.Name = "btn_img";
            this.btn_img.Size = new System.Drawing.Size(80, 28);
            this.btn_img.TabIndex = 17;
            this.btn_img.Text = "&Image";
            this.btn_img.UseVisualStyleBackColor = true;
            this.btn_img.Click += new System.EventHandler(this.Btn_imgClick);
            // 
            // tb_setcode1
            // 
            this.tb_setcode1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_setcode1.Location = new System.Drawing.Point(314, 143);
            this.tb_setcode1.MaxLength = 4;
            this.tb_setcode1.Name = "tb_setcode1";
            this.tb_setcode1.Size = new System.Drawing.Size(30, 21);
            this.tb_setcode1.TabIndex = 111;
            this.tb_setcode1.Text = "0";
            this.tb_setcode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_setcode1.TextChanged += new System.EventHandler(this.Tb_setcode1_TextChanged);
            // 
            // tb_setcode2
            // 
            this.tb_setcode2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_setcode2.Location = new System.Drawing.Point(314, 169);
            this.tb_setcode2.MaxLength = 4;
            this.tb_setcode2.Name = "tb_setcode2";
            this.tb_setcode2.Size = new System.Drawing.Size(30, 21);
            this.tb_setcode2.TabIndex = 112;
            this.tb_setcode2.Text = "0";
            this.tb_setcode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_setcode2.TextChanged += new System.EventHandler(this.Tb_setcode2_TextChanged);
            // 
            // tb_setcode3
            // 
            this.tb_setcode3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_setcode3.Location = new System.Drawing.Point(314, 195);
            this.tb_setcode3.MaxLength = 4;
            this.tb_setcode3.Name = "tb_setcode3";
            this.tb_setcode3.Size = new System.Drawing.Size(30, 21);
            this.tb_setcode3.TabIndex = 113;
            this.tb_setcode3.Text = "0";
            this.tb_setcode3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_setcode3.TextChanged += new System.EventHandler(this.Tb_setcode3_TextChanged);
            // 
            // tb_setcode4
            // 
            this.tb_setcode4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_setcode4.Location = new System.Drawing.Point(314, 221);
            this.tb_setcode4.MaxLength = 4;
            this.tb_setcode4.Name = "tb_setcode4";
            this.tb_setcode4.Size = new System.Drawing.Size(30, 21);
            this.tb_setcode4.TabIndex = 114;
            this.tb_setcode4.Text = "0";
            this.tb_setcode4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_setcode4.TextChanged += new System.EventHandler(this.Tb_setcode4_TextChanged);
            // 
            // lb_cardcode
            // 
            this.lb_cardcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_cardcode.AutoSize = true;
            this.lb_cardcode.Location = new System.Drawing.Point(212, 335);
            this.lb_cardcode.Name = "lb_cardcode";
            this.lb_cardcode.Size = new System.Drawing.Size(59, 12);
            this.lb_cardcode.TabIndex = 7;
            this.lb_cardcode.Text = "Card Code";
            // 
            // lb_markers
            // 
            this.lb_markers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_markers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_markers.Location = new System.Drawing.Point(203, 285);
            this.lb_markers.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lb_markers.Name = "lb_markers";
            this.lb_markers.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lb_markers.Size = new System.Drawing.Size(81, 21);
            this.lb_markers.TabIndex = 19;
            this.lb_markers.Text = "Link Markers";
            this.lb_markers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_link
            // 
            this.tb_link.HideSelection = false;
            this.tb_link.Location = new System.Drawing.Point(265, 270);
            this.tb_link.MaxLength = 9;
            this.tb_link.Name = "tb_link";
            this.tb_link.Size = new System.Drawing.Size(75, 21);
            this.tb_link.TabIndex = 21;
            this.tb_link.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_link.Visible = false;
            this.tb_link.WordWrap = false;
            this.tb_link.TextChanged += new System.EventHandler(this.Tb_linkTextChanged);
            this.tb_link.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_linkKeyPress);
            // 
            // pl_bottom
            // 
            this.pl_bottom.Controls.Add(this.btn_PageDown);
            this.pl_bottom.Controls.Add(this.btn_PageUp);
            this.pl_bottom.Controls.Add(this.lb4);
            this.pl_bottom.Controls.Add(this.tb_page);
            this.pl_bottom.Controls.Add(this.tb_pagenum);
            this.pl_bottom.Controls.Add(this.btn_lua);
            this.pl_bottom.Controls.Add(this.btn_serach);
            this.pl_bottom.Controls.Add(this.btn_img);
            this.pl_bottom.Controls.Add(this.btn_reset);
            this.pl_bottom.Controls.Add(this.btn_del);
            this.pl_bottom.Controls.Add(this.btn_add);
            this.pl_bottom.Controls.Add(this.btn_mod);
            this.pl_bottom.Controls.Add(this.btn_undo);
            this.pl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pl_bottom.Location = new System.Drawing.Point(0, 583);
            this.pl_bottom.Name = "pl_bottom";
            this.pl_bottom.Size = new System.Drawing.Size(991, 34);
            this.pl_bottom.TabIndex = 22;
            // 
            // pl_main
            // 
            this.pl_main.Controls.Add(this.splitContainer);
            this.pl_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pl_main.Location = new System.Drawing.Point(0, 24);
            this.pl_main.Name = "pl_main";
            this.pl_main.Size = new System.Drawing.Size(991, 559);
            this.pl_main.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lv_cardlist);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tb_support);
            this.splitContainer.Panel2.Controls.Add(this.pl_markers);
            this.splitContainer.Panel2.Controls.Add(this.pl_cardtype);
            this.splitContainer.Panel2.Controls.Add(this.tb_cardtext);
            this.splitContainer.Panel2.Controls.Add(this.tb_cardname);
            this.splitContainer.Panel2.Controls.Add(this.lb_tiptexts);
            this.splitContainer.Panel2.Controls.Add(this.cb_cardrule);
            this.splitContainer.Panel2.Controls.Add(this.pl_flags);
            this.splitContainer.Panel2.Controls.Add(this.pl_category);
            this.splitContainer.Panel2.Controls.Add(this.cb_setname4);
            this.splitContainer.Panel2.Controls.Add(this.tb_edittext);
            this.splitContainer.Panel2.Controls.Add(this.cb_cardrace);
            this.splitContainer.Panel2.Controls.Add(this.tb_setcode4);
            this.splitContainer.Panel2.Controls.Add(this.cb_setname2);
            this.splitContainer.Panel2.Controls.Add(this.lb_scripttext);
            this.splitContainer.Panel2.Controls.Add(this.cb_setname1);
            this.splitContainer.Panel2.Controls.Add(this.lb_flags);
            this.splitContainer.Panel2.Controls.Add(this.lb_categorys);
            this.splitContainer.Panel2.Controls.Add(this.cb_cardlevel);
            this.splitContainer.Panel2.Controls.Add(this.cb_setname3);
            this.splitContainer.Panel2.Controls.Add(this.lb_types);
            this.splitContainer.Panel2.Controls.Add(this.cb_cardattribute);
            this.splitContainer.Panel2.Controls.Add(this.lb_markers);
            this.splitContainer.Panel2.Controls.Add(this.tb_link);
            this.splitContainer.Panel2.Controls.Add(this.tb_setcode3);
            this.splitContainer.Panel2.Controls.Add(this.tb_def);
            this.splitContainer.Panel2.Controls.Add(this.tb_odate);
            this.splitContainer.Panel2.Controls.Add(this.tb_cardcode);
            this.splitContainer.Panel2.Controls.Add(this.tb_pright);
            this.splitContainer.Panel2.Controls.Add(this.tb_setcode2);
            this.splitContainer.Panel2.Controls.Add(this.tb_tdate);
            this.splitContainer.Panel2.Controls.Add(this.tb_cardalias);
            this.splitContainer.Panel2.Controls.Add(this.tb_setcode1);
            this.splitContainer.Panel2.Controls.Add(this.tb_atk);
            this.splitContainer.Panel2.Controls.Add(this.lb_cardcode);
            this.splitContainer.Panel2.Controls.Add(this.lb_pleft_right);
            this.splitContainer.Panel2.Controls.Add(this.lb_date);
            this.splitContainer.Panel2.Controls.Add(this.lb_odate);
            this.splitContainer.Panel2.Controls.Add(this.lb_name);
            this.splitContainer.Panel2.Controls.Add(this.lb_support);
            this.splitContainer.Panel2.Controls.Add(this.lb_tdate);
            this.splitContainer.Panel2.Controls.Add(this.lb_cardalias);
            this.splitContainer.Panel2.Controls.Add(this.lb2);
            this.splitContainer.Panel2.Controls.Add(this.pl_image);
            this.splitContainer.Panel2.Controls.Add(this.lb_atkdef);
            this.splitContainer.Panel2.Controls.Add(this.tb_pleft);
            this.splitContainer.Panel2.Controls.Add(this.lb5);
            this.splitContainer.Size = new System.Drawing.Size(984, 556);
            this.splitContainer.SplitterDistance = 328;
            this.splitContainer.TabIndex = 22;
            // 
            // lv_cardlist
            // 
            this.lv_cardlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_cardlist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_cardlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_cardcode,
            this.ch_cardname});
            this.lv_cardlist.FullRowSelect = true;
            this.lv_cardlist.GridLines = true;
            this.lv_cardlist.HideSelection = false;
            this.lv_cardlist.LabelWrap = false;
            this.lv_cardlist.Location = new System.Drawing.Point(3, 3);
            this.lv_cardlist.Name = "lv_cardlist";
            this.lv_cardlist.Scrollable = false;
            this.lv_cardlist.ShowItemToolTips = true;
            this.lv_cardlist.Size = new System.Drawing.Size(322, 551);
            this.lv_cardlist.TabIndex = 1;
            this.lv_cardlist.UseCompatibleStateImageBehavior = false;
            this.lv_cardlist.View = System.Windows.Forms.View.Details;
            this.lv_cardlist.SelectedIndexChanged += new System.EventHandler(this.Lv_cardlistSelectedIndexChanged);
            this.lv_cardlist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Lv_cardlistKeyDown);
            // 
            // ch_cardcode
            // 
            this.ch_cardcode.Text = "Card Code";
            this.ch_cardcode.Width = 70;
            // 
            // ch_cardname
            // 
            this.ch_cardname.Text = "Card Name";
            this.ch_cardname.Width = 1294;
            // 
            // tb_support
            // 
            this.tb_support.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_support.Location = new System.Drawing.Point(195, 260);
            this.tb_support.MaxLength = 20;
            this.tb_support.Name = "tb_support";
            this.tb_support.Size = new System.Drawing.Size(81, 21);
            this.tb_support.TabIndex = 114;
            this.tb_support.Text = "0";
            this.tb_support.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_support.TextChanged += new System.EventHandler(this.Tb_setcode4_TextChanged);
            // 
            // pl_markers
            // 
            this.pl_markers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_markers.AutoScroll = true;
            this.pl_markers.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pl_markers.Location = new System.Drawing.Point(285, 244);
            this.pl_markers.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pl_markers.Name = "pl_markers";
            this.pl_markers.Padding = new System.Windows.Forms.Padding(2);
            this.pl_markers.Size = new System.Drawing.Size(63, 60);
            this.pl_markers.TabIndex = 20;
            // 
            // pl_cardtype
            // 
            this.pl_cardtype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_cardtype.AutoScroll = true;
            this.pl_cardtype.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pl_cardtype.Location = new System.Drawing.Point(348, 25);
            this.pl_cardtype.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pl_cardtype.MaximumSize = new System.Drawing.Size(360, 120);
            this.pl_cardtype.Name = "pl_cardtype";
            this.pl_cardtype.Padding = new System.Windows.Forms.Padding(2);
            this.pl_cardtype.Size = new System.Drawing.Size(301, 120);
            this.pl_cardtype.TabIndex = 12;
            this.pl_cardtype.Leave += new System.EventHandler(this.TextToPendulum);
            // 
            // pl_flags
            // 
            this.pl_flags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_flags.AutoScroll = true;
            this.pl_flags.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pl_flags.Location = new System.Drawing.Point(348, 300);
            this.pl_flags.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pl_flags.MaximumSize = new System.Drawing.Size(360, 175);
            this.pl_flags.Name = "pl_flags";
            this.pl_flags.Padding = new System.Windows.Forms.Padding(2);
            this.pl_flags.Size = new System.Drawing.Size(301, 105);
            this.pl_flags.TabIndex = 13;
            this.pl_flags.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Pl_flagsScroll);
            // 
            // pl_category
            // 
            this.pl_category.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pl_category.AutoScroll = true;
            this.pl_category.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pl_category.Location = new System.Drawing.Point(348, 169);
            this.pl_category.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pl_category.MaximumSize = new System.Drawing.Size(360, 175);
            this.pl_category.Name = "pl_category";
            this.pl_category.Padding = new System.Windows.Forms.Padding(2);
            this.pl_category.Size = new System.Drawing.Size(301, 105);
            this.pl_category.TabIndex = 13;
            this.pl_category.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Pl_categoryScroll);
            // 
            // lb_scripttext
            // 
            this.lb_scripttext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_scripttext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb_scripttext.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_scripttext.FormattingEnabled = true;
            this.lb_scripttext.IntegralHeight = false;
            this.lb_scripttext.ItemHeight = 12;
            this.lb_scripttext.Location = new System.Drawing.Point(348, 432);
            this.lb_scripttext.Name = "lb_scripttext";
            this.lb_scripttext.ScrollAlwaysVisible = true;
            this.lb_scripttext.Size = new System.Drawing.Size(301, 86);
            this.lb_scripttext.TabIndex = 6;
            this.lb_scripttext.SelectedIndexChanged += new System.EventHandler(this.Lb_scripttextSelectedIndexChanged);
            // 
            // lb_flags
            // 
            this.lb_flags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_flags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lb_flags.Location = new System.Drawing.Point(348, 277);
            this.lb_flags.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.lb_flags.Name = "lb_flags";
            this.lb_flags.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lb_flags.Size = new System.Drawing.Size(303, 20);
            this.lb_flags.TabIndex = 11;
            this.lb_flags.Text = "Card Flags";
            this.lb_flags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_odate
            // 
            this.tb_odate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_odate.Location = new System.Drawing.Point(74, 354);
            this.tb_odate.MaxLength = 20;
            this.tb_odate.Name = "tb_odate";
            this.tb_odate.Size = new System.Drawing.Size(120, 21);
            this.tb_odate.TabIndex = 120;
            this.tb_odate.Text = "9999-12-30 22:00:00";
            this.tb_odate.WordWrap = false;
            this.tb_odate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_cardcodeKeyPress);
            // 
            // tb_tdate
            // 
            this.tb_tdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_tdate.Location = new System.Drawing.Point(227, 354);
            this.tb_tdate.MaxLength = 20;
            this.tb_tdate.Name = "tb_tdate";
            this.tb_tdate.Size = new System.Drawing.Size(120, 21);
            this.tb_tdate.TabIndex = 119;
            this.tb_tdate.Text = "9999-12-30 22:00:00";
            this.tb_tdate.WordWrap = false;
            // 
            // lb_date
            // 
            this.lb_date.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_date.AutoSize = true;
            this.lb_date.Location = new System.Drawing.Point(8, 360);
            this.lb_date.Name = "lb_date";
            this.lb_date.Size = new System.Drawing.Size(41, 12);
            this.lb_date.TabIndex = 7;
            this.lb_date.Text = "Dates:";
            // 
            // lb_odate
            // 
            this.lb_odate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_odate.AutoSize = true;
            this.lb_odate.Location = new System.Drawing.Point(49, 360);
            this.lb_odate.Name = "lb_odate";
            this.lb_odate.Size = new System.Drawing.Size(23, 12);
            this.lb_odate.TabIndex = 7;
            this.lb_odate.Text = "OCG";
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(2, 3);
            this.lb_name.Margin = new System.Windows.Forms.Padding(3);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(29, 12);
            this.lb_name.TabIndex = 7;
            this.lb_name.Text = "Name";
            // 
            // lb_support
            // 
            this.lb_support.AutoSize = true;
            this.lb_support.Location = new System.Drawing.Point(195, 245);
            this.lb_support.Margin = new System.Windows.Forms.Padding(3);
            this.lb_support.Name = "lb_support";
            this.lb_support.Size = new System.Drawing.Size(47, 12);
            this.lb_support.TabIndex = 7;
            this.lb_support.Text = "Support";
            // 
            // lb_tdate
            // 
            this.lb_tdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_tdate.AutoSize = true;
            this.lb_tdate.Location = new System.Drawing.Point(202, 360);
            this.lb_tdate.Name = "lb_tdate";
            this.lb_tdate.Size = new System.Drawing.Size(23, 12);
            this.lb_tdate.TabIndex = 7;
            this.lb_tdate.Text = "TCG";
            // 
            // addArchetypeToolStripMenuItem
            // 
            this.menuitem_addarchetype.Name = "addArchetypeToolStripMenuItem";
            this.menuitem_addarchetype.Size = new System.Drawing.Size(98, 20);
            this.menuitem_addarchetype.Text = "Add Arche&type";
            this.menuitem_addarchetype.Click += new EventHandler(AddArchetypeToolStripMenuItem_Click);
            // 
            // DataEditForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(991, 617);
            this.Controls.Add(this.pl_main);
            this.Controls.Add(this.pl_bottom);
            this.Controls.Add(this.mainMenu);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "DataEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataEditorX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataEditFormFormClosing);
            this.Load += new System.EventHandler(this.DataEditFormLoad);
            this.SizeChanged += new System.EventHandler(this.DataEditFormSizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.Enter += new System.EventHandler(this.DataEditFormEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataEditForm_KeyDown);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.pl_bottom.ResumeLayout(false);
            this.pl_bottom.PerformLayout();
            this.pl_main.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripMenuItem menuitem_testpendulumtext;
        private System.Windows.Forms.ToolStripMenuItem menuitem_exportMSEimage;
        private System.Windows.Forms.ToolStripMenuItem menuitem_exportdata;
        private System.Windows.Forms.ToolStripMenuItem menu_data;
        private System.Windows.Forms.ToolStripMenuItem menuitem_cancelTask;
        private System.Windows.Forms.ToolStripSeparator tsep1;
        private System.Windows.Forms.TextBox tb_setcode4;
        private System.Windows.Forms.TextBox tb_setcode3;
        private System.Windows.Forms.TextBox tb_setcode2;
        private System.Windows.Forms.TextBox tb_setcode1;
        private System.Windows.Forms.ToolStripSeparator tsep5;
        private System.Windows.Forms.ToolStripMenuItem menuitem_convertimage;
        private System.Windows.Forms.Button btn_img;
        private System.Windows.Forms.Button btn_undo;
        private System.ComponentModel.BackgroundWorker bgWorker1;
        private System.Windows.Forms.Panel pl_image;
        private System.Windows.Forms.ToolStripMenuItem menuitem_github;
        private System.Windows.Forms.Label lb_tiptexts;
        private System.Windows.Forms.Label lb_categorys;
        private System.Windows.Forms.Label lb_types;
        private DFlowLayoutPanel pl_category;
        private DFlowLayoutPanel pl_flags;
        private DFlowLayoutPanel pl_cardtype;
        private System.Windows.Forms.Button btn_serach;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_lua;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_mod;
        private System.Windows.Forms.TextBox tb_cardalias;
        private System.Windows.Forms.Label lb_cardalias;
        private System.Windows.Forms.TextBox tb_cardcode;
        private System.Windows.Forms.Label lb_cardcode;
        private System.Windows.Forms.TextBox tb_def;
        private System.Windows.Forms.TextBox tb_atk;
        private System.Windows.Forms.Label lb5;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_PageDown;
        private System.Windows.Forms.Button btn_PageUp;
        private System.Windows.Forms.TextBox tb_pagenum;
        private System.Windows.Forms.TextBox tb_page;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.Label lb_atkdef;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.TextBox tb_pright;
        private System.Windows.Forms.TextBox tb_pleft;
        private System.Windows.Forms.Label lb_pleft_right;
        private System.Windows.Forms.TextBox tb_edittext;
        private DListBox lb_scripttext;
        private System.Windows.Forms.TextBox tb_cardtext;
        private System.Windows.Forms.ComboBox cb_setname3;
        private System.Windows.Forms.ComboBox cb_setname4;
        private System.Windows.Forms.ComboBox cb_setname1;
        private System.Windows.Forms.ComboBox cb_setname2;
        private System.Windows.Forms.ComboBox cb_cardrace;
        private System.Windows.Forms.ComboBox cb_cardlevel;
        private System.Windows.Forms.ComboBox cb_cardrule;
        private System.Windows.Forms.TextBox tb_cardname;
        private System.Windows.Forms.ComboBox cb_cardattribute;
        private System.Windows.Forms.ColumnHeader ch_cardname;
        private System.Windows.Forms.ColumnHeader ch_cardcode;
        private DListView lv_cardlist;
        private System.Windows.Forms.ToolStripMenuItem menuitem_checkupdate;
        private System.Windows.Forms.ToolStripMenuItem menuitem_about;
        private System.Windows.Forms.ToolStripMenuItem menuitem_help;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readimages;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readscripts;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readydk;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readlist;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menu_image;
        private System.Windows.Forms.ToolStripMenuItem menuitem_mseconfig;
        private System.Windows.Forms.ToolStripMenuItem menuitem_importmseimg;
        private System.Windows.Forms.ToolStripMenuItem menuitem_findluafunc;
        private System.Windows.Forms.ToolStripSeparator tsep6;
        private System.Windows.Forms.ToolStripMenuItem menuitem_compdb;
        private System.Windows.Forms.ToolStripMenuItem menuitem_readmse;
        private System.Windows.Forms.ToolStripMenuItem menuitem_saveasmse_select;
        private System.Windows.Forms.ToolStripMenuItem menuitem_saveasmse;
        private System.Windows.Forms.ToolStripSeparator tsep3;
        private System.Windows.Forms.ToolStripSeparator tsep7;
        private System.Windows.Forms.ToolStripMenuItem menuitem_cutimages;
        private System.Windows.Forms.ToolStripMenuItem menuitem_operacardsfile;
        private System.Windows.Forms.ToolStripSeparator tsep2;
		private System.Windows.Forms.ToolStripMenuItem menuitem_openfileinthis;
		private System.Windows.Forms.ToolStripMenuItem menuitem_autocheckupdate;
		private System.Windows.Forms.ToolStripMenuItem menuitem_language;
		private System.Windows.Forms.ToolStripMenuItem menuitem_export_select_sql;
		private System.Windows.Forms.ToolStripMenuItem menuitem_export_all_sql;
		private System.Windows.Forms.ToolStripMenuItem menuitem_autoreturn;
        private System.Windows.Forms.ToolStripMenuItem menuitem_replace;
        private System.Windows.Forms.Label lb_markers;
        private System.Windows.Forms.TextBox tb_link;
        private System.Windows.Forms.Panel pl_bottom;
        private System.Windows.Forms.Panel pl_main;
        private System.Windows.Forms.ToolStripMenuItem menuitem_addrequire;
        private System.Windows.Forms.SplitContainer splitContainer;
        private DFlowLayoutPanel pl_markers;
        private System.Windows.Forms.Label lb_flags;
        private System.Windows.Forms.TextBox tb_odate;
        private System.Windows.Forms.TextBox tb_tdate;
        private System.Windows.Forms.TextBox tb_support;
        private System.Windows.Forms.Label lb_tdate;
        private System.Windows.Forms.Label lb_odate;
        private System.Windows.Forms.Label lb_support;
        private System.Windows.Forms.Label lb_date;
        private System.Windows.Forms.Label lb_name;
        private ToolStripMenuItem menuitem_addarchetype;
    }
}

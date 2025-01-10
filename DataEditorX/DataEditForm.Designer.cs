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
            mainMenu = new MenuStrip();
            menu_image = new ToolStripMenuItem();
            menuitem_mseconfig = new ToolStripMenuItem();
            tsep3 = new ToolStripSeparator();
            menuitem_readmse = new ToolStripMenuItem();
            menuitem_saveasmse_select = new ToolStripMenuItem();
            menuitem_saveasmse = new ToolStripMenuItem();
            menuitem_exportMSEimage = new ToolStripMenuItem();
            menuitem_testpendulumtext = new ToolStripMenuItem();
            tsep7 = new ToolStripSeparator();
            menuitem_importmseimg = new ToolStripMenuItem();
            menu_data = new ToolStripMenuItem();
            menuitem_operacardsfile = new ToolStripMenuItem();
            menuitem_openfileinthis = new ToolStripMenuItem();
            menuitem_addrequire = new ToolStripMenuItem();
            tsep2 = new ToolStripSeparator();
            menuitem_readydk = new ToolStripMenuItem();
            menuitem_readlist = new ToolStripMenuItem();
            menuitem_readimages = new ToolStripMenuItem();
            menuitem_readscripts = new ToolStripMenuItem();
            tsep6 = new ToolStripSeparator();
            menuitem_compdb = new ToolStripMenuItem();
            menuitem_export_select_sql = new ToolStripMenuItem();
            menuitem_export_all_sql = new ToolStripMenuItem();
            menuitem_findluafunc = new ToolStripMenuItem();
            menuitem_exportdata = new ToolStripMenuItem();
            tsep5 = new ToolStripSeparator();
            menuitem_cutimages = new ToolStripMenuItem();
            menuitem_convertimage = new ToolStripMenuItem();
            tsep1 = new ToolStripSeparator();
            menuitem_cancelTask = new ToolStripMenuItem();
            menuitem_autoreturn = new ToolStripMenuItem();
            menuitem_replace = new ToolStripMenuItem();
            menuitem_help = new ToolStripMenuItem();
            menuitem_about = new ToolStripMenuItem();
            menuitem_language = new ToolStripMenuItem();
            menuitem_checkupdate = new ToolStripMenuItem();
            menuitem_autocheckupdate = new ToolStripMenuItem();
            menuitem_github = new ToolStripMenuItem();
            menuitem_addarchetype = new ToolStripMenuItem();
            cb_cardattribute = new ComboBox();
            tb_cardname = new TextBox();
            cb_cardrule = new ComboBox();
            cb_cardlevel = new ComboBox();
            cb_cardrace = new ComboBox();
            cb_setname2 = new ComboBox();
            cb_setname1 = new ComboBox();
            cb_setname4 = new ComboBox();
            cb_setname3 = new ComboBox();
            tb_cardtext = new TextBox();
            tb_edittext = new TextBox();
            lb_pleft_right = new Label();
            tb_pleft = new TextBox();
            tb_pright = new TextBox();
            lb_atkdef = new Label();
            lb4 = new Label();
            tb_page = new TextBox();
            tb_pagenum = new TextBox();
            btn_PageUp = new Button();
            btn_PageDown = new Button();
            btn_add = new Button();
            lb5 = new Label();
            tb_atk = new TextBox();
            tb_def = new TextBox();
            tb_cardcode = new TextBox();
            lb_cardalias = new Label();
            tb_cardalias = new TextBox();
            btn_mod = new Button();
            btn_del = new Button();
            btn_lua = new Button();
            btn_reset = new Button();
            btn_serach = new Button();
            lb_categorys = new Label();
            lb2 = new Label();
            pl_image = new Panel();
            lb_types = new Label();
            lb_tiptexts = new Label();
            bgWorker1 = new System.ComponentModel.BackgroundWorker();
            btn_undo = new Button();
            btn_img = new Button();
            tb_setcode1 = new TextBox();
            tb_setcode2 = new TextBox();
            tb_setcode3 = new TextBox();
            tb_setcode4 = new TextBox();
            lb_cardcode = new Label();
            lb_markers = new Label();
            tb_link = new TextBox();
            pl_bottom = new Panel();
            pl_main = new Panel();
            splitContainer = new SplitContainer();
            lv_cardlist = new DListView();
            ch_cardcode = new ColumnHeader();
            ch_cardname = new ColumnHeader();
            tb_support = new TextBox();
            pl_markers = new DFlowLayoutPanel();
            pl_cardtype = new DFlowLayoutPanel();
            pl_flags = new DFlowLayoutPanel();
            pl_category = new DFlowLayoutPanel();
            lb_scripttext = new DListBox();
            lb_flags = new Label();
            lb_name = new Label();
            lb_support = new Label();
            mainMenu.SuspendLayout();
            pl_bottom.SuspendLayout();
            pl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.Items.AddRange(new ToolStripItem[] { menu_image, menu_data, menuitem_help, menuitem_addarchetype });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Size = new Size(991, 24);
            mainMenu.TabIndex = 0;
            mainMenu.Text = "mainMenu";
            // 
            // menu_image
            // 
            menu_image.DropDownItems.AddRange(new ToolStripItem[] { menuitem_mseconfig, tsep3, menuitem_readmse, menuitem_saveasmse_select, menuitem_saveasmse, menuitem_exportMSEimage, menuitem_testpendulumtext, tsep7, menuitem_importmseimg });
            menu_image.Name = "menu_image";
            menu_image.Size = new Size(63, 20);
            menu_image.Text = "Image(&I)";
            // 
            // menuitem_mseconfig
            // 
            menuitem_mseconfig.Name = "menuitem_mseconfig";
            menuitem_mseconfig.Size = new Size(210, 22);
            menuitem_mseconfig.Text = "MSE config";
            // 
            // tsep3
            // 
            tsep3.Name = "tsep3";
            tsep3.Size = new Size(207, 6);
            // 
            // menuitem_readmse
            // 
            menuitem_readmse.Name = "menuitem_readmse";
            menuitem_readmse.Size = new Size(210, 22);
            menuitem_readmse.Text = "Read from MSE";
            menuitem_readmse.Click += Menuitem_readmse_Click;
            // 
            // menuitem_saveasmse_select
            // 
            menuitem_saveasmse_select.Name = "menuitem_saveasmse_select";
            menuitem_saveasmse_select.Size = new Size(210, 22);
            menuitem_saveasmse_select.Text = "Select Save As MSE";
            menuitem_saveasmse_select.Click += Menuitem_saveasmse_selectClick;
            // 
            // menuitem_saveasmse
            // 
            menuitem_saveasmse.Name = "menuitem_saveasmse";
            menuitem_saveasmse.Size = new Size(210, 22);
            menuitem_saveasmse.Text = "All Now Save As MSE";
            menuitem_saveasmse.Click += Menuitem_saveasmseClick;
            // 
            // menuitem_exportMSEimage
            // 
            menuitem_exportMSEimage.Name = "menuitem_exportMSEimage";
            menuitem_exportMSEimage.Size = new Size(210, 22);
            menuitem_exportMSEimage.Text = "Export MSE-Set to Images";
            menuitem_exportMSEimage.Click += Menuitem_exportMSEimageClick;
            // 
            // menuitem_testpendulumtext
            // 
            menuitem_testpendulumtext.Name = "menuitem_testpendulumtext";
            menuitem_testpendulumtext.Size = new Size(210, 22);
            menuitem_testpendulumtext.Text = "Test Pendulum Text";
            menuitem_testpendulumtext.Click += Menuitem_testPendulumTextClick;
            // 
            // tsep7
            // 
            tsep7.Name = "tsep7";
            tsep7.Size = new Size(207, 6);
            // 
            // menuitem_importmseimg
            // 
            menuitem_importmseimg.Name = "menuitem_importmseimg";
            menuitem_importmseimg.Size = new Size(210, 22);
            menuitem_importmseimg.Text = "Drop Image to MSE";
            menuitem_importmseimg.Click += Menuitem_importmseimg_Click;
            // 
            // menu_data
            // 
            menu_data.DropDownItems.AddRange(new ToolStripItem[] { menuitem_operacardsfile, menuitem_openfileinthis, menuitem_addrequire, tsep2, menuitem_readydk, menuitem_readlist, menuitem_readimages, menuitem_readscripts, tsep6, menuitem_compdb, menuitem_export_select_sql, menuitem_export_all_sql, menuitem_findluafunc, menuitem_exportdata, tsep5, menuitem_cutimages, menuitem_convertimage, tsep1, menuitem_cancelTask, menuitem_autoreturn, menuitem_replace });
            menu_data.Name = "menu_data";
            menu_data.Size = new Size(57, 20);
            menu_data.Text = "Data(&T)";
            // 
            // menuitem_operacardsfile
            // 
            menuitem_operacardsfile.Name = "menuitem_operacardsfile";
            menuitem_operacardsfile.Size = new Size(222, 22);
            menuitem_operacardsfile.Text = "Opera Card's Files";
            menuitem_operacardsfile.Click += Menuitem_deletecardsfile_Click;
            // 
            // menuitem_openfileinthis
            // 
            menuitem_openfileinthis.Name = "menuitem_openfileinthis";
            menuitem_openfileinthis.Size = new Size(222, 22);
            menuitem_openfileinthis.Text = "Open File in This";
            menuitem_openfileinthis.Click += Menuitem_openfileinthis_Click;
            // 
            // menuitem_addrequire
            // 
            menuitem_addrequire.Name = "menuitem_addrequire";
            menuitem_addrequire.Size = new Size(222, 22);
            menuitem_addrequire.Text = "Add REQUIRE Automatically";
            menuitem_addrequire.Click += Menuitem_addrequire_Click;
            // 
            // tsep2
            // 
            tsep2.Name = "tsep2";
            tsep2.Size = new Size(219, 6);
            // 
            // menuitem_readydk
            // 
            menuitem_readydk.Name = "menuitem_readydk";
            menuitem_readydk.Size = new Size(222, 22);
            menuitem_readydk.Text = "Cards Form ydk file(&Y)";
            menuitem_readydk.Click += Menuitem_readydkClick;
            // 
            // menuitem_readlist
            // 
            menuitem_readlist.Name = "menuitem_readlist";
            menuitem_readlist.Size = new Size(222, 22);
            menuitem_readlist.Text = "Cards Form deck list";
            menuitem_readlist.Click += Menuitem_readlistClick;
            // 
            // menuitem_readimages
            // 
            menuitem_readimages.Name = "menuitem_readimages";
            menuitem_readimages.Size = new Size(222, 22);
            menuitem_readimages.Text = "Cards From Images(&I)";
            menuitem_readimages.Click += Menuitem_readimagesClick;
            // 
            // menuitem_readscripts
            // 
            menuitem_readscripts.Name = "menuitem_readscripts";
            menuitem_readscripts.Size = new Size(222, 22);
            menuitem_readscripts.Text = "Read Cards From Scripts";
            menuitem_readscripts.Click += Menuitem_readscriptsClick;
            // 
            // tsep6
            // 
            tsep6.Name = "tsep6";
            tsep6.Size = new Size(219, 6);
            // 
            // menuitem_compdb
            // 
            menuitem_compdb.Name = "menuitem_compdb";
            menuitem_compdb.Size = new Size(222, 22);
            menuitem_compdb.Text = "Compression DataBase";
            menuitem_compdb.Click += Menuitem_compdb_Click;
            // 
            // menuitem_export_select_sql
            // 
            menuitem_export_select_sql.Name = "menuitem_export_select_sql";
            menuitem_export_select_sql.Size = new Size(222, 22);
            menuitem_export_select_sql.Text = "Export select to Sql";
            menuitem_export_select_sql.Click += Menuitem_export_select_sqlClick;
            // 
            // menuitem_export_all_sql
            // 
            menuitem_export_all_sql.Name = "menuitem_export_all_sql";
            menuitem_export_all_sql.Size = new Size(222, 22);
            menuitem_export_all_sql.Text = "Export all to Sql";
            menuitem_export_all_sql.Click += Menuitem_export_all_sqlClick;
            // 
            // menuitem_findluafunc
            // 
            menuitem_findluafunc.Name = "menuitem_findluafunc";
            menuitem_findluafunc.Size = new Size(222, 22);
            menuitem_findluafunc.Text = "Find Lua Function";
            menuitem_findluafunc.Click += Menuitem_findluafunc_Click;
            // 
            // menuitem_exportdata
            // 
            menuitem_exportdata.Name = "menuitem_exportdata";
            menuitem_exportdata.Size = new Size(222, 22);
            menuitem_exportdata.Text = "Export Data";
            menuitem_exportdata.Click += Menuitem_exportdataClick;
            // 
            // tsep5
            // 
            tsep5.Name = "tsep5";
            tsep5.Size = new Size(219, 6);
            // 
            // menuitem_cutimages
            // 
            menuitem_cutimages.Name = "menuitem_cutimages";
            menuitem_cutimages.Size = new Size(222, 22);
            menuitem_cutimages.Text = "Cut Images";
            menuitem_cutimages.Click += Menuitem_cutimagesClick;
            // 
            // menuitem_convertimage
            // 
            menuitem_convertimage.Name = "menuitem_convertimage";
            menuitem_convertimage.Size = new Size(222, 22);
            menuitem_convertimage.Text = "Import Images";
            menuitem_convertimage.Click += Menuitem_convertimageClick;
            // 
            // tsep1
            // 
            tsep1.Name = "tsep1";
            tsep1.Size = new Size(219, 6);
            // 
            // menuitem_cancelTask
            // 
            menuitem_cancelTask.Name = "menuitem_cancelTask";
            menuitem_cancelTask.Size = new Size(222, 22);
            menuitem_cancelTask.Text = "Cancel Task";
            menuitem_cancelTask.Click += Menuitem_cancelTaskClick;
            // 
            // menuitem_autoreturn
            // 
            menuitem_autoreturn.Name = "menuitem_autoreturn";
            menuitem_autoreturn.Size = new Size(222, 22);
            menuitem_autoreturn.Text = "*Auto return";
            menuitem_autoreturn.Click += Menuitem_autoreturnClick;
            // 
            // menuitem_replace
            // 
            menuitem_replace.Name = "menuitem_replace";
            menuitem_replace.Size = new Size(222, 22);
            menuitem_replace.Text = "*Auto replace";
            menuitem_replace.Click += Menuitem_replaceClick;
            // 
            // menuitem_help
            // 
            menuitem_help.DropDownItems.AddRange(new ToolStripItem[] { menuitem_about, menuitem_language, menuitem_checkupdate, menuitem_autocheckupdate, menuitem_github });
            menuitem_help.Name = "menuitem_help";
            menuitem_help.Size = new Size(61, 20);
            menuitem_help.Text = "Help(&H)";
            // 
            // menuitem_about
            // 
            menuitem_about.Name = "menuitem_about";
            menuitem_about.ShortcutKeys = Keys.F1;
            menuitem_about.Size = new Size(177, 22);
            menuitem_about.Text = "About";
            menuitem_about.Click += Menuitem_aboutClick;
            // 
            // menuitem_language
            // 
            menuitem_language.Name = "menuitem_language";
            menuitem_language.Size = new Size(177, 22);
            menuitem_language.Text = "Language";
            // 
            // menuitem_checkupdate
            // 
            menuitem_checkupdate.Name = "menuitem_checkupdate";
            menuitem_checkupdate.Size = new Size(177, 22);
            menuitem_checkupdate.Text = "Check Update";
            menuitem_checkupdate.Click += Menuitem_checkupdateClick;
            // 
            // menuitem_autocheckupdate
            // 
            menuitem_autocheckupdate.Name = "menuitem_autocheckupdate";
            menuitem_autocheckupdate.Size = new Size(177, 22);
            menuitem_autocheckupdate.Text = "Auto Check Update";
            menuitem_autocheckupdate.Click += Menuitem_autocheckupdate_Click;
            // 
            // menuitem_github
            // 
            menuitem_github.Name = "menuitem_github";
            menuitem_github.Size = new Size(177, 22);
            menuitem_github.Text = "Source Code";
            menuitem_github.Click += Menuitem_githubClick;
            // 
            // menuitem_addarchetype
            // 
            menuitem_addarchetype.Name = "menuitem_addarchetype";
            menuitem_addarchetype.Size = new Size(98, 20);
            menuitem_addarchetype.Text = "Add Arche&type";
            menuitem_addarchetype.Click += AddArchetypeToolStripMenuItem_Click;
            // 
            // cb_cardattribute
            // 
            cb_cardattribute.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_cardattribute.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_cardattribute.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_cardattribute.FormattingEnabled = true;
            cb_cardattribute.Location = new Point(194, 68);
            cb_cardattribute.Name = "cb_cardattribute";
            cb_cardattribute.Size = new Size(150, 20);
            cb_cardattribute.TabIndex = 2;
            // 
            // tb_cardname
            // 
            tb_cardname.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_cardname.Location = new Point(5, 17);
            tb_cardname.Name = "tb_cardname";
            tb_cardname.Size = new Size(339, 21);
            tb_cardname.TabIndex = 1;
            tb_cardname.TextAlign = HorizontalAlignment.Center;
            tb_cardname.KeyDown += Tb_cardnameKeyDown;
            // 
            // cb_cardrule
            // 
            cb_cardrule.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_cardrule.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_cardrule.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_cardrule.FormattingEnabled = true;
            cb_cardrule.Location = new Point(194, 44);
            cb_cardrule.Name = "cb_cardrule";
            cb_cardrule.Size = new Size(150, 20);
            cb_cardrule.TabIndex = 2;
            // 
            // cb_cardlevel
            // 
            cb_cardlevel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_cardlevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_cardlevel.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_cardlevel.FormattingEnabled = true;
            cb_cardlevel.Location = new Point(194, 93);
            cb_cardlevel.Name = "cb_cardlevel";
            cb_cardlevel.Size = new Size(150, 20);
            cb_cardlevel.TabIndex = 2;
            // 
            // cb_cardrace
            // 
            cb_cardrace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_cardrace.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_cardrace.DropDownWidth = 107;
            cb_cardrace.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_cardrace.FormattingEnabled = true;
            cb_cardrace.Location = new Point(194, 118);
            cb_cardrace.Name = "cb_cardrace";
            cb_cardrace.Size = new Size(150, 20);
            cb_cardrace.TabIndex = 2;
            // 
            // cb_setname2
            // 
            cb_setname2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_setname2.DropDownHeight = 320;
            cb_setname2.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_setname2.DropDownWidth = 140;
            cb_setname2.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_setname2.FormattingEnabled = true;
            cb_setname2.IntegralHeight = false;
            cb_setname2.ItemHeight = 12;
            cb_setname2.Location = new Point(195, 170);
            cb_setname2.Name = "cb_setname2";
            cb_setname2.Size = new Size(113, 20);
            cb_setname2.TabIndex = 2;
            cb_setname2.SelectedIndexChanged += Cb_setname2_SelectedIndexChanged;
            // 
            // cb_setname1
            // 
            cb_setname1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_setname1.DropDownHeight = 320;
            cb_setname1.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_setname1.DropDownWidth = 140;
            cb_setname1.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_setname1.FormattingEnabled = true;
            cb_setname1.IntegralHeight = false;
            cb_setname1.ItemHeight = 12;
            cb_setname1.Location = new Point(195, 144);
            cb_setname1.Name = "cb_setname1";
            cb_setname1.Size = new Size(113, 20);
            cb_setname1.TabIndex = 2;
            cb_setname1.SelectedIndexChanged += Cb_setname1_SelectedIndexChanged;
            // 
            // cb_setname4
            // 
            cb_setname4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_setname4.DropDownHeight = 320;
            cb_setname4.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_setname4.DropDownWidth = 140;
            cb_setname4.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_setname4.FormattingEnabled = true;
            cb_setname4.IntegralHeight = false;
            cb_setname4.ItemHeight = 12;
            cb_setname4.Location = new Point(195, 222);
            cb_setname4.Name = "cb_setname4";
            cb_setname4.Size = new Size(113, 20);
            cb_setname4.TabIndex = 2;
            cb_setname4.SelectedIndexChanged += Cb_setname4_SelectedIndexChanged;
            // 
            // cb_setname3
            // 
            cb_setname3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cb_setname3.DropDownHeight = 320;
            cb_setname3.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_setname3.DropDownWidth = 140;
            cb_setname3.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cb_setname3.FormattingEnabled = true;
            cb_setname3.IntegralHeight = false;
            cb_setname3.ItemHeight = 12;
            cb_setname3.Location = new Point(195, 196);
            cb_setname3.Name = "cb_setname3";
            cb_setname3.Size = new Size(113, 20);
            cb_setname3.TabIndex = 2;
            cb_setname3.SelectedIndexChanged += Cb_setname3_SelectedIndexChanged;
            // 
            // tb_cardtext
            // 
            tb_cardtext.AcceptsReturn = true;
            tb_cardtext.AcceptsTab = true;
            tb_cardtext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tb_cardtext.Font = new Font("Microsoft YaHei", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            tb_cardtext.Location = new Point(4, 354);
            tb_cardtext.MaxLength = 5000;
            tb_cardtext.Multiline = true;
            tb_cardtext.Name = "tb_cardtext";
            tb_cardtext.ScrollBars = ScrollBars.Vertical;
            tb_cardtext.Size = new Size(340, 200);
            tb_cardtext.TabIndex = 121;
            tb_cardtext.KeyDown += Tb_cardtext_KeyDown;
            // 
            // tb_edittext
            // 
            tb_edittext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tb_edittext.HideSelection = false;
            tb_edittext.Location = new Point(348, 513);
            tb_edittext.MaxLength = 2000;
            tb_edittext.Multiline = true;
            tb_edittext.Name = "tb_edittext";
            tb_edittext.ScrollBars = ScrollBars.Vertical;
            tb_edittext.Size = new Size(301, 40);
            tb_edittext.TabIndex = 4;
            tb_edittext.WordWrap = false;
            tb_edittext.TextChanged += Tb_edittextTextChanged;
            // 
            // lb_pleft_right
            // 
            lb_pleft_right.AutoSize = true;
            lb_pleft_right.BackColor = SystemColors.Control;
            lb_pleft_right.Location = new Point(10, 311);
            lb_pleft_right.Name = "lb_pleft_right";
            lb_pleft_right.Size = new Size(41, 12);
            lb_pleft_right.TabIndex = 7;
            lb_pleft_right.Text = "PScale";
            // 
            // tb_pleft
            // 
            tb_pleft.Location = new Point(63, 306);
            tb_pleft.MaxLength = 12;
            tb_pleft.Name = "tb_pleft";
            tb_pleft.Size = new Size(40, 21);
            tb_pleft.TabIndex = 115;
            tb_pleft.Text = "0";
            tb_pleft.TextAlign = HorizontalAlignment.Center;
            // 
            // tb_pright
            // 
            tb_pright.Location = new Point(119, 306);
            tb_pright.MaxLength = 12;
            tb_pright.Name = "tb_pright";
            tb_pright.Size = new Size(40, 21);
            tb_pright.TabIndex = 116;
            tb_pright.Text = "0";
            tb_pright.TextAlign = HorizontalAlignment.Center;
            // 
            // lb_atkdef
            // 
            lb_atkdef.AutoSize = true;
            lb_atkdef.Location = new Point(10, 335);
            lb_atkdef.Name = "lb_atkdef";
            lb_atkdef.Size = new Size(47, 12);
            lb_atkdef.TabIndex = 7;
            lb_atkdef.Text = "ATK/DEF";
            // 
            // lb4
            // 
            lb4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lb4.AutoSize = true;
            lb4.Location = new Point(107, 11);
            lb4.Name = "lb4";
            lb4.Size = new Size(11, 12);
            lb4.TabIndex = 7;
            lb4.Text = "/";
            // 
            // tb_page
            // 
            tb_page.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tb_page.Location = new Point(71, 7);
            tb_page.MaxLength = 12;
            tb_page.Name = "tb_page";
            tb_page.Size = new Size(34, 21);
            tb_page.TabIndex = 8;
            tb_page.Text = "1";
            tb_page.TextAlign = HorizontalAlignment.Center;
            tb_page.KeyPress += Tb_pageKeyPress;
            // 
            // tb_pagenum
            // 
            tb_pagenum.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tb_pagenum.Location = new Point(121, 7);
            tb_pagenum.MaxLength = 12;
            tb_pagenum.Name = "tb_pagenum";
            tb_pagenum.ReadOnly = true;
            tb_pagenum.Size = new Size(34, 21);
            tb_pagenum.TabIndex = 8;
            tb_pagenum.Text = "1";
            tb_pagenum.TextAlign = HorizontalAlignment.Center;
            // 
            // btn_PageUp
            // 
            btn_PageUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_PageUp.Location = new Point(6, 3);
            btn_PageUp.Name = "btn_PageUp";
            btn_PageUp.Size = new Size(64, 28);
            btn_PageUp.TabIndex = 5;
            btn_PageUp.Text = "< <";
            btn_PageUp.UseVisualStyleBackColor = true;
            btn_PageUp.Click += Btn_PageUpClick;
            // 
            // btn_PageDown
            // 
            btn_PageDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_PageDown.Location = new Point(157, 3);
            btn_PageDown.Name = "btn_PageDown";
            btn_PageDown.Size = new Size(64, 28);
            btn_PageDown.TabIndex = 5;
            btn_PageDown.Text = "> >";
            btn_PageDown.UseVisualStyleBackColor = true;
            btn_PageDown.Click += Btn_PageDownClick;
            // 
            // btn_add
            // 
            btn_add.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_add.Location = new Point(683, 3);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(75, 28);
            btn_add.TabIndex = 5;
            btn_add.Text = "&Add";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += Btn_addClick;
            // 
            // lb5
            // 
            lb5.AutoSize = true;
            lb5.Location = new Point(105, 333);
            lb5.Name = "lb5";
            lb5.Size = new Size(11, 12);
            lb5.TabIndex = 7;
            lb5.Text = "/";
            // 
            // tb_atk
            // 
            tb_atk.Location = new Point(63, 330);
            tb_atk.MaxLength = 12;
            tb_atk.Name = "tb_atk";
            tb_atk.Size = new Size(40, 21);
            tb_atk.TabIndex = 117;
            tb_atk.Text = "0";
            tb_atk.TextAlign = HorizontalAlignment.Center;
            // 
            // tb_def
            // 
            tb_def.Location = new Point(119, 330);
            tb_def.MaxLength = 12;
            tb_def.Name = "tb_def";
            tb_def.Size = new Size(40, 21);
            tb_def.TabIndex = 118;
            tb_def.Text = "0";
            tb_def.TextAlign = HorizontalAlignment.Center;
            // 
            // tb_cardcode
            // 
            tb_cardcode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_cardcode.Location = new Point(277, 330);
            tb_cardcode.MaxLength = 12;
            tb_cardcode.Name = "tb_cardcode";
            tb_cardcode.Size = new Size(67, 21);
            tb_cardcode.TabIndex = 120;
            tb_cardcode.Text = "0";
            tb_cardcode.TextAlign = HorizontalAlignment.Right;
            tb_cardcode.KeyPress += Tb_cardcodeKeyPress;
            // 
            // lb_cardalias
            // 
            lb_cardalias.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_cardalias.AutoSize = true;
            lb_cardalias.Location = new Point(212, 311);
            lb_cardalias.Name = "lb_cardalias";
            lb_cardalias.Size = new Size(65, 12);
            lb_cardalias.TabIndex = 7;
            lb_cardalias.Text = "Alias Card";
            // 
            // tb_cardalias
            // 
            tb_cardalias.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_cardalias.Location = new Point(277, 306);
            tb_cardalias.MaxLength = 12;
            tb_cardalias.Name = "tb_cardalias";
            tb_cardalias.Size = new Size(67, 21);
            tb_cardalias.TabIndex = 119;
            tb_cardalias.Text = "0";
            tb_cardalias.TextAlign = HorizontalAlignment.Right;
            // 
            // btn_mod
            // 
            btn_mod.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_mod.Location = new Point(764, 3);
            btn_mod.Name = "btn_mod";
            btn_mod.Size = new Size(68, 28);
            btn_mod.TabIndex = 5;
            btn_mod.Text = "&Modify";
            btn_mod.UseVisualStyleBackColor = true;
            btn_mod.Click += Btn_modClick;
            // 
            // btn_del
            // 
            btn_del.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_del.ForeColor = Color.DarkRed;
            btn_del.Location = new Point(914, 3);
            btn_del.Name = "btn_del";
            btn_del.Size = new Size(72, 28);
            btn_del.TabIndex = 5;
            btn_del.Text = "&Delete";
            btn_del.UseVisualStyleBackColor = true;
            btn_del.Click += Btn_delClick;
            // 
            // btn_lua
            // 
            btn_lua.Anchor = AnchorStyles.Top;
            btn_lua.ForeColor = Color.FromArgb(0, 64, 0);
            btn_lua.Location = new Point(587, 3);
            btn_lua.Name = "btn_lua";
            btn_lua.Size = new Size(80, 28);
            btn_lua.TabIndex = 5;
            btn_lua.Text = "&Lua Script";
            btn_lua.UseVisualStyleBackColor = true;
            btn_lua.Click += Btn_luaClick;
            // 
            // btn_reset
            // 
            btn_reset.Anchor = AnchorStyles.Top;
            btn_reset.Location = new Point(422, 3);
            btn_reset.Name = "btn_reset";
            btn_reset.Size = new Size(80, 28);
            btn_reset.TabIndex = 5;
            btn_reset.Text = "&Reset";
            btn_reset.UseVisualStyleBackColor = true;
            btn_reset.Click += Btn_resetClick;
            // 
            // btn_serach
            // 
            btn_serach.Anchor = AnchorStyles.Top;
            btn_serach.Location = new Point(340, 3);
            btn_serach.Name = "btn_serach";
            btn_serach.Size = new Size(80, 28);
            btn_serach.TabIndex = 0;
            btn_serach.Text = "&Search";
            btn_serach.UseVisualStyleBackColor = true;
            btn_serach.Click += Btn_serachClick;
            // 
            // lb_categorys
            // 
            lb_categorys.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_categorys.BackColor = Color.FromArgb(192, 192, 255);
            lb_categorys.Location = new Point(348, 148);
            lb_categorys.Margin = new Padding(3, 3, 3, 0);
            lb_categorys.Name = "lb_categorys";
            lb_categorys.Padding = new Padding(4, 0, 0, 0);
            lb_categorys.Size = new Size(303, 20);
            lb_categorys.TabIndex = 11;
            lb_categorys.Text = "Card Categorys";
            lb_categorys.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lb2
            // 
            lb2.AutoSize = true;
            lb2.Location = new Point(105, 309);
            lb2.Name = "lb2";
            lb2.Size = new Size(11, 12);
            lb2.TabIndex = 7;
            lb2.Text = "/";
            // 
            // pl_image
            // 
            pl_image.AllowDrop = true;
            pl_image.BackColor = SystemColors.ButtonHighlight;
            pl_image.BackgroundImageLayout = ImageLayout.Zoom;
            pl_image.BorderStyle = BorderStyle.FixedSingle;
            pl_image.Location = new Point(5, 40);
            pl_image.Name = "pl_image";
            pl_image.Size = new Size(184, 264);
            pl_image.TabIndex = 14;
            pl_image.DragDrop += Pl_imageDragDrop;
            pl_image.DragEnter += Pl_imageDragEnter;
            pl_image.DoubleClick += Pl_image_DoubleClick;
            // 
            // lb_types
            // 
            lb_types.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_types.BackColor = Color.FromArgb(192, 192, 255);
            lb_types.Location = new Point(348, 3);
            lb_types.Margin = new Padding(3, 3, 3, 0);
            lb_types.Name = "lb_types";
            lb_types.Padding = new Padding(4, 0, 0, 0);
            lb_types.Size = new Size(305, 20);
            lb_types.TabIndex = 11;
            lb_types.Text = "Card Types";
            lb_types.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lb_tiptexts
            // 
            lb_tiptexts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_tiptexts.BackColor = Color.FromArgb(192, 192, 255);
            lb_tiptexts.Location = new Point(348, 409);
            lb_tiptexts.Margin = new Padding(3, 3, 3, 0);
            lb_tiptexts.Name = "lb_tiptexts";
            lb_tiptexts.Padding = new Padding(4, 0, 0, 0);
            lb_tiptexts.Size = new Size(301, 20);
            lb_tiptexts.TabIndex = 11;
            lb_tiptexts.Text = "Tips Texts";
            lb_tiptexts.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // bgWorker1
            // 
            bgWorker1.WorkerReportsProgress = true;
            bgWorker1.WorkerSupportsCancellation = true;
            bgWorker1.DoWork += BgWorker1DoWork;
            bgWorker1.ProgressChanged += BgWorker1ProgressChanged;
            bgWorker1.RunWorkerCompleted += BgWorker1RunWorkerCompleted;
            // 
            // btn_undo
            // 
            btn_undo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_undo.Enabled = false;
            btn_undo.Location = new Point(836, 3);
            btn_undo.Name = "btn_undo";
            btn_undo.Size = new Size(75, 28);
            btn_undo.TabIndex = 5;
            btn_undo.Text = "&Undo";
            btn_undo.UseVisualStyleBackColor = true;
            btn_undo.Click += Btn_undoClick;
            // 
            // btn_img
            // 
            btn_img.Anchor = AnchorStyles.Top;
            btn_img.Location = new Point(505, 3);
            btn_img.Name = "btn_img";
            btn_img.Size = new Size(80, 28);
            btn_img.TabIndex = 17;
            btn_img.Text = "&Image";
            btn_img.UseVisualStyleBackColor = true;
            btn_img.Click += Btn_imgClick;
            // 
            // tb_setcode1
            // 
            tb_setcode1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_setcode1.Location = new Point(314, 143);
            tb_setcode1.MaxLength = 4;
            tb_setcode1.Name = "tb_setcode1";
            tb_setcode1.Size = new Size(30, 21);
            tb_setcode1.TabIndex = 111;
            tb_setcode1.Text = "0";
            tb_setcode1.TextAlign = HorizontalAlignment.Right;
            tb_setcode1.TextChanged += Tb_setcode1_TextChanged;
            // 
            // tb_setcode2
            // 
            tb_setcode2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_setcode2.Location = new Point(314, 169);
            tb_setcode2.MaxLength = 4;
            tb_setcode2.Name = "tb_setcode2";
            tb_setcode2.Size = new Size(30, 21);
            tb_setcode2.TabIndex = 112;
            tb_setcode2.Text = "0";
            tb_setcode2.TextAlign = HorizontalAlignment.Right;
            tb_setcode2.TextChanged += Tb_setcode2_TextChanged;
            // 
            // tb_setcode3
            // 
            tb_setcode3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_setcode3.Location = new Point(314, 195);
            tb_setcode3.MaxLength = 4;
            tb_setcode3.Name = "tb_setcode3";
            tb_setcode3.Size = new Size(30, 21);
            tb_setcode3.TabIndex = 113;
            tb_setcode3.Text = "0";
            tb_setcode3.TextAlign = HorizontalAlignment.Right;
            tb_setcode3.TextChanged += Tb_setcode3_TextChanged;
            // 
            // tb_setcode4
            // 
            tb_setcode4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_setcode4.Location = new Point(314, 221);
            tb_setcode4.MaxLength = 4;
            tb_setcode4.Name = "tb_setcode4";
            tb_setcode4.Size = new Size(30, 21);
            tb_setcode4.TabIndex = 114;
            tb_setcode4.Text = "0";
            tb_setcode4.TextAlign = HorizontalAlignment.Right;
            tb_setcode4.TextChanged += Tb_setcode4_TextChanged;
            // 
            // lb_cardcode
            // 
            lb_cardcode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_cardcode.AutoSize = true;
            lb_cardcode.Location = new Point(212, 335);
            lb_cardcode.Name = "lb_cardcode";
            lb_cardcode.Size = new Size(59, 12);
            lb_cardcode.TabIndex = 7;
            lb_cardcode.Text = "Card Code";
            // 
            // lb_markers
            // 
            lb_markers.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_markers.BackColor = Color.FromArgb(192, 192, 255);
            lb_markers.Location = new Point(203, 285);
            lb_markers.Margin = new Padding(3, 3, 3, 0);
            lb_markers.Name = "lb_markers";
            lb_markers.Padding = new Padding(4, 0, 0, 0);
            lb_markers.Size = new Size(81, 21);
            lb_markers.TabIndex = 19;
            lb_markers.Text = "Link Markers";
            lb_markers.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tb_link
            // 
            tb_link.HideSelection = false;
            tb_link.Location = new Point(265, 270);
            tb_link.MaxLength = 9;
            tb_link.Name = "tb_link";
            tb_link.Size = new Size(75, 21);
            tb_link.TabIndex = 21;
            tb_link.TextAlign = HorizontalAlignment.Right;
            tb_link.Visible = false;
            tb_link.WordWrap = false;
            tb_link.TextChanged += Tb_linkTextChanged;
            tb_link.KeyPress += Tb_linkKeyPress;
            // 
            // pl_bottom
            // 
            pl_bottom.Controls.Add(btn_PageDown);
            pl_bottom.Controls.Add(btn_PageUp);
            pl_bottom.Controls.Add(lb4);
            pl_bottom.Controls.Add(tb_page);
            pl_bottom.Controls.Add(tb_pagenum);
            pl_bottom.Controls.Add(btn_lua);
            pl_bottom.Controls.Add(btn_serach);
            pl_bottom.Controls.Add(btn_img);
            pl_bottom.Controls.Add(btn_reset);
            pl_bottom.Controls.Add(btn_del);
            pl_bottom.Controls.Add(btn_add);
            pl_bottom.Controls.Add(btn_mod);
            pl_bottom.Controls.Add(btn_undo);
            pl_bottom.Dock = DockStyle.Bottom;
            pl_bottom.Location = new Point(0, 583);
            pl_bottom.Name = "pl_bottom";
            pl_bottom.Size = new Size(991, 34);
            pl_bottom.TabIndex = 22;
            // 
            // pl_main
            // 
            pl_main.Controls.Add(splitContainer);
            pl_main.Dock = DockStyle.Fill;
            pl_main.Location = new Point(0, 24);
            pl_main.Name = "pl_main";
            pl_main.Size = new Size(991, 559);
            pl_main.TabIndex = 0;
            // 
            // splitContainer
            // 
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer.Location = new Point(3, 3);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(lv_cardlist);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(tb_support);
            splitContainer.Panel2.Controls.Add(pl_markers);
            splitContainer.Panel2.Controls.Add(pl_cardtype);
            splitContainer.Panel2.Controls.Add(tb_cardtext);
            splitContainer.Panel2.Controls.Add(tb_cardname);
            splitContainer.Panel2.Controls.Add(lb_tiptexts);
            splitContainer.Panel2.Controls.Add(cb_cardrule);
            splitContainer.Panel2.Controls.Add(pl_flags);
            splitContainer.Panel2.Controls.Add(pl_category);
            splitContainer.Panel2.Controls.Add(cb_setname4);
            splitContainer.Panel2.Controls.Add(tb_edittext);
            splitContainer.Panel2.Controls.Add(cb_cardrace);
            splitContainer.Panel2.Controls.Add(tb_setcode4);
            splitContainer.Panel2.Controls.Add(cb_setname2);
            splitContainer.Panel2.Controls.Add(lb_scripttext);
            splitContainer.Panel2.Controls.Add(cb_setname1);
            splitContainer.Panel2.Controls.Add(lb_flags);
            splitContainer.Panel2.Controls.Add(lb_categorys);
            splitContainer.Panel2.Controls.Add(cb_cardlevel);
            splitContainer.Panel2.Controls.Add(cb_setname3);
            splitContainer.Panel2.Controls.Add(lb_types);
            splitContainer.Panel2.Controls.Add(cb_cardattribute);
            splitContainer.Panel2.Controls.Add(lb_markers);
            splitContainer.Panel2.Controls.Add(tb_link);
            splitContainer.Panel2.Controls.Add(tb_setcode3);
            splitContainer.Panel2.Controls.Add(tb_def);
            splitContainer.Panel2.Controls.Add(tb_cardcode);
            splitContainer.Panel2.Controls.Add(tb_pright);
            splitContainer.Panel2.Controls.Add(tb_setcode2);
            splitContainer.Panel2.Controls.Add(tb_cardalias);
            splitContainer.Panel2.Controls.Add(tb_setcode1);
            splitContainer.Panel2.Controls.Add(tb_atk);
            splitContainer.Panel2.Controls.Add(lb_cardcode);
            splitContainer.Panel2.Controls.Add(lb_pleft_right);
            splitContainer.Panel2.Controls.Add(lb_name);
            splitContainer.Panel2.Controls.Add(lb_support);
            splitContainer.Panel2.Controls.Add(lb_cardalias);
            splitContainer.Panel2.Controls.Add(lb2);
            splitContainer.Panel2.Controls.Add(pl_image);
            splitContainer.Panel2.Controls.Add(lb_atkdef);
            splitContainer.Panel2.Controls.Add(tb_pleft);
            splitContainer.Panel2.Controls.Add(lb5);
            splitContainer.Size = new Size(984, 556);
            splitContainer.SplitterDistance = 328;
            splitContainer.TabIndex = 22;
            // 
            // lv_cardlist
            // 
            lv_cardlist.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lv_cardlist.BorderStyle = BorderStyle.FixedSingle;
            lv_cardlist.Columns.AddRange(new ColumnHeader[] { ch_cardcode, ch_cardname });
            lv_cardlist.FullRowSelect = true;
            lv_cardlist.GridLines = true;
            lv_cardlist.LabelWrap = false;
            lv_cardlist.Location = new Point(3, 3);
            lv_cardlist.Name = "lv_cardlist";
            lv_cardlist.Scrollable = false;
            lv_cardlist.ShowItemToolTips = true;
            lv_cardlist.Size = new Size(322, 551);
            lv_cardlist.TabIndex = 1;
            lv_cardlist.UseCompatibleStateImageBehavior = false;
            lv_cardlist.View = View.Details;
            lv_cardlist.SelectedIndexChanged += Lv_cardlistSelectedIndexChanged;
            lv_cardlist.KeyDown += Lv_cardlistKeyDown;
            // 
            // ch_cardcode
            // 
            ch_cardcode.Text = "Card Code";
            ch_cardcode.Width = 70;
            // 
            // ch_cardname
            // 
            ch_cardname.Text = "Card Name";
            ch_cardname.Width = 1294;
            // 
            // tb_support
            // 
            tb_support.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_support.Location = new Point(195, 260);
            tb_support.MaxLength = 20;
            tb_support.Name = "tb_support";
            tb_support.Size = new Size(81, 21);
            tb_support.TabIndex = 114;
            tb_support.Text = "0";
            tb_support.TextAlign = HorizontalAlignment.Right;
            tb_support.TextChanged += Tb_setcode4_TextChanged;
            // 
            // pl_markers
            // 
            pl_markers.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pl_markers.AutoScroll = true;
            pl_markers.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            pl_markers.Location = new Point(285, 244);
            pl_markers.Margin = new Padding(1, 2, 1, 2);
            pl_markers.Name = "pl_markers";
            pl_markers.Padding = new Padding(2);
            pl_markers.Size = new Size(63, 60);
            pl_markers.TabIndex = 20;
            // 
            // pl_cardtype
            // 
            pl_cardtype.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pl_cardtype.AutoScroll = true;
            pl_cardtype.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            pl_cardtype.Location = new Point(348, 25);
            pl_cardtype.Margin = new Padding(1, 2, 1, 2);
            pl_cardtype.MaximumSize = new Size(360, 120);
            pl_cardtype.Name = "pl_cardtype";
            pl_cardtype.Padding = new Padding(2);
            pl_cardtype.Size = new Size(301, 120);
            pl_cardtype.TabIndex = 12;
            pl_cardtype.Leave += TextToPendulum;
            // 
            // pl_flags
            // 
            pl_flags.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pl_flags.AutoScroll = true;
            pl_flags.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            pl_flags.Location = new Point(348, 300);
            pl_flags.Margin = new Padding(1, 2, 1, 2);
            pl_flags.MaximumSize = new Size(360, 175);
            pl_flags.Name = "pl_flags";
            pl_flags.Padding = new Padding(2);
            pl_flags.Size = new Size(301, 105);
            pl_flags.TabIndex = 13;
            pl_flags.MouseWheel += Pl_flagsScroll;
            // 
            // pl_category
            // 
            pl_category.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pl_category.AutoScroll = true;
            pl_category.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            pl_category.Location = new Point(348, 169);
            pl_category.Margin = new Padding(1, 2, 1, 2);
            pl_category.MaximumSize = new Size(360, 175);
            pl_category.Name = "pl_category";
            pl_category.Padding = new Padding(2);
            pl_category.Size = new Size(301, 105);
            pl_category.TabIndex = 13;
            pl_category.MouseWheel += Pl_categoryScroll;
            // 
            // lb_scripttext
            // 
            lb_scripttext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lb_scripttext.BorderStyle = BorderStyle.None;
            lb_scripttext.Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lb_scripttext.FormattingEnabled = true;
            lb_scripttext.IntegralHeight = false;
            lb_scripttext.ItemHeight = 12;
            lb_scripttext.Location = new Point(348, 432);
            lb_scripttext.Name = "lb_scripttext";
            lb_scripttext.ScrollAlwaysVisible = true;
            lb_scripttext.Size = new Size(301, 86);
            lb_scripttext.TabIndex = 6;
            lb_scripttext.SelectedIndexChanged += Lb_scripttextSelectedIndexChanged;
            // 
            // lb_flags
            // 
            lb_flags.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lb_flags.BackColor = Color.FromArgb(192, 192, 255);
            lb_flags.Location = new Point(348, 277);
            lb_flags.Margin = new Padding(3, 3, 3, 0);
            lb_flags.Name = "lb_flags";
            lb_flags.Padding = new Padding(4, 0, 0, 0);
            lb_flags.Size = new Size(303, 20);
            lb_flags.TabIndex = 11;
            lb_flags.Text = "Card Flags";
            lb_flags.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lb_name
            // 
            lb_name.AutoSize = true;
            lb_name.Location = new Point(2, 3);
            lb_name.Margin = new Padding(3);
            lb_name.Name = "lb_name";
            lb_name.Size = new Size(29, 12);
            lb_name.TabIndex = 7;
            lb_name.Text = "Name";
            // 
            // lb_support
            // 
            lb_support.AutoSize = true;
            lb_support.Location = new Point(195, 245);
            lb_support.Margin = new Padding(3);
            lb_support.Name = "lb_support";
            lb_support.Size = new Size(47, 12);
            lb_support.TabIndex = 7;
            lb_support.Text = "Support";
            // 
            // DataEditForm
            // 
            AllowDrop = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(991, 617);
            Controls.Add(pl_main);
            Controls.Add(pl_bottom);
            Controls.Add(mainMenu);
            Font = new Font("SimSun", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = mainMenu;
            MaximizeBox = false;
            Name = "DataEditForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DataEditorX";
            FormClosing += DataEditFormFormClosing;
            Load += DataEditFormLoad;
            SizeChanged += DataEditFormSizeChanged;
            DragDrop += OnDragDrop;
            DragEnter += OnDragEnter;
            Enter += DataEditFormEnter;
            KeyDown += DataEditForm_KeyDown;
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            pl_bottom.ResumeLayout(false);
            pl_bottom.PerformLayout();
            pl_main.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem menuitem_testpendulumtext;
        private System.Windows.Forms.ToolStripMenuItem menuitem_exportMSEimage;
        private System.Windows.Forms.ToolStripMenuItem menuitem_exportdata;
        private System.Windows.Forms.ToolStripMenuItem menu_data;
        private System.Windows.Forms.ToolStripMenuItem menuitem_cancelTask;
        private System.Windows.Forms.ToolStripSeparator tsep1;
        private System.Windows.Forms.TextBox tb_setcode1;
        private System.Windows.Forms.TextBox tb_setcode2;
        private System.Windows.Forms.TextBox tb_setcode3;
        private System.Windows.Forms.TextBox tb_setcode4;
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
        private System.Windows.Forms.TextBox tb_support;
        private System.Windows.Forms.Label lb_support;
        private System.Windows.Forms.Label lb_name;
        private ToolStripMenuItem menuitem_addarchetype;
    }
}

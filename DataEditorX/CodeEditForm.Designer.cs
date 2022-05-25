/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-22
 * 时间: 19:16
 * 
 */
namespace DataEditorX
{
	partial class CodeEditForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeEditForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuitem_setting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_showmap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_showinput = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_save2database = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_find = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_replace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_setcard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_tooltipFont = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_tools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_testlua = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_fixCardCode = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.fctb = new FastColoredTextBoxNS.FastColoredTextBoxEx();
            this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_setting,
            this.menuitem_help,
            this.menuitem_tools});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(705, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // menuitem_setting
            // 
            this.menuitem_setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_showmap,
            this.menuitem_showinput,
            this.menuitem_save2database,
            this.menuitem_find,
            this.menuitem_replace,
            this.menuitem_setcard,
            this.menuitem_tooltipFont});
            this.menuitem_setting.Name = "menuitem_setting";
            this.menuitem_setting.Size = new System.Drawing.Size(75, 20);
            this.menuitem_setting.Text = "Settings(&S)";
            // 
            // menuitem_showmap
            // 
            this.menuitem_showmap.Name = "menuitem_showmap";
            this.menuitem_showmap.Size = new System.Drawing.Size(184, 22);
            this.menuitem_showmap.Text = "Show/Hide Map";
            this.menuitem_showmap.Click += new System.EventHandler(this.ShowMapToolStripMenuItemClick);
            // 
            // menuitem_showinput
            // 
            this.menuitem_showinput.Checked = true;
            this.menuitem_showinput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuitem_showinput.Name = "menuitem_showinput";
            this.menuitem_showinput.Size = new System.Drawing.Size(184, 22);
            this.menuitem_showinput.Text = "Show/Hide InputBox";
            this.menuitem_showinput.Click += new System.EventHandler(this.Menuitem_showinputClick);
            // 
            // menuitem_save2database
            // 
            this.menuitem_save2database.Checked = true;
            this.menuitem_save2database.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuitem_save2database.Name = "menuitem_save2database";
            this.menuitem_save2database.Size = new System.Drawing.Size(184, 22);
            this.menuitem_save2database.Text = "Save to Database";
            this.menuitem_save2database.Click += new System.EventHandler(this.Menuitem_save2database_Click);
            // 
            // menuitem_find
            // 
            this.menuitem_find.Name = "menuitem_find";
            this.menuitem_find.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuitem_find.Size = new System.Drawing.Size(184, 22);
            this.menuitem_find.Text = "Find";
            this.menuitem_find.Click += new System.EventHandler(this.Menuitem_findClick);
            // 
            // menuitem_replace
            // 
            this.menuitem_replace.Name = "menuitem_replace";
            this.menuitem_replace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuitem_replace.Size = new System.Drawing.Size(184, 22);
            this.menuitem_replace.Text = "Replace";
            this.menuitem_replace.Click += new System.EventHandler(this.Menuitem_replaceClick);
            // 
            // menuitem_setcard
            // 
            this.menuitem_setcard.Name = "menuitem_setcard";
            this.menuitem_setcard.Size = new System.Drawing.Size(184, 22);
            this.menuitem_setcard.Text = "Set Cards";
            // 
            // menuitem_tooltipFont
            // 
            this.menuitem_tooltipFont.Name = "menuitem_tooltipFont";
            this.menuitem_tooltipFont.Size = new System.Drawing.Size(184, 22);
            this.menuitem_tooltipFont.Text = "Set Tooltip Font";
            this.menuitem_tooltipFont.Click += new System.EventHandler(this.Menuitem_tooltipFont_Click);
            // 
            // menuitem_help
            // 
            this.menuitem_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_about});
            this.menuitem_help.Name = "menuitem_help";
            this.menuitem_help.Size = new System.Drawing.Size(61, 20);
            this.menuitem_help.Text = "Help(&H)";
            // 
            // menuitem_about
            // 
            this.menuitem_about.Name = "menuitem_about";
            this.menuitem_about.Size = new System.Drawing.Size(107, 22);
            this.menuitem_about.Text = "About";
            this.menuitem_about.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // menuitem_tools
            // 
            this.menuitem_tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitem_testlua,
            this.menuitem_fixCardCode});
            this.menuitem_tools.Name = "menuitem_tools";
            this.menuitem_tools.Size = new System.Drawing.Size(60, 20);
            this.menuitem_tools.Text = "Tools(&T)";
            // 
            // menuitem_testlua
            // 
            this.menuitem_testlua.Name = "menuitem_testlua";
            this.menuitem_testlua.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuitem_testlua.Size = new System.Drawing.Size(164, 22);
            this.menuitem_testlua.Text = "Syntax Check";
            this.menuitem_testlua.Click += new System.EventHandler(this.Menuitem_testlua_Click);
            // 
            // menuitem_fixCardCode
            // 
            this.menuitem_fixCardCode.Name = "menuitem_fixCardCode";
            this.menuitem_fixCardCode.Size = new System.Drawing.Size(164, 22);
            this.menuitem_fixCardCode.Text = "Fix card code";
            this.menuitem_fixCardCode.Click += new System.EventHandler(this.Menuitem_fixCardCode_Click);
            // 
            // tb_input
            // 
            this.tb_input.BackColor = System.Drawing.SystemColors.Control;
            this.tb_input.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tb_input.Location = new System.Drawing.Point(0, 394);
            this.tb_input.Margin = new System.Windows.Forms.Padding(0);
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(514, 21);
            this.tb_input.TabIndex = 1;
            this.tb_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_inputKeyDown);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // fctb
            // 
            this.fctb.AutoCompleteBrackets = true;
            this.fctb.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fctb.AutoIndent = false;
            this.fctb.AutoIndentChars = false;
            this.fctb.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>.+)";
            this.fctb.AutoIndentExistingLines = false;
            this.fctb.AutoScrollMinSize = new System.Drawing.Size(0, 22);
            this.fctb.BackBrush = null;
            this.fctb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fctb.CharHeight = 22;
            this.fctb.CharWidth = 10;
            this.fctb.CommentPrefix = "--";
            this.fctb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctb.DelayedEventsInterval = 1;
            this.fctb.DelayedTextChangedInterval = 1;
            this.fctb.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctb.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fctb.ForeColor = System.Drawing.Color.GhostWhite;
            this.fctb.Hotkeys = resources.GetString("fctb.Hotkeys");
            this.fctb.IndentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fctb.IsReplaceMode = false;
            this.fctb.Language = FastColoredTextBoxNS.Language.Lua;
            this.fctb.LeftBracket = '(';
            this.fctb.LeftBracket2 = '{';
            this.fctb.LineNumberColor = System.Drawing.Color.WhiteSmoke;
            this.fctb.Location = new System.Drawing.Point(0, 24);
            this.fctb.Margin = new System.Windows.Forms.Padding(0);
            this.fctb.Name = "fctb";
            this.fctb.Paddings = new System.Windows.Forms.Padding(0);
            this.fctb.RightBracket = ')';
            this.fctb.RightBracket2 = '}';
            this.fctb.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctb.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fctb.ServiceColors")));
            this.fctb.Size = new System.Drawing.Size(514, 370);
            this.fctb.TabIndex = 0;
            this.fctb.ToolTipDelay = 1;
            this.fctb.WordWrap = true;
            this.fctb.Zoom = 100;
            this.fctb.ToolTipNeeded += new System.EventHandler<FastColoredTextBoxNS.ToolTipNeededEventArgs>(this.FctbToolTipNeeded);
            this.fctb.SelectionChangedDelayed += new System.EventHandler(this.FctbSelectionChangedDelayed);
            this.fctb.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.fctb.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.fctb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FctbMouseClick);
            // 
            // documentMap1
            // 
            this.documentMap1.BackColor = System.Drawing.Color.DimGray;
            this.documentMap1.Dock = System.Windows.Forms.DockStyle.Right;
            this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap1.Location = new System.Drawing.Point(514, 24);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.Size = new System.Drawing.Size(191, 391);
            this.documentMap1.TabIndex = 5;
            this.documentMap1.Target = this.fctb;
            this.documentMap1.Text = "documentMap1";
            this.documentMap1.Visible = false;
            // 
            // CodeEditForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(705, 415);
            this.Controls.Add(this.fctb);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.documentMap1);
            this.Controls.Add(this.mainMenu);
            this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "CodeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "CodeEditor";
            this.Text = "CodeEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CodeEditFormFormClosing);
            this.Load += new System.EventHandler(this.CodeEditFormLoad);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.Enter += new System.EventHandler(this.CodeEditFormEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_inputKeyDown);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fctb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripMenuItem menuitem_setcard;
		private System.Windows.Forms.ToolStripMenuItem menuitem_replace;
		private System.Windows.Forms.ToolStripMenuItem menuitem_find;
		private System.Windows.Forms.ToolStripMenuItem menuitem_showinput;
		private System.Windows.Forms.TextBox tb_input;
		private FastColoredTextBoxNS.DocumentMap documentMap1;
		private FastColoredTextBoxNS.FastColoredTextBoxEx fctb;
		private System.Windows.Forms.ToolStripMenuItem menuitem_showmap;
		private System.Windows.Forms.ToolStripMenuItem menuitem_about;
		private System.Windows.Forms.ToolStripMenuItem menuitem_help;
		private System.Windows.Forms.ToolStripMenuItem menuitem_setting;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem menuitem_tools;
        private System.Windows.Forms.ToolStripMenuItem menuitem_testlua;
        private System.Windows.Forms.ToolStripMenuItem menuitem_fixCardCode;
        private System.Windows.Forms.ToolStripMenuItem menuitem_tooltipFont;
        private ToolStripMenuItem menuitem_save2database;
    }
}

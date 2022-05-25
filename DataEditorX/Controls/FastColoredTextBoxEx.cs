/*
 * 由SharpDevelop创建。
 * 用户： Acer
 * 日期: 2014-10-24
 * 时间: 7:19
 * 
 */

namespace FastColoredTextBoxNS
{
    public class FastColoredTextBoxEx : FastColoredTextBox
    {
        public Label lbTooltip;
        private Label lbSizeController;
        Point lastMouseCoord;
        public FastColoredTextBoxEx() : base()
        {
            SyntaxHighlighter = new MySyntaxHighlighter(this);
            TextChanged += FctbTextChanged;
            ToolTipDelay = 1;
            DelayedEventsInterval = 1;
            DelayedTextChangedInterval = 1;
            Selection.ColumnSelectionMode = true;
            InitializeComponent();
        }
        public new event EventHandler<ToolTipNeededEventArgs> ToolTipNeeded;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            lastMouseCoord = e.Location;
        }
        //函数悬停提示
        protected override void OnToolTip()
        {
            if (ToolTip == null)
            {
                return;
            }

            if (ToolTipNeeded == null)
            {
                return;
            }

            //get place under mouse
            Place place = PointToPlace(lastMouseCoord);

            //check distance
            Point p = PlaceToPoint(place);
            if (Math.Abs(p.X - lastMouseCoord.X) > CharWidth * 2 ||
                Math.Abs(p.Y - lastMouseCoord.Y) > CharHeight * 2)
            {
                return;
            }
            //get word under mouse
            var r = new Range(this, place, place);
            string hoveredWord = r.GetFragment("[a-zA-Z0-9_]").Text;
            //event handler
            var ea = new ToolTipNeededEventArgs(place, hoveredWord);
            ToolTipNeeded(this, ea);

            if (ea.ToolTipText != null)
            {
                ShowTooltipWithLabel(ea.ToolTipTitle, ea.ToolTipText);
            }
        }
        public void ShowTooltipWithLabel(AutocompleteItem item)
        {
            ShowTooltipWithLabel(item.ToolTipTitle, item.ToolTipText);
        }
        public void ShowTooltipWithLabel(string title, string text, int height)
        {
            lbTooltip.Visible = true;
            lbTooltip.Text = $"{title}\r\n\r\n{text}";
            lbTooltip.Location = new Point(Size.Width - 500, height);
        }

        public void ShowTooltipWithLabel(string title, string text)
        {
            ShowTooltipWithLabel(title, text, lastMouseCoord.Y + CharHeight);
        }

        //高亮当前词
        void FctbTextChanged(object sender, TextChangedEventArgs e)
        {
            //delete all markers
            Range.ClearFoldingMarkers();

            var currentIndent = 0;
            var lastNonEmptyLine = 0;

            for (int i = 0; i < LinesCount; i++)
            {
                var line = this[i];
                var spacesCount = line.StartSpacesCount;
                if (spacesCount == line.Count) //empty line
                {
                    continue;
                }

                if (currentIndent < spacesCount)
                {
                    //append start folding marker
                    this[lastNonEmptyLine].FoldingStartMarker = "m" + currentIndent;
                }
                else if (currentIndent > spacesCount)
                {
                    //append end folding marker
                    this[lastNonEmptyLine].FoldingEndMarker = "m" + spacesCount;
                }

                currentIndent = spacesCount;
                lastNonEmptyLine = i;
            }
        }

        private void InitializeComponent()
        {
            lbTooltip = new System.Windows.Forms.Label();
            lbSizeController = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            SuspendLayout();
            // 
            // lbTooltip
            // 
            lbTooltip.AutoSize = true;
            lbTooltip.BackColor = SystemColors.Desktop;
            lbTooltip.Font = new System.Drawing.Font("微软雅黑", 15.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lbTooltip.ForeColor = SystemColors.Control;
            lbTooltip.Location = new System.Drawing.Point(221, 117);
            lbTooltip.MaximumSize = new System.Drawing.Size(480, 0);
            lbTooltip.Name = "lbTooltip";
            lbTooltip.Size = new System.Drawing.Size(0, 28);
            lbTooltip.TabIndex = 1;
            lbTooltip.MouseMove += new System.Windows.Forms.MouseEventHandler(lbTooltip_MouseMove);
            // 
            // lbSizeController
            // 
            lbSizeController.AutoSize = true;
            lbSizeController.BackColor = Color.Transparent;
            lbSizeController.ForeColor = Color.Transparent;
            lbSizeController.Location = new System.Drawing.Point(179, 293);
            lbSizeController.Name = "lbSizeController";
            lbSizeController.Size = new System.Drawing.Size(136, 16);
            lbSizeController.TabIndex = 2;
            lbSizeController.Text = "lbSizeController";
            // 
            // FastColoredTextBoxEx
            // 
            AutoScrollMinSize = new System.Drawing.Size(27, 14);
            BackColor = SystemColors.Control;
            Controls.Add(lbSizeController);
            Controls.Add(lbTooltip);
            Name = "FastColoredTextBoxEx";
            Size = new System.Drawing.Size(584, 327);
            Load += new System.EventHandler(FastColoredTextBoxEx_Load);
            Scroll += new System.Windows.Forms.ScrollEventHandler(FastColoredTextBoxEx_Scroll);
            SizeChanged += new System.EventHandler(FastColoredTextBoxEx_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void FastColoredTextBoxEx_Load(object sender, EventArgs e)
        {

        }

        private void lbTooltip_MouseMove(object sender, MouseEventArgs e)
        {
            lbTooltip.Visible = false;
        }
        private void ResizeWindow()
        {
            lbSizeController.Location = new Point(0, Height);
            lbSizeController.Text = "\r\n\r\n";
        }
        private void FastColoredTextBoxEx_SizeChanged(object sender, EventArgs e)
        {
            lbTooltip.Visible = false;
            ResizeWindow();
        }

        private void FastColoredTextBoxEx_Scroll(object sender, ScrollEventArgs e)
        {
            ResizeWindow();
        }
    }
}

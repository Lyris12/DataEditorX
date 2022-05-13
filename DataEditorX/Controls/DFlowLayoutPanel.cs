namespace DataEditorX
{
    public class DFlowLayoutPanel : FlowLayoutPanel
    {
        public DFlowLayoutPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint,
                     true);
            UpdateStyles();
        }
    }
}

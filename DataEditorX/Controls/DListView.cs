namespace DataEditorX
{
    public class DListView : ListView
    {
        public DListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint,
                     true);
            UpdateStyles();
        }
    }
}

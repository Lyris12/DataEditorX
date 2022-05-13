namespace DataEditorX
{
    public class DListBox : ListBox
    {
        public DListBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
         ControlStyles.AllPaintingInWmPaint,
         true);
            UpdateStyles();
        }
    }
}

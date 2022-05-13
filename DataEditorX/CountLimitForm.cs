namespace DataEditorX
{
    public partial class CountLimitForm : Form
    {
        public EffectCountLimit CountLimit;
        public CountLimitForm(EffectCountLimit ecl)
        {
            InitializeComponent();
            CountLimit = ecl;
            checkIsOath.Checked = ecl.IsOath;
            checkIsInDuel.Checked = ecl.IsInDuel;
            checkIsHasCode.Checked = ecl.IsHasCode;
            checkIsSingle.Checked = ecl.IsSingle;
            numCount.Value = ecl.Count;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CountLimit.IsOath = checkIsOath.Checked;
            CountLimit.IsInDuel = checkIsInDuel.Checked;
            CountLimit.IsHasCode = checkIsHasCode.Checked;
            CountLimit.IsSingle = checkIsSingle.Checked;
            CountLimit.Count = numCount.Value;
        }
    }
    public class EffectCountLimit
    {
        public bool IsOath = false;
        public bool IsInDuel = false;
        public bool IsHasCode = false;
        public bool IsSingle = false;
        public decimal Code = 1000;
        public decimal Offset = 0;
        public decimal Count = 1;
        public EffectCountLimit(decimal code)
        {
            Code = code;
            Offset = 0;
        }
        public EffectCountLimit(decimal code, decimal offset)
        {
            Code = code;
            Offset = offset;
        }
    }
}

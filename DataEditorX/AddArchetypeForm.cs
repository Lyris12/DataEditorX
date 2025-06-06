﻿namespace DataEditorX
{
    public partial class AddArchetypeForm : Form
    {
        public string name;
        public int code;
        public Dictionary<long, string> setcodes;
        public AddArchetypeForm(Dictionary<long, string> dic)
        {
            InitializeComponent();
            setcodes = dic;
        }
        private void Btn_confirm_Click(object sender, EventArgs e)
        {
            name = tb_archename.Text;
            Random rng = new((int)DateTime.Now.Ticks);
            code = rng.Next(0x640, 0xfff);
            while(setcodes.TryGetValue(code, out _)) code = rng.Next(0x640, 0xfff);
        }
        private void AddArchetypeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
            case Keys.Enter:
                Btn_confirm_Click(sender, e);
                Close();
                break;
            case Keys.Escape:
                Close();
                break;
            }
        }
    }
}

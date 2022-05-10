using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DataEditorX
{
    public partial class AddArchetypeForm : Form
    {
        public AddArchetypeForm(Dictionary<long, string> dic)
        {
            InitializeComponent();
            setcodes = dic;
        }
        public string name;
        public int code;
        public Dictionary<long, string> setcodes;
        private void Btn_confirm_Click(object sender, EventArgs e)
        {
            name = tb_archename.Text;
            code = 0x640;
            while (setcodes.TryGetValue(code, out _)) code++;
        }
        private void AddArchetypeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Btn_confirm_Click(sender, e);
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}

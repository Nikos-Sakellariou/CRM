using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace CRMapp
{
    public partial class AlertView : UserControl
    {
        public AlertView(string Header,string Alert, Color color)
        {
            InitializeComponent();
            AlertTxt.Text = Alert;
            HeaderTxt.Text = Header;
            AlertTxt.BackColor = color;
            HeaderTxt.BackColor = color;
        }

        public void AppendText(string s)
        {
            int ss = AlertTxt.TextLength;
            AlertTxt.AppendText(s);
            int sl = AlertTxt.SelectionStart - ss + 1;

            Font bold = new Font(AlertTxt.Font, FontStyle.Regular);
            AlertTxt.Select(ss, sl);
            AlertTxt.SelectionFont = bold;
        }

        public void AppendBoldText(string s)
        {
            int ss = AlertTxt.TextLength;
            AlertTxt.AppendText(s);
            int sl = AlertTxt.SelectionStart - ss + 1;

            Font bold = new Font(AlertTxt.Font, FontStyle.Bold);
            AlertTxt.Select(ss, sl);
            AlertTxt.SelectionFont = bold;
        }

        private void HeaderTxt_Enter(object sender, EventArgs e)
        {
            ActiveControl = NotesPanel;
        }

        private void AlertTxt_Enter(object sender, EventArgs e)
        {
            ActiveControl = NotesPanel;
        }
    }
}

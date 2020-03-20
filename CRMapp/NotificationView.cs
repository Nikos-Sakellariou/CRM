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
    public partial class NotificationView : UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();

        public NotificationView(string ID)
        {
            con = new Checks().Initiallize_con();
            InitializeComponent();
            FillData(ID);
            if (RepeatTxt.Text=="0")
            {
                CheckBtn.Visible = false;
                DeleteBtn.Height = ControlsPanel.Height / 2;
                DeleteBtn.Location = new Point(0, ControlsPanel.Height/4);
            }
            else
            {
                CheckBtn.Height = ControlsPanel.Height / 2;
                CheckBtn.Location = new Point(0, 0);
                DeleteBtn.Height = ControlsPanel.Height / 2;
                DeleteBtn.Location = new Point(0, ControlsPanel.Height / 2);
            }
        }

        private void FillData(string id)
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select Id,Note,Date,Repeat from Notes where Id=@id";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", id);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count==1)
                        {
                            IdTxt.Text = dt.Rows[0][0].ToString();
                            NoteTxt.Text = dt.Rows[0][1].ToString();
                            DateTime date = DateTime.Parse(dt.Rows[0][2].ToString());
                            DateTxt.Text = date.Day + " / " + date.Month + " / " + date.Year;
                            Date2Txt.Text = dt.Rows[0][2].ToString();
                            RepeatTxt.Text = dt.Rows[0][3].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση σημείωσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }
        
        private void CheckCancelBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        SqlCommand UpdCmd = sqlcon.CreateCommand();
                        UpdCmd.Connection = sqlcon;
                        if (RepeatTxt.Text == "1")
                        {
                            UpdCmd.CommandText = "update Notes set Date=DATEADD(month,1,Date) where Id=@id";
                            UpdCmd.Parameters.AddWithValue("@id", IdTxt.Text);
                        }
                        else
                        {
                            UpdCmd.CommandText = "delete Notes where Id=@id";
                            UpdCmd.Parameters.AddWithValue("@id", IdTxt.Text);
                        }
                        UpdCmd.ExecuteNonQuery();
                        this.Dispose();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ενημέρωση της σημείωσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }

        private void NoteTxt_Enter(object sender, EventArgs e)
        {
            ActiveControl = ControlsPanel;
        }

        private void DateTxt_Enter(object sender, EventArgs e)
        {
            ActiveControl = ControlsPanel;
        }
    }
}

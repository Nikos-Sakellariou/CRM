using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class AddNoteForm : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();

        public AddNoteForm()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            NoteDateTimePicker.Value = System.DateTime.Today;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NoteBox.Text=="")
            {
                MessageBox.Show("Θα πρέπει να συμπληρώσετε μία σημείωση.");
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlCommand InsCmd1 = sqlcon.CreateCommand();
                        InsCmd1.Connection = sqlcon;
                        try
                        {
                            InsCmd1.CommandText = "insert into Notes(Id, Note, Date, Repeat) values((select dbo.nvl(Max(Id)+1,1) from dbo.Notes), @note, @date, @repeat)";
                            InsCmd1.Parameters.AddWithValue("@note", NoteBox.Text);
                            InsCmd1.Parameters.AddWithValue("@date", NoteDateTimePicker.Value);
                            InsCmd1.Parameters.AddWithValue("@repeat", ((RepeatBox.Checked == false) ? 0 : 1));
                            InsCmd1.ExecuteNonQuery();
                            MessageBox.Show("Η σημείωση προστέθηκε με επιτυχία.");
                            NoteBox.Text = "";
                            NoteDateTimePicker.Value = System.DateTime.Today;
                            RepeatBox.Checked = false;
                        }
                        catch (Exception ex)
                        {
                           
                            MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση της σημείωσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");

                        }
                        sqlcon.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                }
            }
        }
        
    }
}

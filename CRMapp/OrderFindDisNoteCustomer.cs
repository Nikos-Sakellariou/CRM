using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class OrderFindDisNoteCustomer : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string, string> dc = new Dictionary<string, string>();

        public OrderFindDisNoteCustomer(string CustomerId)
        {
            con = new Checks().Initiallize_con();
            InitializeComponent();
            GetData(CustomerId);
        }

        public List<string> ReturnDisNoteIds()
        {
            return ls;
        }

        private void GetData(string CustomerId)
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select DisNoteId,DisNoteSeries,DisNoteDate,Id from CustomerDisNote where CustomerId=@supid and Invoice='0' order by DisNoteDate";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"supid", CustomerId);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DateTime dat = Convert.ToDateTime(dt.Rows[i][2].ToString());
                            DisNoteLbx.Items.Add(dt.Rows[i][0].ToString()+" "+((dt.Rows[i][1].ToString()=="") ?"" : "/ "+(dt.Rows[i][1].ToString()))+"      - "+ dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year);
                            dc.Add(i.ToString(),dt.Rows[i][3].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }

        private void RetrieveBtn_Click(object sender, EventArgs e)
        {
            if (DisNoteLbx.SelectedItems.Count==0)
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον Δελτίο Αποστολής του πελάτη.");
            }
            else
            {
                foreach (var item in DisNoteLbx.SelectedItems)
                {
                    string disnoteid;
                    dc.TryGetValue(DisNoteLbx.Items.IndexOf(item).ToString(), out disnoteid);
                    ls.Add(disnoteid);
                }
                this.Close();
            }
        }

    }
}

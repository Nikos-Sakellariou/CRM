using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class OrderFindDisNoteReturnSupplier : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        string ls2 = "";
        Dictionary<string, string> dc = new Dictionary<string, string>();
        Dictionary<string, string> dc2 = new Dictionary<string, string>();

        public OrderFindDisNoteReturnSupplier(string SupplierId)
        {
            con = new Checks().Initiallize_con();
            InitializeComponent();
            GetData(SupplierId);
        }

        public List<string> ReturnDisNoteIds()
        {
            return ls;
        }

        public string ReturnDisNotes()
        {
            return ls2;
        }

        private void GetData(string SupplierId)
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select DisNoteId,DisNoteSeries,DisNoteDate,Id from SupplierReturnDisNote where SupplierId=@supid and DebitInvoice='0' order by DisNoteDate";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"supid", SupplierId);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DateTime dat = Convert.ToDateTime(dt.Rows[i][2].ToString());
                            DisNoteLbx.Items.Add(dt.Rows[i][0].ToString()+" "+((dt.Rows[i][1].ToString()=="") ?"" : "/ "+(dt.Rows[i][1].ToString()))+"      - "+ dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year);
                            dc.Add(i.ToString(), dt.Rows[i][3].ToString());
                            dc2.Add(i.ToString(), dt.Rows[i][0].ToString() + "/" + dt.Rows[i][1].ToString());
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
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον Δελτίο Αποστολής του προμηθευτή.");
            }
            else
            {
                int i = 0;
                foreach (var item in DisNoteLbx.SelectedItems)
                {
                    string disnoteid;
                    dc.TryGetValue(DisNoteLbx.Items.IndexOf(item).ToString(), out disnoteid);
                    ls.Add(disnoteid);
                    string disnotes;
                    dc2.TryGetValue(DisNoteLbx.Items.IndexOf(item).ToString(), out disnotes);
                    if (i>0)
                    {
                        ls2 += ", "+disnotes;
                    }
                    else
                    {
                        ls2 += disnotes;
                    }
                    i++;
                }
                this.Close();
            }
        }

    }
}

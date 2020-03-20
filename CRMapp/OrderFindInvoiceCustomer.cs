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
    public partial class OrderFindInvoiceCustomer : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string, string> dc = new Dictionary<string, string>();

        public OrderFindInvoiceCustomer(string CustomerId)
        {
            con = new Checks().Initiallize_con();
            InitializeComponent();
            GetData(CustomerId);
        }

        public List<string> ReturnInvoiceIds()
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
                        string query = "select InvoiceId,InvoiceSeries,InvoiceDate,Id from CustomerInvoice where CustomerId=@custid order by InvoiceDate";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"custid", CustomerId);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; (i < dt.Rows.Count && i<21); i++)
                        {
                            DateTime dat = Convert.ToDateTime(dt.Rows[i][2].ToString());
                            InvoiceLbx.Items.Add(dt.Rows[i][0].ToString()+" "+((dt.Rows[i][1].ToString()=="") ?"" : "/ "+(dt.Rows[i][1].ToString()))+"      - "+ dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year);
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
            if (InvoiceLbx.SelectedItems.Count==0)
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον Τιμολόγιο του πελάτη.");
            }
            else
            {
                foreach (var item in InvoiceLbx.SelectedItems)
                {
                    string disnoteid;
                    dc.TryGetValue(InvoiceLbx.Items.IndexOf(item).ToString(), out disnoteid);
                    ls.Add(disnoteid);
                }
                this.Close();
            }
        }

    }
}

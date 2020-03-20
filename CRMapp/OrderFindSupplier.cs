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
    public partial class OrderFindSupplier : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        private string SupplierId;
        private string SupplierName;
        private string SupplierAfm;
        private string SupplierDebit;
        private string SupplierOccupation;
        private string SupplierAddress;
        private string SupplierTk;
        private string SupplierTax_office;
        private string SupplierPhone;
        private string SupplierPhone2;
        private string SupplierRegion;
        private string SupplierManager;
        private string SupplierEmail;

        public OrderFindSupplier()
        {
            con = new Checks().Initiallize_con();
            InitializeComponent();
            GetDataNameAfm();
        }


        private void GetDataNameAfm()
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select Name from Suppliers order by Name";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        SelectNameCmb.Items.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SelectNameCmb.Items.Add(dt.Rows[i][0].ToString());
                            ls.Add(dt.Rows[i][0].ToString());
                        }
                        string query2 = "select Afm from Suppliers order by Afm";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            SearchAfmTxt.AutoCompleteCustomSource.Add(dt2.Rows[i][0].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση επωνυμίας/Α.Φ.Μ. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }

        private void NameTxt_TextChanged(object sender, EventArgs e)
        {
            if (SearchNameTxt.TextLength > 0)
            {
                SearchNameListBox.Height = 21;
                SearchNameListBox.Items.Clear();
                if (SearchNameTxt.TextLength > 0)
                {
                    SearchNameListBox.Height = 21;
                    string[] s2 = SearchNameTxt.Text.Split(' ');
                    foreach (var item in ls)
                    {
                        int Contain = 0;
                        foreach (var item2 in s2)
                        {

                            if (Checks.RemoveDiacritics(item.ToUpper()).Contains(Checks.RemoveDiacritics(item2.ToUpper())) && item2 != "")
                            {
                                Contain++;
                            }
                        }
                        if (Contain == s2.Count())
                        {
                            SearchNameListBox.Visible = true;
                            SearchNameListBox.Items.Add(item);

                        }
                    }
                    if (SearchNameListBox.Items.Count == 1)
                    {
                        SearchNameListBox.Height = 42;
                    }
                    else if (SearchNameListBox.Items.Count == 2)
                    {
                        SearchNameListBox.Height = 63;
                    }
                    else if (SearchNameListBox.Items.Count >= 3)
                    {
                        SearchNameListBox.Height = 84;
                    }


                }
                else
                {
                    SearchNameListBox.Visible = false;
                }
            }
        }

        private void SearchNameListBox_Click(object sender, EventArgs e)
        {
            if (SearchNameListBox.SelectedItem!=null)
            {
                SearchNameTxt.Text = SearchNameListBox.SelectedItem.ToString();
                SearchNameListBox.Visible = false;
                SearchNameTxt.Focus();
            }
        }

        private void SearchNameTxt_Enter(object sender, EventArgs e)
        {
            SelectNameCmb.SelectedIndex = -1;
            SearchAfmTxt.Text = "";
        }

        private void SelectNameCmb_Enter(object sender, EventArgs e)
        {
            SearchNameTxt.Text = "";
            SearchAfmTxt.Text = "";
        }

        private void SearchAfmTxt_Enter(object sender, EventArgs e)
        {
            SearchNameTxt.Text = "";
            SelectNameCmb.SelectedIndex = -1;
        }

        private void SearchNameTxt_Leave(object sender, EventArgs e)
        {
            if (SearchNameListBox.Focused != true)
            {
                SearchNameListBox.Visible = false;
            }
        }

        public string ReturnSupplierId()
        {
            return SupplierId;
        }
        public string ReturnSupplierName()
        {
            return SupplierName;
        }
        public string ReturnSupplierAfm()
        {
            return SupplierAfm;
        }
        public string ReturnSupplierDebit()
        {
            return SupplierDebit;
        }
        public string ReturnSupplierOccupation()
        {
            return SupplierOccupation;
        }
        public string ReturnSupplierAddress()
        {
            return SupplierAddress;
        }
        public string ReturnSupplierTk()
        {
            return SupplierTk;
        }
        public string ReturnSupplierTax_office()
        {
            return SupplierTax_office;
        }
        public string ReturnSupplierPhone()
        {
            return SupplierPhone;
        }
        public string ReturnSupplierEmail()
        {
            return SupplierEmail;
        }
        public string ReturnSupplierRegion()
        {
            return SupplierRegion;
        }
        public string ReturnSupplierManager()
        {
            return SupplierManager;
        }
        public string ReturnSupplierPhone2()
        {
            return SupplierPhone2;
        }

        private void RetrieveBtn_Click(object sender, EventArgs e)
        {
            if (SearchNameTxt.Text == "" && SearchAfmTxt.Text == "" && SelectNameCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Θα πρέπει πρώτα να εισάγετε Επωνυμία ή Α.Φ.Μ. του προμηθευτή.");
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        try
                        {
                            string query;
                            string param;
                            if (SearchNameTxt.Text != "")
                            {
                                query = "select Id,Name,Afm,Debit,Occupation,Address,Tk,Tax_office,Phone, Email,Region,Manager,Phone2 from Suppliers where UPPER(Name)=@parameter";
                                param = SearchNameTxt.Text;
                            }
                            else if (SearchAfmTxt.Text != "")
                            {

                                query = "select Id,Name,Afm,Debit,Occupation,Address,Tk,Tax_office,Phone, Email,Region,Manager,Phone2 from Suppliers where Afm=@parameter";
                                param = SearchAfmTxt.Text;
                            }
                            else
                            {
                                query = "select Id,Name,Afm,Debit,Occupation,Address,Tk,Tax_office,Phone, Email,Region,Manager,Phone2 from Suppliers where UPPER(Name)=@parameter";
                                param = SelectNameCmb.GetItemText(SelectNameCmb.SelectedItem);
                            }

                            SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", param);
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Δε βρέθηκε προμηθευτής με τα στοιχεία που εισάγατε.");
                            }
                            else if (dt.Rows.Count > 1)
                            {
                                MessageBox.Show("Υπάρχουν " + dt.Rows.Count + " καταχωρημένοι προμηθευτές με τα στοιχεία που έχετε εισάγει. Παρακαλώ αλλάξτε τρόπο αναζήτησης.");
                            }
                            else
                            {
                                SupplierId = dt.Rows[0][0].ToString();
                                SupplierName = dt.Rows[0][1].ToString();
                                SupplierAfm = dt.Rows[0][2].ToString();
                                SupplierDebit = dt.Rows[0][3].ToString();
                                SupplierOccupation = dt.Rows[0][4].ToString();
                                SupplierAddress = dt.Rows[0][5].ToString();
                                SupplierTk = dt.Rows[0][6].ToString();
                                SupplierTax_office = dt.Rows[0][7].ToString();
                                SupplierPhone = dt.Rows[0][8].ToString();
                                SupplierEmail = dt.Rows[0][9].ToString();
                                SupplierRegion = dt.Rows[0][10].ToString();
                                SupplierManager = dt.Rows[0][11].ToString();
                                SupplierPhone2 = dt.Rows[0][12].ToString();
                                SearchNameTxt.Text = "";
                                SearchAfmTxt.Text = "";
                                SelectNameCmb.SelectedIndex = -1;
                                this.Close();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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

        private void SearchNameListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchNameListBox_Click(null, null);
            }
        }

        private void SearchNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (SearchNameListBox.Visible == true && SearchNameListBox.Items.Count >= 1)
                {
                    SearchNameListBox.Focus();
                    SearchNameListBox.SelectedIndex = 0;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class ReserveAddProduct : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> ls = new List<string>();

        public ReserveAddProduct()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetDataProd();
        }
        
        private void GetDataProd()
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        ls.Clear();
                        SearchIdTxt.AutoCompleteCustomSource.Clear();
                        string query = "select Description from Products order by Description";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        SelectNameCmb.Items.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SelectNameCmb.Items.Add(dt.Rows[i][0].ToString());
                            ls.Add(dt.Rows[i][0].ToString());
                        }
                        string query2 = "select Id from Products order by Id";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            SearchIdTxt.AutoCompleteCustomSource.Add(dt2.Rows[i][0].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση περιγραφής/κωδικού Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string errorMessages = "";
            if (DescrTxt.Text == "" ||  QuantTxt.Text == "" || PriceTxt.Text == "" || DiscTxt.Text == "")
            {
                errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα πεδία με αστερίσκο (*).\n"; ;
            }
            else
            {
                errorMessages += chk.CheckQuant(QuantTxt.Text);
                errorMessages += chk.CheckPrice(PriceTxt.Text);
                errorMessages += chk.CheckPrice(DiscTxt.Text);
            }
            if (errorMessages != "")
            {
                MessageBox.Show(errorMessages);
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                        SqlCommand InsCmd1 = sqlcon.CreateCommand();
                        InsCmd1.Connection = sqlcon;
                        InsCmd1.Transaction = InsTrans;
                        try
                        {
                            InsCmd1.CommandText = "insert into ProductsReserve (Id, ProductId, Quant, Price, Disc, Date, SalesPrice, SalesDisc) values((select dbo.nvl(Max(Id)+1,0) from dbo.ProductsReserve), @prodid, @quant, @price, @disc, @date, (select dbo.ProfitNewPrice(@price,@disc,0,0,(select ProfitPerc from Products where id=@prodid))),(select dbo.ProfitNewDisc(@price,@disc,0,0,(select ProfitPerc from Products where id=@prodid))))";
                            InsCmd1.Parameters.AddWithValue("@prodid", IdTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@quant", QuantTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@price", PriceTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@disc", DiscTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@date", System.DateTime.Today.ToShortDateString());
                            InsCmd1.ExecuteNonQuery();
                            InsTrans.Commit();
                            MessageBox.Show("Το προϊόν προστέθηκε στην αποθήκη με επιτυχία.");
                            GetDataProd();
                            ClearValues();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του προϊόντος. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                            try
                            {
                                InsTrans.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του προϊόντος. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                            }
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

        private void ClearValues()
        {
            IdTxt.Text = "";
            DescrTxt.Text = "";
            QuantTxt.Text = "";
            PriceTxt.Text = "";
            DiscTxt.Text = "";
            AddBtn.Visible = false;
        }

        private void QuantTxt_TextChanged(object sender, EventArgs e)
        {
            QuantTxt.Text = QuantTxt.Text.Replace(',', '.');
            QuantTxt.SelectionStart = QuantTxt.Text.Length;
        }
        
        private void DescrTxt_TextChanged(object sender, EventArgs e)
        {

            if (DescrTxt.TextLength > 0)
            {
                SearchNameListBox.Height = 21;
                SearchNameListBox.Items.Clear();
                if (DescrTxt.TextLength > 0)
                {
                    SearchNameListBox.Height = 21;
                    string[] s2 = DescrTxt.Text.Split(' ');
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
            else
            {
                SearchNameListBox.Height = 21;
                SearchNameListBox.Visible = false;
            }
        }

        private void DescrTxt_Leave(object sender, EventArgs e)
        {
            if (SearchNameListBox.Focused != true)
            {
                SearchNameListBox.Visible = false;
            }
        }

        private void SearchNameListBox_Leave(object sender, EventArgs e)
        {
            SearchNameListBox.Visible = false;
        }
        

        private void PriceTxt_TextChanged(object sender, EventArgs e)
        {
            PriceTxt.Text = PriceTxt.Text.Replace(',', '.');
            PriceTxt.SelectionStart = PriceTxt.Text.Length;
        }

        private void DiscTxt_TextChanged(object sender, EventArgs e)
        {
            DiscTxt.Text = DiscTxt.Text.Replace(',', '.');
            DiscTxt.SelectionStart = DiscTxt.Text.Length;
        }

        private void SearchNameTxt_TextChanged(object sender, EventArgs e)
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
            else
            {
                SearchNameListBox.Height = 21;
                SearchNameListBox.Visible = false;
            }
        }


        private void SearchNameListBox_Click(object sender, EventArgs e)
        {
            if (SearchNameListBox.SelectedItem != null)
            {
                string Name = SearchNameListBox.SelectedItem.ToString();
                SearchNameTxt.Text = Name;
                SearchNameTxt.Focus();
                SearchNameListBox.Visible = false;
            }
        }

        private void SearchNameTxt_Enter(object sender, EventArgs e)
        {
            SelectNameCmb.SelectedIndex = -1;
            SearchIdTxt.Text = "";
        }

        private void SelectNameCmb_Enter(object sender, EventArgs e)
        {
            SearchNameTxt.Text = "";
            SearchIdTxt.Text = "";
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

        private void RetrieveBtn_Click(object sender, EventArgs e)
        {
            if (SearchNameTxt.Text == "" && SearchIdTxt.Text == "" && SelectNameCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Θα πρέπει πρώτα να εισάγετε περιγραφή ή κωδικό του προϊόντος.");
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
                                query = "select Id,Description from Products where UPPER(Description)=@parameter";
                                param = SearchNameTxt.Text;
                            }
                            else if (SearchIdTxt.Text != "")
                            {

                                query = "select Id,Description from Products where Id=@parameter";
                                param = SearchIdTxt.Text;
                            }
                            else
                            {
                                query = "select Id,Description from Products where UPPER(Description)=@parameter";
                                param = SelectNameCmb.GetItemText(SelectNameCmb.SelectedItem);
                            }

                            SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", param);
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Δε βρέθηκε προϊόν με τα στοιχεία που εισάγατε.");
                            }
                            else if (dt.Rows.Count > 1)
                            {
                                MessageBox.Show("Υπάρχουν " + dt.Rows.Count + " καταχωρημένα προϊόντα με τα στοιχεία που έχετε εισάγει. Παρακαλώ αλλάξτε τρόπο αναζήτησης.");
                            }
                            else
                            {
                                IdTxt.Text = dt.Rows[0][0].ToString();
                                DescrTxt.Text = dt.Rows[0][1].ToString();
                                SearchNameTxt.Text = "";
                                SearchIdTxt.Text = "";
                                SelectNameCmb.SelectedIndex = -1;
                                AddBtn.Visible = true;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση του προϊόντος. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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

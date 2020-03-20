using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class OrderAddProduct : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> ls = new List<string>();
        List<string> ls2 = new List<string>();
        bool has_added = false;

        public OrderAddProduct()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
        }


        public bool has_added_prod()
        {
            return has_added;
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
                        string query = "select Description from Products order by Description";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ls.Add(dt.Rows[i][0].ToString());
                        }
                        ls2.Clear();
                        string query2 = "select SupplierDescr from Products order by SupplierDescr";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            ls2.Add(dt2.Rows[i][0].ToString());
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
            if (DescrTxt.Text == "" || LongDescrTxt.Text == "" || SupplierDescrTxt.Text == "" || QuantTxt.Text == "" || PriceTxt.Text == "" || DiscTxt.Text == "" || UnitTxt.Text == "" || ProfitPercTxt.Text == "")
            {
                errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα πεδία με αστερίσκο (*).\n"; ;
            }
            else
            {
                errorMessages += chk.CheckQuant(QuantTxt.Text);
                errorMessages += chk.CheckPrice(PriceTxt.Text);
                errorMessages += chk.CheckPrice(DiscTxt.Text);
                errorMessages += chk.CheckPrice(ProfitPercTxt.Text);
                if (MinStockTxt.Text != "")
                {
                    errorMessages += chk.CheckMinStock(MinStockTxt.Text);
                }
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
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from Products where Description=@descr", sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"descr", DescrTxt.Text);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"manufac", ManufacTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Υπάρχει ήδη καταχωρημένο προϊόν με τα στοιχεία που έχετε εισάγει.");
                        }
                        else
                        {
                            SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                            SqlCommand InsCmd1 = sqlcon.CreateCommand();
                            InsCmd1.Connection = sqlcon;
                            SqlCommand InsCmd2 = sqlcon.CreateCommand();
                            InsCmd2.Connection = sqlcon;
                            SqlCommand InsCmd3 = sqlcon.CreateCommand();
                            InsCmd3.Connection = sqlcon;
                            InsCmd1.Transaction = InsTrans;
                            InsCmd2.Transaction = InsTrans;
                            InsCmd3.Transaction = InsTrans;
                            try
                            {
                                InsCmd1.CommandText = "insert into Products(Id, Description, LongDescr, SupplierDescr, Manufacture, Unit, ProfitPerc) values((select dbo.nvl(Max(Id)+1,10000) from dbo.Products), @descr, @longd, @supdescr, @manuf, @unit, @prof)";
                                InsCmd1.Parameters.AddWithValue("@descr", DescrTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@longd", LongDescrTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@supdescr", SupplierDescrTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@manuf", ManufacTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@unit", UnitTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@prof", ProfitPercTxt.Text);
                                InsCmd1.ExecuteNonQuery();
                                if (MinStockTxt.Text != "")
                                {
                                    InsCmd2.CommandText = "insert into ProductsMinStock (Id, ProductId, MinStock) values((select dbo.nvl(Max(Id) + 1, 0) from ProductsMinStock), (select dbo.nvl(Max(Id),1000) from dbo.Products), @minstock)";
                                    InsCmd2.Parameters.AddWithValue("@minstock", MinStockTxt.Text);
                                    InsCmd2.ExecuteNonQuery();
                                }
                                InsCmd3.CommandText = "insert into ProductsReserve (Id, ProductId, Quant, Price, Disc, Date, SalesPrice, SalesDisc) values((select dbo.nvl(Max(Id)+1,0) from dbo.ProductsReserve), (select dbo.nvl(Max(Id),1000) from dbo.Products), @quant, @price, @disc, @date, (select dbo.ProfitNewPrice(@price,@disc,0,0,@prof)),(select dbo.ProfitNewDisc(@price,@disc,0,0,@prof)))";
                                InsCmd3.Parameters.AddWithValue("@quant", QuantTxt.Text);
                                InsCmd3.Parameters.AddWithValue("@price", PriceTxt.Text);
                                InsCmd3.Parameters.AddWithValue("@disc", DiscTxt.Text);
                                InsCmd3.Parameters.AddWithValue("@prof", ProfitPercTxt.Text);
                                InsCmd3.Parameters.AddWithValue("@date", System.DateTime.Today.ToShortDateString());
                                InsCmd3.ExecuteNonQuery();

                                InsTrans.Commit();
                                MessageBox.Show("Το προϊόν προστέθηκε με επιτυχία.");
                                GetDataProd();
                                has_added = true;
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
            DescrTxt.Text = "";
            LongDescrTxt.Text = "";
            SupplierDescrTxt.Text = "";
            ManufacTxt.Text = "";
            ProfitPercTxt.Text = "";
            QuantTxt.Text = "";
            PriceTxt.Text = "";
            DiscTxt.Text = "";
            UnitTxt.Text = "";
            MinStockTxt.Text = "";
        }

        private void QuantTxt_TextChanged(object sender, EventArgs e)
        {
            QuantTxt.Text = QuantTxt.Text.Replace(',', '.');
            QuantTxt.SelectionStart = QuantTxt.Text.Length;
        }

        private void MinStockTxt_TextChanged(object sender, EventArgs e)
        {
            MinStockTxt.Text = MinStockTxt.Text.Replace(',', '.');
            MinStockTxt.SelectionStart = MinStockTxt.Text.Length;
        }

        private void ProfitPercTxt_TextChanged(object sender, EventArgs e)
        {
            ProfitPercTxt.Text = ProfitPercTxt.Text.Replace(',', '.');
            ProfitPercTxt.SelectionStart = ProfitPercTxt.Text.Length;
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

        private void SearchNameListBox_Click(object sender, EventArgs e)
        {
            if (SearchNameListBox.SelectedItem != null)
            {
                DescrTxt.Text = SearchNameListBox.SelectedItem.ToString();
                SearchNameListBox.Visible = false;
                DescrTxt.Focus();
            }
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

        private void SearchNameListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchNameListBox_Click(null, null);
            }
        }

        private void DescrTxt_KeyDown(object sender, KeyEventArgs e)
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


        private void SearchSupplierDescrListBox_Click(object sender, EventArgs e)
        {
            if (SearchSupplierDescrListBox.SelectedItem != null)
            {
                SupplierDescrTxt.Text = SearchSupplierDescrListBox.SelectedItem.ToString();
                SearchSupplierDescrListBox.Visible = false;
                SupplierDescrTxt.Focus();
            }
        }

        private void SearchSupplierDescrListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SearchSupplierDescrListBox_Click(null, null);
            }
        }

        private void SearchSupplierDescrListBox_Leave(object sender, EventArgs e)
        {
            SearchSupplierDescrListBox.Visible = false;
        }

        private void SupplierDescrTxt_Leave(object sender, EventArgs e)
        {

            if (SearchSupplierDescrListBox.Focused != true)
            {
                SearchSupplierDescrListBox.Visible = false;
            }
        }

        private void SupplierDescrTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (SearchSupplierDescrListBox.Visible == true && SearchSupplierDescrListBox.Items.Count >= 1)
                {
                    SearchSupplierDescrListBox.Focus();
                    SearchSupplierDescrListBox.SelectedIndex = 0;
                }
            }
        }

        private void SupplierDescrTxt_TextChanged(object sender, EventArgs e)
        {

            if (SupplierDescrTxt.TextLength > 0)
            {
                SearchSupplierDescrListBox.Height = 21;
                SearchSupplierDescrListBox.Items.Clear();
                if (SupplierDescrTxt.TextLength > 0)
                {
                    SearchSupplierDescrListBox.Height = 21;
                    string[] s2 = SupplierDescrTxt.Text.Split(' ');
                    foreach (var item in ls2)
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
                            SearchSupplierDescrListBox.Visible = true;
                            SearchSupplierDescrListBox.Items.Add(item);

                        }
                    }
                    if (SearchSupplierDescrListBox.Items.Count == 1)
                    {
                        SearchSupplierDescrListBox.Height = 42;
                    }
                    else if (SearchSupplierDescrListBox.Items.Count == 2)
                    {
                        SearchSupplierDescrListBox.Height = 63;
                    }
                    else if (SearchSupplierDescrListBox.Items.Count >= 3)
                    {
                        SearchSupplierDescrListBox.Height = 84;
                    }


                }
                else
                {
                    SearchSupplierDescrListBox.Visible = false;
                }
            }
            else
            {
                SearchSupplierDescrListBox.Height = 21;
                SearchSupplierDescrListBox.Visible = false;
            }
        }
    }
}

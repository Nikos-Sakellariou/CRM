using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class EditProduct : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> ProductList;
        List<string> ls = new List<string>();
        Dictionary<string, string> CusInvDc = new Dictionary<string, string>();
        Dictionary<string, string> CusDebDc = new Dictionary<string, string>();
        Dictionary<string, string> CusDisDc = new Dictionary<string, string>();
        Dictionary<string, string> SupInvDc = new Dictionary<string, string>();
        Dictionary<string, string> SupDebDc = new Dictionary<string, string>();
        Dictionary<string, string> SupDisDc = new Dictionary<string, string>();
        Dictionary<string, string> SupDisRDc = new Dictionary<string, string>();

        public EditProduct()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetDataProd();

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            DescrTxt.Enabled = true;
            LongDescrTxt.Enabled = true;
            SupplierDescrTxt.Enabled = true;
            ManufacTxt.Enabled = true;
            QuantTxt.Enabled = true;
            UnitTxt.Enabled = true;
            ProfitPercTxt.Enabled = true;
            MinStockTxt.Enabled = true;
            SearchNameTxt.Enabled = false;
            SearchIdTxt.Enabled = false;
            SelectNameCmb.Enabled = false;
            RetrieveBtn.Enabled = false;
            SaveBtn.Visible = true;
            CancelBtn.Visible = true;
            EditBtn.Visible = false;
            ProductList = new List<string>(new string[] { DescrTxt.Text,
            LongDescrTxt.Text,
            ManufacTxt.Text,
            QuantTxt.Text,
            UnitTxt.Text,
            MinStockTxt.Text,
            ProfitPercTxt.Text,
            SupplierDescrTxt.Text});
        }
        
        private void ClearValues()
        {
            IdTxt.Text = "";
            DescrTxt.Text = "";
            LongDescrTxt.Text = "";
            SupplierDescrTxt.Text = "";
            ManufacTxt.Text = "";
            ProfitPercTxt.Text = "";
            QuantTxt.Text = "";
            UnitTxt.Text = "";
            MinStockTxt.Text = "";
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
            if (SearchNameListBox.Focused!=true)
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
                                query = "select a.Id,a.Description,a.LongDescr,a.Manufacture,b.Quant,dbo.FVAL_PRODUCT_MINSTOCK(a.Id),a.Unit, a.ProfitPerc,a.SupplierDescr from Products a, ProductsReserveView b where a.Id=b.Id and UPPER(a.Description)=@parameter";
                                param = SearchNameTxt.Text;
                            }
                            else if (SearchIdTxt.Text != "")
                            {

                                query = "select a.Id,a.Description,a.LongDescr,a.Manufacture,b.Quant,dbo.FVAL_PRODUCT_MINSTOCK(a.Id),a.Unit, a.ProfitPerc,a.SupplierDescr from Products a, ProductsReserveView b where a.Id=b.Id and a.Id=@parameter";
                                param = SearchIdTxt.Text;
                            }
                            else
                            {
                                query = "select a.Id,a.Description,a.LongDescr,a.Manufacture,b.Quant,dbo.FVAL_PRODUCT_MINSTOCK(a.Id),a.Unit, a.ProfitPerc,a.SupplierDescr from Products a, ProductsReserveView b where a.Id=b.Id and UPPER(a.Description)=@parameter";
                                param = SelectNameCmb.GetItemText(SelectNameCmb.SelectedItem);
                            }
                            
                            SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", param);
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            if (dt.Rows.Count==0)
                            {
                                MessageBox.Show("Δε βρέθηκε προϊόν με τα στοιχεία που εισάγατε.");
                            }
                            else if (dt.Rows.Count>1)
                            {
                                MessageBox.Show("Υπάρχουν "+ dt.Rows.Count+" καταχωρημένα προϊόντα με τα στοιχεία που έχετε εισάγει. Παρακαλώ αλλάξτε τρόπο αναζήτησης.");
                            }
                            else
                            {
                                IdTxt.Text = dt.Rows[0][0].ToString();
                                DescrTxt.Text = dt.Rows[0][1].ToString();
                                LongDescrTxt.Text = dt.Rows[0][2].ToString();
                                ManufacTxt.Text = dt.Rows[0][3].ToString();
                                QuantTxt.Text = dt.Rows[0][4].ToString();
                                MinStockTxt.Text = dt.Rows[0][5].ToString();
                                UnitTxt.Text = dt.Rows[0][6].ToString();
                                ProfitPercTxt.Text = dt.Rows[0][7].ToString();
                                SupplierDescrTxt.Text = dt.Rows[0][8].ToString();
                                SearchNameTxt.Text = "";
                                SearchIdTxt.Text = "";
                                SelectNameCmb.SelectedIndex = -1;
                                EditBtn.Visible = true;
                            }

                            


                            string query2 = @"select distinct Right('          '+InvoiceId,15),Right('          '+InvoiceSeries,8),InvoiceDate,Right('          '+ProductQuant,8),Right('          '+ProductPrice,12),Prev,Id from
                            (select InvoiceId, InvoiceSeries, InvoiceDate, ProductQuant, ProductPrice,'SupInvoice' Prev,b.Id from SupplierInvoiceProducts a, SupplierInvoice b where a.SupplierInvoiceId= b.id and ProductId = @id
                            union
                            select DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, ProductQuant, ProductPrice,'SupDebitInvoice',b.Id from SupplierDebitInvoiceProducts a, SupplierDebitInvoice b  where a.SupplierDebitInvoiceId = b.id and ProductId = @id
                            union
                            select DisNoteId, DisNoteSeries, DisNoteDate, ProductQuant, '','SupDisNoteInvoice',b.Id from SupplierDisNoteProducts a, SupplierDisNote b  where a.SupplierDisNoteId = b.id and dbo.SupplierDisNote_without_Invoice(b.Id) = 0 and ProductId = @id
                            union
                            select DisNoteId, DisNoteSeries, DisNoteDate, ProductQuant, '','SupDisNoteReturnInvoice',b.Id from SupplierReturnDisNoteProducts a, SupplierReturnDisNote b  where a.SupplierDisNoteId = b.id and ProductId = @id
                            union
                            select InvoiceId, InvoiceSeries, InvoiceDate, ProductQuant, ProductPrice,'CusInvoice',b.Id from CustomerInvoiceProducts a, CustomerInvoice b where a.CustomerInvoiceId = b.id and ProductId = @id
                            union
                            select DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, ProductQuant, ProductPrice,'CusDebitInvoice',b.Id  from CustomerDebitInvoiceProducts a, CustomerDebitInvoice b  where a.CustomerDebitInvoiceId = b.id and ProductId = @id
                            union
                            select DisNoteId, DisNoteSeries, DisNoteDate, ProductQuant, '','CusDisNoteInvoice',b.Id from CustomerDisNoteProducts a, CustomerDisNote b  where a.CustomerDisNoteId = b.id and dbo.CustomerDisNote_without_Invoice(b.Id) = 0 and ProductId = @id
                            ) a
                            order by InvoiceDate desc";
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                            SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                            DataTable dt2 = new DataTable();
                            SearchAdapt2.Fill(dt2);
                            DocumentLbx.Items.Clear();
                            SupInvDc.Clear();
                            SupDebDc.Clear();
                            SupDisDc.Clear();
                            SupDisRDc.Clear();
                            CusInvDc.Clear();
                            CusDebDc.Clear();
                            CusDisDc.Clear();
                            for (int i = 0; (i < dt2.Rows.Count); i++)
                            {
                                DateTime dat = Convert.ToDateTime(dt2.Rows[i][2].ToString());
                                DocumentLbx.Items.Add(dt2.Rows[i][0].ToString() + " / " + dt2.Rows[i][1].ToString() + "  -  " + dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year + "  -  " + dt2.Rows[i][3].ToString() + "  -  " + dt2.Rows[i][4].ToString());
                                if (dt2.Rows[i][5].ToString() == "SupInvoice")
                                {
                                    SupInvDc.Add(i.ToString(), dt2.Rows[i][6].ToString());
                                }
                                else if (dt2.Rows[i][5].ToString() == "SupDebitInvoice")
                                {
                                    SupDebDc.Add(i.ToString(), dt2.Rows[i][6].ToString());
                                }
                                else if (dt2.Rows[i][5].ToString() == "SupDisNoteInvoice")
                                {
                                    SupDisDc.Add(i.ToString(), dt2.Rows[i][6].ToString());
                                }
                                else if (dt2.Rows[i][5].ToString() == "SupDisNoteReturnInvoice")
                                {
                                    SupDisRDc.Add(i.ToString(), dt2.Rows[i][6].ToString());
                                }
                                else if (dt2.Rows[i][5].ToString() == "CusInvoice")
                                {
                                    CusInvDc.Add(i.ToString(), dt2.Rows[i][6].ToString());
                                }
                                else if (dt2.Rows[i][5].ToString() == "CusDebitInvoice")
                                {
                                    CusDebDc.Add(i.ToString(), dt2.Rows[i][6].ToString());
                                }
                                else if (dt2.Rows[i][5].ToString() == "CusDisNoteInvoice")
                                {
                                    CusDisDc.Add(i.ToString(), dt2.Rows[i][6].ToString());
                                }
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

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DescrTxt.Text = ProductList[0];
            LongDescrTxt.Text = ProductList[1];
            ManufacTxt.Text = ProductList[2];
            QuantTxt.Text = ProductList[3];
            UnitTxt.Text = ProductList[4];
            MinStockTxt.Text = ProductList[5];
            ProfitPercTxt.Text = ProductList[6];
            SupplierDescrTxt.Text = ProductList[7];
            DescrTxt.Enabled = false;
            LongDescrTxt.Enabled = false;
            SupplierDescrTxt.Enabled = false;
            ManufacTxt.Enabled = false;
            QuantTxt.Enabled = false;
            UnitTxt.Enabled = false;
            ProfitPercTxt.Enabled = false;
            MinStockTxt.Enabled = false;
            SearchNameTxt.Enabled = true;
            SearchIdTxt.Enabled = true;
            SelectNameCmb.Enabled = true;
            RetrieveBtn.Enabled = true;
            SaveBtn.Visible = false;
            CancelBtn.Visible = false;
            EditBtn.Visible = true;
            ProductList.Clear();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            List<string> NewProductList = new List<string>(new string[] {
                DescrTxt.Text,
                LongDescrTxt.Text,
                ManufacTxt.Text,
                QuantTxt.Text,
                UnitTxt.Text,
                MinStockTxt.Text,
                ProfitPercTxt.Text,
                SupplierDescrTxt.Text});
            int cnt = 0;
            for (int i = 0; i < NewProductList.Count; i++)
            {
                if (NewProductList[i]!= ProductList[i])
                {
                    cnt++;
                }
            }
            if (cnt==0)
            {
                MessageBox.Show("Δεν έχετε πραγματοποιήσει κάποια αλλαγή στα στοιχεία του προϊόντος.");
            }
            else
            {
                string errorMessages = "";
                if (DescrTxt.Text == "" || LongDescrTxt.Text == "" || SupplierDescrTxt.Text == "" || ManufacTxt.Text == "" || QuantTxt.Text == "" || UnitTxt.Text == "" || ProfitPercTxt.Text == "")
                {
                    errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα απαραίτητα πεδία.\n"; ;
                }
                else
                {
                    errorMessages += chk.CheckQuant(QuantTxt.Text);
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
                                SqlTransaction InsTrans = sqlcon.BeginTransaction("UpdateTransaction");
                                SqlCommand UpdCmd1 = sqlcon.CreateCommand();
                                UpdCmd1.Connection = sqlcon;
                                SqlCommand UpdCmd2 = sqlcon.CreateCommand();
                                UpdCmd2.Connection = sqlcon;
                                UpdCmd1.Transaction = InsTrans;
                                UpdCmd2.Transaction = InsTrans;
                                try
                                {
                                    UpdCmd1.CommandText = "update Products set Description=@descr, LongDescr=@longd, Manufacture = @manufac , Unit = @unit , ProfitPerc = @prof , SupplierDescr=@supdescr where Id=@id";
                                    UpdCmd1.Parameters.AddWithValue("@descr", DescrTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@longd", LongDescrTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@supdescr", SupplierDescrTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@manufac", ManufacTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@unit", UnitTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@prof", ProfitPercTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@id", IdTxt.Text);
                                    UpdCmd1.ExecuteNonQuery();
                                    if (NewProductList[5]!= "" && ProductList[5] == "") //Min Stock
                                    {
                                        UpdCmd2.CommandText = "insert into ProductsMinStock (Id, ProductId, MinStock) values((select dbo.nvl(Max(Id) + 1, 0) from ProductsMinStock), @id, @minstock)";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.Parameters.AddWithValue("@minstock", MinStockTxt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }
                                    else if (NewProductList[5] == "" && ProductList[5] != "")
                                    {
                                        UpdCmd2.CommandText = "delete ProductsMinStock where ProductId=@id";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }
                                    else if (NewProductList[5] != "" && ProductList[5] != "" && ProductList[5] != NewProductList[5])
                                    {
                                        UpdCmd2.CommandText = "update ProductsMinStock set MinStock=@minstock where ProductId=@id";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.Parameters.AddWithValue("@minstock", MinStockTxt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }
                                    
                                    InsTrans.Commit();
                                    MessageBox.Show("Οι αλλαγές πραγματοποιήθηκαν με επιτυχία.");
                                    ClearValues();
                                    DescrTxt.Enabled = false;
                                    LongDescrTxt.Enabled = false;
                                    SupplierDescrTxt.Enabled = false;
                                    ManufacTxt.Enabled = false;
                                    ProfitPercTxt.Enabled = false;
                                    QuantTxt.Enabled = false;
                                    UnitTxt.Enabled = false;
                                    MinStockTxt.Enabled = false;
                                    SearchNameTxt.Enabled = true;
                                    SearchIdTxt.Enabled = true;
                                    SelectNameCmb.Enabled = true;
                                    RetrieveBtn.Enabled = true;
                                    SaveBtn.Visible = false;
                                    CancelBtn.Visible = false;
                                    ProductList.Clear();
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

        private void SearchNameListBox_Leave(object sender, EventArgs e)
        {
            SearchNameListBox.Visible = false;
        }
        
        void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }

        private void DocumentLbx_DoubleClick(object sender, EventArgs e)
        {
            if (DocumentLbx.SelectedIndex != -1)
            {
                string outcome;
                if (CusInvDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    CusInvDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewInvoiceCustomer PrevInvoice = new PreviewInvoiceCustomer(outcome);
                    PrevInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevInvoice.ShowDialog();
                }
                else if (CusDebDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    CusDebDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDebitInvoiceCustomer PrevDebitInvoice = new PreviewDebitInvoiceCustomer(outcome);
                    PrevDebitInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDebitInvoice.ShowDialog();
                }
                else if (CusDisDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    CusDisDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDisNoteCustomer PrevDisNote = new PreviewDisNoteCustomer(outcome);
                    PrevDisNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDisNote.ShowDialog();
                }
                else if (SupInvDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    SupInvDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewInvoiceSupplier PrevInvoice = new PreviewInvoiceSupplier(outcome);
                    PrevInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevInvoice.ShowDialog();
                }
                else if (SupDebDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    SupDebDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDebitInvoiceSupplier PrevDebitInvoice = new PreviewDebitInvoiceSupplier(outcome);
                    PrevDebitInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDebitInvoice.ShowDialog();
                }
                else if (SupDisDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    SupDisDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDisNoteSupplier PrevDisNote = new PreviewDisNoteSupplier(outcome);
                    PrevDisNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDisNote.ShowDialog();
                }
                else if (SupDisRDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    SupDisRDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDisNoteReturnSupplier PrevDisNote = new PreviewDisNoteReturnSupplier(outcome);
                    PrevDisNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDisNote.ShowDialog();
                }
            }
        }

        private void ProfitPercTxt_TextChanged(object sender, EventArgs e)
        {
            ProfitPercTxt.Text = ProfitPercTxt.Text.Replace(',', '.');
            ProfitPercTxt.SelectionStart = ProfitPercTxt.Text.Length;
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

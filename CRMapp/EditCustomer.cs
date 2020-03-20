using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace CRMapp
{

    public partial class EditCustomer : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> CustomerList;
        List<string> ls = new List<string>();
        Dictionary<string, string> InvDc = new Dictionary<string, string>();
        Dictionary<string, string> DebDc = new Dictionary<string, string>();
        Dictionary<string, string> DisDc = new Dictionary<string, string>();
        Dictionary<string, string> RecDc = new Dictionary<string, string>();

        public EditCustomer()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetDataNameAfm();

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Enabled = true;
            OccupationTxt.Enabled = true;
            AfmTxt.Enabled = true;
            AddressTxt.Enabled = true;
            TkTxt.Enabled = true;
            VatTxt.Enabled = true;
            PhoneTxt.Enabled = true;
            Phone2Txt.Enabled = true;
            EmailTxt.Enabled = true;
            Tax_officeTxt.Enabled = true;
            RegionTxt.Enabled = true;
            CreditTxt.Enabled = true;
            MaxCreditTxt.Enabled = true;
            RetailBox.Enabled = true;
            SearchNameTxt.Enabled = false;
            SearchAfmTxt.Enabled = false;
            SelectNameCmb.Enabled = false;
            RetrieveBtn.Enabled = false;
            SaveBtn.Visible = true;
            CancelBtn.Visible = true;
            EditBtn.Visible = false;
            CustomerList = new List<string>(new string[] { NameTxt.Text,
            OccupationTxt.Text,
            AfmTxt.Text,
            AddressTxt.Text,
            TkTxt.Text,
            VatTxt.Text,
            PhoneTxt.Text,
            Phone2Txt.Text,
            EmailTxt.Text,
            Tax_officeTxt.Text,
            CreditTxt.Text,
            MaxCreditTxt.Text,
            (RetailBox.Checked ?"1" :"0"),
            RegionTxt.Text});
        }
        
        private void ClearValues()
        {
            IdTxt.Text = "";
            NameTxt.Text = "";
            OccupationTxt.Text = "";
            AfmTxt.Text = "";
            AddressTxt.Text = "";
            TkTxt.Text = "";
            VatTxt.Text = "";
            PhoneTxt.Text = "";
            Phone2Txt.Text = "";
            EmailTxt.Text = "";
            Tax_officeTxt.Text = "";
            CreditTxt.Text = "";
            MaxCreditTxt.Text = "";
            RegionTxt.Text = "";
            RetailBox.Checked = false;
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
                        string query = "select Name from Customers order by Name";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        SelectNameCmb.Items.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SelectNameCmb.Items.Add(dt.Rows[i][0].ToString());
                            ls.Add(dt.Rows[i][0].ToString());
                        }
                        string query2 = "select Afm from Customers order by Afm";
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
            else
            {
                SearchNameListBox.Height = 21;
                SearchNameListBox.Visible = false;
            }
        }

        private void SearchNameListBox_Click(object sender, EventArgs e)
        {
            if (SearchNameListBox.SelectedItem!=null)
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
            if (SearchNameListBox.Focused!=true)
            {
                SearchNameListBox.Visible = false;
            }
        }

        private void RetrieveBtn_Click(object sender, EventArgs e)
        {
            if (SearchNameTxt.Text == "" && SearchAfmTxt.Text == "" && SelectNameCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Θα πρέπει πρώτα να εισάγετε Επωνυμία ή Α.Φ.Μ. του πελάτη.");
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
                                query = "select Id,Name,Occupation,Afm,Address,Phone,Email,Tax_office,Credit,Tk,dbo.FVAL_CUSTOMER_VAT(Id,24),dbo.FVAL_CUSTOMER_PHONE2(Id),dbo.FVAL_CUSTOMER_MAXCREDIT(Id),Retail,Region from Customers where UPPER(Name)=@parameter";
                                param = SearchNameTxt.Text;
                            }
                            else if (SearchAfmTxt.Text != "")
                            {

                                query = "select Id,Name,Occupation,Afm,Address,Phone,Email,Tax_office,Credit,Tk,dbo.FVAL_CUSTOMER_VAT(Id,24),dbo.FVAL_CUSTOMER_PHONE2(Id),dbo.FVAL_CUSTOMER_MAXCREDIT(Id),Retail,Region from Customers where Afm=@parameter";
                                param = SearchAfmTxt.Text;
                            }
                            else
                            {
                                query = "select Id,Name,Occupation,Afm,Address,Phone,Email,Tax_office,Credit,Tk,dbo.FVAL_CUSTOMER_VAT(Id,24),dbo.FVAL_CUSTOMER_PHONE2(Id),dbo.FVAL_CUSTOMER_MAXCREDIT(Id),Retail,Region from Customers where UPPER(Name)=@parameter";
                                param = SelectNameCmb.GetItemText(SelectNameCmb.SelectedItem);
                            }
                            
                            SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", param);
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            if (dt.Rows.Count==0)
                            {
                                MessageBox.Show("Δε βρέθηκε πελάτης με τα στοιχεία που εισάγατε.");
                            }
                            else if (dt.Rows.Count>1)
                            {
                                MessageBox.Show("Υπάρχουν "+ dt.Rows.Count+" καταχωρημένοι πελάτες με τα στοιχεία που έχετε εισάγει. Παρακαλώ αλλάξτε τρόπο αναζήτησης.");
                            }
                            else
                            {
                                IdTxt.Text = dt.Rows[0][0].ToString();
                                NameTxt.Text = dt.Rows[0][1].ToString();
                                OccupationTxt.Text = dt.Rows[0][2].ToString();
                                AfmTxt.Text = dt.Rows[0][3].ToString();
                                AddressTxt.Text = dt.Rows[0][4].ToString();
                                PhoneTxt.Text = dt.Rows[0][5].ToString();
                                EmailTxt.Text = dt.Rows[0][6].ToString();
                                Tax_officeTxt.Text = dt.Rows[0][7].ToString();
                                CreditTxt.Text = dt.Rows[0][8].ToString();
                                TkTxt.Text = dt.Rows[0][9].ToString();
                                VatTxt.Text = dt.Rows[0][10].ToString();
                                Phone2Txt.Text = dt.Rows[0][11].ToString();
                                MaxCreditTxt.Text = dt.Rows[0][12].ToString();
                                RetailBox.Checked = (dt.Rows[0][13].ToString()=="1" ?true :false);
                                RegionTxt.Text = dt.Rows[0][14].ToString();
                                SearchNameTxt.Text = "";
                                SearchAfmTxt.Text = "";
                                SelectNameCmb.SelectedIndex = -1;
                                EditBtn.Visible = true;
                            }
                            string query2 = @"select distinct Right('          '+InvoiceId,15),Right('          '+InvoiceSeries,8),InvoiceDate,Right('          '+InvoicePrice,18),Prev,Id from
                            (select InvoiceId, InvoiceSeries, InvoiceDate, InvoicePrice,'Invoice' Prev,Id from CustomerInvoice where CustomerId = @id
                            union
                            select DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, DebitInvoicePrice,'DebitInvoice',Id from CustomerDebitInvoice where CustomerId = @id
                            union
                            select DisNoteId, DisNoteSeries, DisNoteDate, '','DisNote',Id from CustomerDisNote where CustomerId = @id
                            union
                            select ReceiptId, ReceiptSeries, ReceiptDate, ReceiptPrice,'Receipt',Id from CustomerReceipt where CustomerId = @id ) a
                            order by InvoiceDate desc";
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                            SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                            DataTable dt2 = new DataTable();
                            SearchAdapt2.Fill(dt2);
                            DocumentLbx.Items.Clear();
                            InvDc.Clear();
                            DebDc.Clear();
                            DisDc.Clear();
                            RecDc.Clear();
                            for (int i = 0; (i < dt2.Rows.Count); i++)
                            {
                                DateTime dat = Convert.ToDateTime(dt2.Rows[i][2].ToString());
                                DocumentLbx.Items.Add(dt2.Rows[i][0].ToString() + " /" + dt2.Rows[i][1].ToString() + "   -   " + dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year + "   -   " + dt2.Rows[i][3].ToString());
                                if (dt2.Rows[i][4].ToString() == "Invoice")
                                {
                                    InvDc.Add(i.ToString(), dt2.Rows[i][5].ToString());
                                }
                                else if (dt2.Rows[i][4].ToString() == "DebitInvoice")
                                {
                                    DebDc.Add(i.ToString(), dt2.Rows[i][5].ToString());
                                }
                                else if (dt2.Rows[i][4].ToString() == "DisNote")
                                {
                                    DisDc.Add(i.ToString(), dt2.Rows[i][5].ToString());
                                }
                                else if (dt2.Rows[i][4].ToString() == "Receipt")
                                {
                                    RecDc.Add(i.ToString(), dt2.Rows[i][5].ToString());
                                }
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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


        void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text = CustomerList[0];
            OccupationTxt.Text = CustomerList[1];
            AfmTxt.Text = CustomerList[2];
            AddressTxt.Text = CustomerList[3];
            TkTxt.Text = CustomerList[4];
            VatTxt.Text = CustomerList[5];
            PhoneTxt.Text = CustomerList[6];
            Phone2Txt.Text = CustomerList[7];
            EmailTxt.Text = CustomerList[8];
            Tax_officeTxt.Text = CustomerList[9];
            CreditTxt.Text = CustomerList[10];
            MaxCreditTxt.Text = CustomerList[11];
            RetailBox.Checked = (CustomerList[12] == "1" ? true : false);
            RegionTxt.Text = CustomerList[13];
            NameTxt.Enabled = false;
            OccupationTxt.Enabled = false;
            AfmTxt.Enabled = false;
            AddressTxt.Enabled = false;
            RegionTxt.Enabled = false;
            TkTxt.Enabled = false;
            VatTxt.Enabled = false;
            PhoneTxt.Enabled = false;
            Phone2Txt.Enabled = false;
            EmailTxt.Enabled = false;
            Tax_officeTxt.Enabled = false;
            CreditTxt.Enabled = false;
            MaxCreditTxt.Enabled = false;
            RetailBox.Enabled = false;
            SearchNameTxt.Enabled = true;
            SearchAfmTxt.Enabled = true;
            SelectNameCmb.Enabled = true;
            RetrieveBtn.Enabled = true;
            SaveBtn.Visible = false;
            CancelBtn.Visible = false;
            EditBtn.Visible = true;
            CustomerList.Clear();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            List<string> NewCustomerList = new List<string>(new string[] {
                NameTxt.Text,
                OccupationTxt.Text,
                AfmTxt.Text,
                AddressTxt.Text,
                TkTxt.Text,
                VatTxt.Text,
                PhoneTxt.Text,
                Phone2Txt.Text,
                EmailTxt.Text,
                Tax_officeTxt.Text,
                CreditTxt.Text,
                MaxCreditTxt.Text,
                (RetailBox.Checked ?"1" :"0"),
                RegionTxt.Text
            });
            int cnt = 0;
            for (int i = 0; i < NewCustomerList.Count; i++)
            {
                if (NewCustomerList[i]!= CustomerList[i])
                {
                    cnt++;
                }
            }
            if (cnt==0)
            {
                MessageBox.Show("Δεν έχετε πραγματοποιήσει κάποια αλλαγή στα στοιχεία του πελάτη.");
            }
            else
            {
                string errorMessages = "";
                if (NameTxt.Text == "" || OccupationTxt.Text == "" || AfmTxt.Text == "" || AddressTxt.Text == "" || RegionTxt.Text == "" || PhoneTxt.Text == "" || Tax_officeTxt.Text == "")
                {
                    errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα απαραίτητα πεδία.\n"; ;
                }
                else
                {
                    errorMessages += chk.CheckAfm(AfmTxt.Text);
                    errorMessages += chk.CheckPhone(PhoneTxt.Text);
                    if (Phone2Txt.Visible == true)
                    {
                        errorMessages += chk.CheckPhone(Phone2Txt.Text);
                    }
                    if (TkTxt.Text != "")
                    {
                        errorMessages += chk.CheckTk(TkTxt.Text);
                    }
                    if (VatTxt.Text != "")
                    {
                        errorMessages += chk.CheckVat(VatTxt.Text);
                    }
                    if (EmailTxt.Text != "")
                    {
                        errorMessages += chk.CheckEmail(EmailTxt.Text);
                    }
                    if (CreditTxt.Text != "")
                    {
                        errorMessages += chk.CheckCredit(CreditTxt.Text);
                    }
                    if (MaxCreditTxt.Text != "")
                    {
                        errorMessages += chk.CheckMaxCredit(MaxCreditTxt.Text);
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
                            SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from Customers where Afm=@afm and Id!=@id", sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"afm", AfmTxt.Text);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Υπάρχει ήδη καταχωρημένος πελάτης με το Α.Φ.Μ. που έχετε εισάγει.");
                            }
                            else
                            {
                                SqlTransaction InsTrans = sqlcon.BeginTransaction("UpdateTransaction");
                                SqlCommand UpdCmd1 = sqlcon.CreateCommand();
                                UpdCmd1.Connection = sqlcon;
                                SqlCommand UpdCmd2 = sqlcon.CreateCommand();
                                UpdCmd2.Connection = sqlcon;
                                SqlCommand UpdCmd3 = sqlcon.CreateCommand();
                                UpdCmd3.Connection = sqlcon;
                                SqlCommand UpdCmd4 = sqlcon.CreateCommand();
                                UpdCmd4.Connection = sqlcon;
                                UpdCmd1.Transaction = InsTrans;
                                UpdCmd2.Transaction = InsTrans;
                                UpdCmd3.Transaction = InsTrans;
                                UpdCmd4.Transaction = InsTrans;
                                try
                                {
                                    UpdCmd1.CommandText = "update Customers set Name=@name, Occupation=@occup, Address = @addr, Tk = @tk, Afm = @afm, Tax_office = @to, Phone = @phone, Email= @email, Credit= @credit , Retail= @ret, Region=@reg where Id=@id";
                                    UpdCmd1.Parameters.AddWithValue("@name", NameTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@occup", OccupationTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@addr", AddressTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@tk", TkTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@afm", AfmTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@to", Tax_officeTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@phone", PhoneTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@email", EmailTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@credit", ((CreditTxt.Text == "") ? "0" : CreditTxt.Text));
                                    UpdCmd1.Parameters.AddWithValue("@ret", ((RetailBox.Checked) ? "1" : "0"));
                                    UpdCmd1.Parameters.AddWithValue("@reg", RegionTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@id", IdTxt.Text);
                                    UpdCmd1.ExecuteNonQuery();
                                    if (NewCustomerList[7]!= "" && CustomerList[7] == "") //phone2
                                    {
                                        UpdCmd2.CommandText = "insert into CustomersPhone2 (Id, CustomerId, Phone2) values((select dbo.nvl(Max(Id) + 1, 0) from CustomersPhone2), @id, @phone2)";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.Parameters.AddWithValue("@phone2", Phone2Txt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }
                                    else if (NewCustomerList[7] == "" && CustomerList[7] != "")
                                    {
                                        UpdCmd2.CommandText = "delete CustomersPhone2 where CustomerId=@id";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }
                                    else if (NewCustomerList[7] != "" && CustomerList[7] != "" && CustomerList[7] != NewCustomerList[7])
                                    {
                                        UpdCmd2.CommandText = "update CustomersPhone2 set Phone2=@phone2 where CustomerId=@id";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.Parameters.AddWithValue("@phone2", Phone2Txt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }


                                    if (NewCustomerList[5] != "" && CustomerList[5] == "") //vat
                                    {
                                        UpdCmd3.CommandText = "insert into CustomersVat (Id,CustomerId,Vat) values ((select dbo.nvl(Max(Id)+1,0) from CustomersVat),@id,@vat)";
                                        UpdCmd3.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd3.Parameters.AddWithValue("@vat", VatTxt.Text);
                                        UpdCmd3.ExecuteNonQuery();
                                    }
                                    else if (NewCustomerList[5] == "" && CustomerList[5] != "")
                                    {
                                        UpdCmd3.CommandText = "delete CustomersVat where CustomerId=@id";
                                        UpdCmd3.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd3.ExecuteNonQuery();
                                    }
                                    else if (NewCustomerList[5] != "" && CustomerList[5] != "" && CustomerList[5] != NewCustomerList[5])
                                    {
                                        UpdCmd3.CommandText = "update CustomersVat set Vat=@vat where CustomerId=@id";
                                        UpdCmd3.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd3.Parameters.AddWithValue("@vat", VatTxt.Text);
                                        UpdCmd3.ExecuteNonQuery();
                                    }

                                    if (NewCustomerList[11] != "" && CustomerList[11] == "") //max credit
                                    {
                                        UpdCmd4.CommandText = "insert into CustomersMaxCredit (Id,CustomerId,MaxCredit) values ((select dbo.nvl(Max(Id)+1,0) from CustomersVat),@id,@maxcredit)";
                                        UpdCmd4.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd4.Parameters.AddWithValue("@maxcredit", MaxCreditTxt.Text);
                                        UpdCmd4.ExecuteNonQuery();
                                    }
                                    else if (NewCustomerList[11] == "" && CustomerList[11] != "")
                                    {
                                        UpdCmd4.CommandText = "delete CustomersMaxCredit where CustomerId=@id";
                                        UpdCmd4.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd4.ExecuteNonQuery();
                                    }
                                    else if (NewCustomerList[11] != "" && CustomerList[11] != "" && CustomerList[11] != NewCustomerList[11])
                                    {
                                        UpdCmd4.CommandText = "update CustomersMaxCredit set MaxCredit=@maxcredit where CustomerId=@id";
                                        UpdCmd4.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd4.Parameters.AddWithValue("@maxcredit", MaxCreditTxt.Text);
                                        UpdCmd4.ExecuteNonQuery();
                                    }
                                    InsTrans.Commit();
                                    MessageBox.Show("Οι αλλαγές πραγματοποιήθηκαν με επιτυχία.");
                                    ClearValues();
                                    NameTxt.Enabled = false;
                                    OccupationTxt.Enabled = false;
                                    AfmTxt.Enabled = false;
                                    AddressTxt.Enabled = false;
                                    RegionTxt.Enabled = false;
                                    TkTxt.Enabled = false;
                                    VatTxt.Enabled = false;
                                    PhoneTxt.Enabled = false;
                                    Phone2Txt.Enabled = false;
                                    EmailTxt.Enabled = false;
                                    Tax_officeTxt.Enabled = false;
                                    CreditTxt.Enabled = false;
                                    MaxCreditTxt.Enabled = false;
                                    RetailBox.Enabled = false;
                                    SearchNameTxt.Enabled = true;
                                    SearchAfmTxt.Enabled = true;
                                    SelectNameCmb.Enabled = true;
                                    RetrieveBtn.Enabled = true;
                                    SaveBtn.Visible = false;
                                    CancelBtn.Visible = false;
                                    CustomerList.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                    try
                                    {
                                        InsTrans.Rollback();
                                    }
                                    catch (Exception ex2)
                                    {
                                        MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του πελάτη. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
        }

        private void CreditTxt_TextChanged(object sender, EventArgs e)
        {
            CreditTxt.Text = CreditTxt.Text.Replace(',', '.');
            CreditTxt.SelectionStart = CreditTxt.Text.Length;
        }

        private void MaxCreditTxt_TextChanged(object sender, EventArgs e)
        {
            MaxCreditTxt.Text = MaxCreditTxt.Text.Replace(',', '.');
            MaxCreditTxt.SelectionStart = MaxCreditTxt.Text.Length;
        }

        private void SearchNameListBox_Leave(object sender, EventArgs e)
        {
            SearchNameListBox.Visible = false;
        }

        private void DocumentLbx_DoubleClick(object sender, EventArgs e)
        {
            if (DocumentLbx.SelectedIndex!=-1)
            {
                string outcome;
                if (InvDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    InvDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewInvoiceCustomer PrevInvoice = new PreviewInvoiceCustomer(outcome);
                    PrevInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevInvoice.ShowDialog();
                }
                else if (DebDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    DebDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDebitInvoiceCustomer PrevDebitInvoice = new PreviewDebitInvoiceCustomer(outcome);
                    PrevDebitInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDebitInvoice.ShowDialog();
                }
                else if (DisDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    DisDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDisNoteCustomer PrevDisNote = new PreviewDisNoteCustomer(outcome);
                    PrevDisNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDisNote.ShowDialog();
                }
                else if (RecDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    RecDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewReceiptCustomer PrevReceiptNote = new PreviewReceiptCustomer(outcome);
                    PrevReceiptNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevReceiptNote.ShowDialog();
                }
            }
        }

        private void SearchNameListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)13)
            {
                SearchNameListBox_Click(null, null);
            }
        }
        

        private void SearchNameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (SearchNameListBox.Visible == true && SearchNameListBox.Items.Count>=1)
                {
                    SearchNameListBox.Focus();
                    SearchNameListBox.SelectedIndex = 0;
                }
            }
        }
    }
}

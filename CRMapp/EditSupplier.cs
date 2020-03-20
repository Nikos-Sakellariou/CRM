using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class EditSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> SupplierList;
        List<string> ls = new List<string>();
        Dictionary<string, string> InvDc = new Dictionary<string, string>();
        Dictionary<string, string> DebDc = new Dictionary<string, string>();
        Dictionary<string, string> DisDc = new Dictionary<string, string>();
        Dictionary<string, string> DisRDc = new Dictionary<string, string>();
        Dictionary<string, string> RecDc = new Dictionary<string, string>();
        Dictionary<string, string> Rec2Dc = new Dictionary<string, string>();

        public EditSupplier()
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
            Tax_officeTxt.Enabled = true;
            PhoneTxt.Enabled = true;
            EmailTxt.Enabled = true;
            DebitTxt.Enabled = true;
            MaxDebitTxt.Enabled = true;
            RegionTxt.Enabled = true;
            Phone2Txt.Enabled = true;
            ManagerTxt.Enabled = true;
            SearchNameTxt.Enabled = false;
            SearchAfmTxt.Enabled = false;
            SelectNameCmb.Enabled = false;
            RetrieveBtn.Enabled = false;
            SaveBtn.Visible = true;
            CancelBtn.Visible = true;
            EditBtn.Visible = false;
            SupplierList = new List<string>(new string[] { NameTxt.Text,
            OccupationTxt.Text,
            AfmTxt.Text,
            Tax_officeTxt.Text,
            AddressTxt.Text,
            TkTxt.Text,
            PhoneTxt.Text,
            EmailTxt.Text,
            DebitTxt.Text,
            RegionTxt.Text,
            ManagerTxt.Text,
            Phone2Txt.Text,
            MaxDebitTxt.Text });
        }
        
        private void ClearValues()
        {
            IdTxt.Text = "";
            NameTxt.Text = "";
            OccupationTxt.Text = "";
            AfmTxt.Text = "";
            AddressTxt.Text = "";
            TkTxt.Text = "";
            Tax_officeTxt.Text = "";
            PhoneTxt.Text = "";
            EmailTxt.Text = "";
            DebitTxt.Text = "";
            MaxDebitTxt.Text = "";
            RegionTxt.Text = "";
            ManagerTxt.Text = "";
            Phone2Txt.Text = "";
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
                                query = "select Id,Name,Occupation,Afm,Address,Tax_office,Debit,Tk,dbo.FVAL_SUPPLIER_MAXDEBIT(Id),Phone,Email,Region,Manager,Phone2 from Suppliers where UPPER(Name)=@parameter";
                                param = SearchNameTxt.Text;
                            }
                            else if (SearchAfmTxt.Text != "")
                            {

                                query = "select Id,Name,Occupation,Afm,Address,Tax_office,Debit,Tk,dbo.FVAL_SUPPLIER_MAXDEBIT(Id),Phone,Email,Region,Manager,Phone2 from Suppliers where Afm=@parameter";
                                param = SearchAfmTxt.Text;
                            }
                            else
                            {
                                query = "select Id,Name,Occupation,Afm,Address,Tax_office,Debit,Tk,dbo.FVAL_SUPPLIER_MAXDEBIT(Id),Phone,Email,Region,Manager,Phone2 from Suppliers where UPPER(Name)=@parameter";
                                param = SelectNameCmb.GetItemText(SelectNameCmb.SelectedItem);
                            }
                            
                            SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", param);
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            if (dt.Rows.Count==0)
                            {
                                MessageBox.Show("Δε βρέθηκε προμηθευτής με τα στοιχεία που εισάγατε.");
                            }
                            else if (dt.Rows.Count>1)
                            {
                                MessageBox.Show("Υπάρχουν "+ dt.Rows.Count+ " καταχωρημένοι προμηθευτές με τα στοιχεία που έχετε εισάγει. Παρακαλώ αλλάξτε τρόπο αναζήτησης.");
                            }
                            else
                            {
                                IdTxt.Text = dt.Rows[0][0].ToString();
                                NameTxt.Text = dt.Rows[0][1].ToString();
                                OccupationTxt.Text = dt.Rows[0][2].ToString();
                                AfmTxt.Text = dt.Rows[0][3].ToString();
                                AddressTxt.Text = dt.Rows[0][4].ToString();
                                Tax_officeTxt.Text = dt.Rows[0][5].ToString();
                                DebitTxt.Text = dt.Rows[0][6].ToString();
                                TkTxt.Text = dt.Rows[0][7].ToString();
                                MaxDebitTxt.Text = dt.Rows[0][8].ToString();
                                PhoneTxt.Text = dt.Rows[0][9].ToString();
                                EmailTxt.Text = dt.Rows[0][10].ToString();
                                RegionTxt.Text = dt.Rows[0][11].ToString();
                                ManagerTxt.Text = dt.Rows[0][12].ToString();
                                Phone2Txt.Text = dt.Rows[0][13].ToString();
                                SearchNameTxt.Text = "";
                                SearchAfmTxt.Text = "";
                                SelectNameCmb.SelectedIndex = -1;
                                EditBtn.Visible = true;
                            }
                            string query2 = @"select distinct Right('          '+InvoiceId,15),Right('          '+InvoiceSeries,8),InvoiceDate,Right('          '+InvoicePrice,18),Prev,Id from
                            (select InvoiceId, InvoiceSeries, InvoiceDate, InvoicePrice,'Invoice' Prev,Id from SupplierInvoice where SupplierId = @id
                            union
                            select DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, DebitInvoicePrice,'DebitInvoice',Id from SupplierDebitInvoice where SupplierId = @id
                            union
                            select DisNoteId, DisNoteSeries, DisNoteDate, '','DisNote',Id from SupplierDisNote where SupplierId = @id
                            union
                            select DisNoteId, DisNoteSeries, DisNoteDate, '','DisNoteReturn',Id from SupplierReturnDisNote where SupplierId = @id
                            union
                            select ReceiptId, ReceiptSeries, ReceiptDate, ReceiptPrice,'Receipt',Id from SupplierReceipt where SupplierId = @id
                            union
                            select ReceiptId, ReceiptSeries, ReceiptDate, ReceiptPrice,'Receipt2',Id from MySupplierReceipt where SupplierId = @id ) a
                            order by InvoiceDate desc";
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                            SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                            DataTable dt2 = new DataTable();
                            SearchAdapt2.Fill(dt2);
                            DocumentLbx.Items.Clear();
                            InvDc.Clear();
                            DebDc.Clear();
                            DisDc.Clear();
                            DisRDc.Clear();
                            RecDc.Clear();
                            Rec2Dc.Clear();
                            for (int i = 0; (i < dt2.Rows.Count); i++)
                            {
                                DateTime dat = Convert.ToDateTime(dt2.Rows[i][2].ToString());
                                DocumentLbx.Items.Add(dt2.Rows[i][0].ToString() + " / " + dt2.Rows[i][1].ToString() + "    -    " + dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year + "    -    " + dt2.Rows[i][3].ToString());
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
                                else if (dt2.Rows[i][4].ToString() == "DisNoteReturn")
                                {
                                    DisRDc.Add(i.ToString(), dt2.Rows[i][5].ToString());
                                }
                                else if (dt2.Rows[i][4].ToString() == "Receipt")
                                {
                                    RecDc.Add(i.ToString(), dt2.Rows[i][5].ToString());
                                }
                                else if (dt2.Rows[i][4].ToString() == "Receipt2")
                                {
                                    Rec2Dc.Add(i.ToString(), dt2.Rows[i][5].ToString());
                                }
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

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text = SupplierList[0];
            OccupationTxt.Text = SupplierList[1];
            AfmTxt.Text = SupplierList[2];
            Tax_officeTxt.Text = SupplierList[3];
            AddressTxt.Text = SupplierList[4];
            TkTxt.Text = SupplierList[5];
            PhoneTxt.Text = SupplierList[6];
            EmailTxt.Text = SupplierList[7];
            DebitTxt.Text = SupplierList[8];
            MaxDebitTxt.Text = SupplierList[9];
            RegionTxt.Text = SupplierList[10];
            ManagerTxt.Text = SupplierList[11];
            Phone2Txt.Text = SupplierList[12];
            NameTxt.Enabled = false;
            OccupationTxt.Enabled = false;
            AfmTxt.Enabled = false;
            AddressTxt.Enabled = false;
            TkTxt.Enabled = false;
            Tax_officeTxt.Enabled = false;
            PhoneTxt.Enabled = false;
            RegionTxt.Enabled = false;
            ManagerTxt.Enabled = false;
            Phone2Txt.Enabled = false;
            EmailTxt.Enabled = false;
            DebitTxt.Enabled = false;
            MaxDebitTxt.Enabled = false;
            SearchNameTxt.Enabled = true;
            SearchAfmTxt.Enabled = true;
            SelectNameCmb.Enabled = true;
            RetrieveBtn.Enabled = true;
            SaveBtn.Visible = false;
            CancelBtn.Visible = false;
            EditBtn.Visible = true;
            SupplierList.Clear();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            List<string> NewSupplierList = new List<string>(new string[] {
                NameTxt.Text,
                OccupationTxt.Text,
                AfmTxt.Text,
                Tax_officeTxt.Text,
                AddressTxt.Text,
                TkTxt.Text,
                PhoneTxt.Text,
                EmailTxt.Text,
                DebitTxt.Text,
                MaxDebitTxt.Text,
                RegionTxt.Text,
                ManagerTxt.Text,
                Phone2Txt.Text });
            int cnt = 0;
            for (int i = 0; i < NewSupplierList.Count; i++)
            {
                if (NewSupplierList[i]!= SupplierList[i])
                {
                    cnt++;
                }
            }
            if (cnt==0)
            {
                MessageBox.Show("Δεν έχετε πραγματοποιήσει κάποια αλλαγή στα στοιχεία του προμηθευτή.");
            }
            else
            {
                string errorMessages = "";
                if (NameTxt.Text == "" || OccupationTxt.Text == "" || AfmTxt.Text == "" || AddressTxt.Text == "" || RegionTxt.Text == "" || TkTxt.Text == "" || PhoneTxt.Text == "" || Tax_officeTxt.Text == "")
                {
                    errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα απαραίτητα πεδία.\n"; ;
                }
                else
                {
                    errorMessages += chk.CheckAfm(AfmTxt.Text);
                    errorMessages += chk.CheckTk(TkTxt.Text);
                    errorMessages += chk.CheckPhone(PhoneTxt.Text);
                    if (Phone2Txt.Text != "")
                    {
                        errorMessages += chk.CheckPhone(Phone2Txt.Text);
                    }
                    if (DebitTxt.Text != "")
                    {
                        errorMessages += chk.CheckDebit(DebitTxt.Text);
                    }
                    if (MaxDebitTxt.Text != "")
                    {
                        errorMessages += chk.CheckMaxDebit(MaxDebitTxt.Text);
                    }
                    if (EmailTxt.Text != "")
                    {
                        errorMessages += chk.CheckEmail(EmailTxt.Text);
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
                            SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from Suppliers where Afm=@afm and Id!=@id", sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"afm", AfmTxt.Text);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Υπάρχει ήδη καταχωρημένος προμηθευτής με το Α.Φ.Μ. που έχετε εισάγει.");
                            }
                            else
                            {
                                SqlTransaction InsTrans = sqlcon.BeginTransaction("UpdateTransaction");
                                SqlCommand UpdCmd1 = sqlcon.CreateCommand();
                                UpdCmd1.Connection = sqlcon;
                                SqlCommand UpdCmd2 = sqlcon.CreateCommand();
                                UpdCmd2.Connection = sqlcon;
                                UpdCmd1.Transaction = InsTrans;
                                UpdCmd2.Transaction = InsTrans;
                                try
                                {
                                    UpdCmd1.CommandText = "update Suppliers set Name=@name, Occupation=@occup, Address = @addr, Tk = @tk, Afm = @afm, Tax_office = @to,  Debit= @debit, Phone = @phone, Email= @email, Region=@reg, Manager=@man, Phone2=@phone2 where Id=@id";
                                    UpdCmd1.Parameters.AddWithValue("@name", NameTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@occup", OccupationTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@addr", AddressTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@tk", TkTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@afm", AfmTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@to", Tax_officeTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@phone", PhoneTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@email", EmailTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@debit", ((DebitTxt.Text == "") ? "0" : DebitTxt.Text));
                                    UpdCmd1.Parameters.AddWithValue("@reg", AfmTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@man", Tax_officeTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@phone2", PhoneTxt.Text);
                                    UpdCmd1.Parameters.AddWithValue("@id", IdTxt.Text);
                                    UpdCmd1.ExecuteNonQuery();

                                    if (NewSupplierList[7]!= "" && SupplierList[7] == "") //max debit
                                    {
                                        UpdCmd2.CommandText = "insert into SuppliersMaxDebit (Id, SupplierId, MaxDebit) values((select dbo.nvl(Max(Id) + 1, 0) from SuppliersMaxDebit), @id, @maxdebit)";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.Parameters.AddWithValue("@maxdebit", MaxDebitTxt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }
                                    else if (NewSupplierList[7] == "" && SupplierList[7] != "")
                                    {
                                        UpdCmd2.CommandText = "delete SuppliersMaxDebit where SupplierId=@id";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }
                                    else if (NewSupplierList[7] != "" && SupplierList[7] != "" && SupplierList[7] != NewSupplierList[7])
                                    {
                                        UpdCmd2.CommandText = "update SuppliersMaxDebit set MaxDebit=@maxdebit where SupplierId=@id";
                                        UpdCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                        UpdCmd2.Parameters.AddWithValue("@maxdebit", MaxDebitTxt.Text);
                                        UpdCmd2.ExecuteNonQuery();
                                    }

                                    
                                    InsTrans.Commit();
                                    MessageBox.Show("Οι αλλαγές πραγματοποιήθηκαν με επιτυχία.");
                                    ClearValues();
                                    NameTxt.Enabled = false;
                                    OccupationTxt.Enabled = false;
                                    AfmTxt.Enabled = false;
                                    AddressTxt.Enabled = false;
                                    TkTxt.Enabled = false;
                                    Tax_officeTxt.Enabled = false;
                                    PhoneTxt.Enabled = false;
                                    RegionTxt.Enabled = false;
                                    ManagerTxt.Enabled = false;
                                    Phone2Txt.Enabled = false;
                                    EmailTxt.Enabled = false;
                                    DebitTxt.Enabled = false;
                                    MaxDebitTxt.Enabled = false;
                                    SearchNameTxt.Enabled = true;
                                    SearchAfmTxt.Enabled = true;
                                    SelectNameCmb.Enabled = true;
                                    RetrieveBtn.Enabled = true;
                                    SaveBtn.Visible = false;
                                    CancelBtn.Visible = false;
                                    SupplierList.Clear();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                                    try
                                    {
                                        InsTrans.Rollback();
                                    }
                                    catch (Exception ex2)
                                    {
                                        MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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

        private void DebitTxt_TextChanged(object sender, EventArgs e)
        {
            DebitTxt.Text = DebitTxt.Text.Replace(',', '.');
            DebitTxt.SelectionStart = DebitTxt.Text.Length;
        }

        private void MaxDebitTxt_TextChanged(object sender, EventArgs e)
        {
            MaxDebitTxt.Text = MaxDebitTxt.Text.Replace(',', '.');
            MaxDebitTxt.SelectionStart = MaxDebitTxt.Text.Length;
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
                if (InvDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    InvDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewInvoiceSupplier PrevInvoice = new PreviewInvoiceSupplier(outcome);
                    PrevInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevInvoice.ShowDialog();
                }
                else if (DebDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    DebDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDebitInvoiceSupplier PrevDebitInvoice = new PreviewDebitInvoiceSupplier(outcome);
                    PrevDebitInvoice.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDebitInvoice.ShowDialog();
                }
                else if (DisDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    DisDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDisNoteSupplier PrevDisNote = new PreviewDisNoteSupplier(outcome);
                    PrevDisNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDisNote.ShowDialog();
                }
                else if (DisRDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    DisRDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewDisNoteReturnSupplier PrevDisNote = new PreviewDisNoteReturnSupplier(outcome);
                    PrevDisNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevDisNote.ShowDialog();
                }
                else if (RecDc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    RecDc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewReceiptSupplier PrevReceiptNote = new PreviewReceiptSupplier(outcome);
                    PrevReceiptNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevReceiptNote.ShowDialog();
                }
                else if (Rec2Dc.ContainsKey(DocumentLbx.SelectedIndex.ToString()))
                {
                    Rec2Dc.TryGetValue(DocumentLbx.SelectedIndex.ToString(), out outcome);
                    PreviewReceiptToSupplier PrevReceiptNote = new PreviewReceiptToSupplier(outcome);
                    PrevReceiptNote.FormClosed += new FormClosedEventHandler(FormClose);
                    this.Enabled = false;
                    PrevReceiptNote.ShowDialog();
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

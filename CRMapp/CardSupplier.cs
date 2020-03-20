using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace CRMapp
{

    public partial class CardSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> ls = new List<string>();
        List<string[]> lsview = new List<string[]>();
        Image InvoicePlainImg = Properties.Resources.InvPlain;
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Far};
        int pagecount = 1;
        int AA = 1;

        public CardSupplier()
        {
            con = chk.Initiallize_con();
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

        private void RetrieveBtn_Click(object sender, EventArgs e)
        {
            if (SearchNameTxt.Text == "" && SearchAfmTxt.Text == "" && SelectNameCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Θα πρέπει πρώτα να εισάγετε Επωνυμία ή Α.Φ.Μ. του προμηθευτή.");
            }
            else if (Convert.ToDateTime(DateFrom.Value.ToShortDateString())> Convert.ToDateTime(DateTo.Value.ToShortDateString()) || Convert.ToDateTime(DateFrom.Value.ToShortDateString()) > Convert.ToDateTime(DateTime.Today.ToShortDateString()) || Convert.ToDateTime(DateTo.Value.ToShortDateString()) > Convert.ToDateTime(DateTime.Today.ToShortDateString()))
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε έγκυρες ημερομηνίες.");
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
                                query = "select Id,Name,Afm from Suppliers where UPPER(Name)=@parameter";
                                param = SearchNameTxt.Text;
                            }
                            else if (SearchAfmTxt.Text != "")
                            {

                                query = "select Id,Name,Afm from Suppliers where Afm=@parameter";
                                param = SearchAfmTxt.Text;
                            }
                            else
                            {
                                query = "select Id,Name,Afm from Suppliers where UPPER(Name)=@parameter";
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
                                MessageBox.Show("Υπάρχουν "+ dt.Rows.Count+" καταχωρημένοι προμηθευτές με τα στοιχεία που έχετε εισάγει. Παρακαλώ αλλάξτε τρόπο αναζήτησης.");
                            }
                            else
                            {
                                IdTxt.Text = dt.Rows[0][0].ToString();
                                NameTxt.Text = dt.Rows[0][1].ToString();
                                AfmTxt.Text = dt.Rows[0][2].ToString();

                                decimal InitialDebit=0;
                                string query4 = @"select dbo.nvl(cast(Debit as decimal(18,2))-POS,0.00) from Suppliers b,(select dbo.nvl(sum(POS),0.00) as POS from
                                (select sum(cast(InvoicePrice as decimal(18,2))) as POS from SupplierInvoice where SupplierId = @id and InvoiceDate>=cast(@datefrom as Date)
                                union
                                select -sum(cast(DebitInvoicePrice as decimal(18,2))) from SupplierDebitInvoice where SupplierId = @id and DebitInvoiceDate>=cast(@datefrom as Date)
                                union
                                select -sum(cast(Price as decimal(18,2))) from SupplierBankDeposits where SupplierId = @id  and Date>=cast(@datefrom as Date)
                                union
                                select -sum(cast(ReceiptPrice as decimal(18,2))) from SupplierReceipt where SupplierId = @id  and ReceiptDate>=cast(@datefrom as Date)
                                union
                                select -sum(cast(ReceiptPrice as decimal(18,2))) from MySupplierReceipt where SupplierId = @id  and ReceiptDate>=cast(@datefrom as Date)) a) c
								where b.Id = @id";
                                SqlDataAdapter SearchAdapt4 = new SqlDataAdapter(query4, sqlcon);
                                SearchAdapt4.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                                SearchAdapt4.SelectCommand.Parameters.AddWithValue(@"datefrom", DateFrom.Value.ToShortDateString());
                                DataTable dt4 = new DataTable();
                                SearchAdapt4.Fill(dt4);
                                if (dt4.Rows.Count==1)
                                {
                                    InitialDebit = Convert.ToDecimal(dt4.Rows[0][0].ToString());
                                }

                                string query2 = @"select distinct InvoiceId,InvoiceSeries,InvoiceDate,InvoicePrice,Prev,Id from
                                (select InvoiceId, InvoiceSeries, InvoiceDate, InvoicePrice,'Invoice' Prev,Id from SupplierInvoice where SupplierId = @id
                                union
                                select DebitInvoiceId, DebitInvoiceSeries, DebitInvoiceDate, DebitInvoicePrice,'DebitInvoice',Id from SupplierDebitInvoice where SupplierId = @id
                                union
                                select Doc, '', Date, Price,'BankDeposits',Id from SupplierBankDeposits where SupplierId = @id
                                union
                                select ReceiptId, ReceiptSeries, ReceiptDate, ReceiptPrice,'Receipt',Id from SupplierReceipt where SupplierId = @id 
                                union
                                select ReceiptId, ReceiptSeries, ReceiptDate, ReceiptPrice,'Receipt',Id from MySupplierReceipt where SupplierId = @id ) a
                                where a.InvoiceDate>=cast(@datefrom as Date) and a.InvoiceDate<=cast(@dateto as Date)
                                order by InvoiceDate";
                                SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                                SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                                SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"datefrom", DateFrom.Value.ToShortDateString());
                                SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"dateto", DateTo.Value.ToShortDateString());
                                DataTable dt2 = new DataTable();
                                SearchAdapt2.Fill(dt2);
                                CardSupplierLst.Items.Clear();
                                lsview.Clear();
                                string[] Lbl = new string[] {
                                                "",
                                                "",
                                                "Ημ. λήξης αξιογράφου     -     Αξία",
                                                "",
                                                "",
                                                ""
                                                };
                                List<string[]> DataLst = new List<string[]>();
                                decimal PriceT = InitialDebit;
                                string[] mtf = new string[] {
                                                "",
                                                "",
                                                "*** ΑΠΟ ΜΕΤΑΦΟΡΑ ΤΗΝ "+Convert.ToDateTime(DateFrom.Value).Day+"/"+Convert.ToDateTime(DateFrom.Value).Month+"/"+Convert.ToDateTime(DateFrom.Value).Year+" ***",
                                                "",
                                                "",
                                                InitialDebit.ToString()
                                                };
                                lsview.Add(mtf);
                                CardSupplierLst.Items.Add(new ListViewItem(mtf));
                                for (int i = 0; (i < dt2.Rows.Count); i++)
                                {
                                    DataLst.Clear();
                                    DateTime dat = Convert.ToDateTime(dt2.Rows[i][2].ToString());
                                    string Reason = "";
                                    string PriceX = "0";
                                    string PriceP = "0";
                                    if (dt2.Rows[i][4].ToString() == "Invoice")
                                    {
                                        Reason="ΤΙΜΟΛΟΓΙΟ ΠΩΛΗΣΗΣ";
                                        PriceP = dt2.Rows[i][3].ToString();
                                    }
                                    else if (dt2.Rows[i][4].ToString() == "DebitInvoice")
                                    {
                                        Reason = "ΠΙΣΤΩΤΙΚΟ ΤΙΜΟΛΟΓΙΟ";
                                        PriceX = dt2.Rows[i][3].ToString();
                                    }
                                    else if (dt2.Rows[i][4].ToString() == "Receipt")
                                    {
                                        Reason = "ΠΛΗΡΩΜΗ ΜΕΤΡΗΤΩΝ/ΠΑΡΑΔΟΣΗ ΑΞΙΟΓΡΑΦΩΝ";
                                        string query3 = @"select ValueDocId,ValueDocDate,ValueDocPrice from ReceiptValueDocs where ReceiptKind='1' and ReceiptId=@id";
                                        SqlDataAdapter SearchAdapt3 = new SqlDataAdapter(query3, sqlcon);
                                        SearchAdapt3.SelectCommand.Parameters.AddWithValue(@"id", dt2.Rows[i][5].ToString());
                                        DataTable dt3 = new DataTable();
                                        SearchAdapt3.Fill(dt3);
                                        for (int j = 0; (j < dt3.Rows.Count); j++)
                                        {
                                            string[] datals = new string[] {
                                            "",
                                            "",
                                            Convert.ToDateTime(dt3.Rows[j][1].ToString()).Day + "/" + Convert.ToDateTime(dt3.Rows[j][1].ToString()).Month + "/" + Convert.ToDateTime(dt3.Rows[j][1].ToString()).Year + "                        -     " + dt3.Rows[j][2].ToString(),
                                            "",
                                            "",
                                            ""
                                            };
                                            DataLst.Add(datals);
                                        }
                                        PriceX = dt2.Rows[i][3].ToString();
                                    }
                                    else if (dt2.Rows[i][4].ToString() == "BankDeposits")
                                    {
                                        Reason = "ΕΜΒΑΣΜΑ ΣΕ ΤΡΑΠΕΖΑ";
                                        PriceX = dt2.Rows[i][3].ToString();
                                    }

                                    PriceT = PriceT + Convert.ToDecimal(PriceP) - Convert.ToDecimal(PriceX);

                                    string[] data = new string[] {
                                            dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year,
                                            dt2.Rows[i][0].ToString() + "/" + dt2.Rows[i][1].ToString(),
                                            Reason,
                                            PriceX,
                                            PriceP,
                                            PriceT.ToString()
                                            };
                                    lsview.Add(data);
                                    CardSupplierLst.Items.Add(new ListViewItem(data));
                                    if (DataLst.Count()>0)
                                    {
                                        lsview.Add(Lbl);
                                        CardSupplierLst.Items.Add(new ListViewItem(Lbl));
                                        foreach (var item in DataLst)
                                        {
                                            lsview.Add(item);
                                            CardSupplierLst.Items.Add(new ListViewItem(item));
                                        }
                                    }
                                }
                                string[] mtf2 = new string[] {
                                                "",
                                                "",
                                                "*** ΣΕ ΜΕΤΑΦΟΡΑ ΤΗΝ "+Convert.ToDateTime(DateTo.Value).Day+"/"+Convert.ToDateTime(DateTo.Value).Month+"/"+Convert.ToDateTime(DateTo.Value).Year+" ***",
                                                "",
                                                "",
                                                PriceT.ToString()
                                            };
                                lsview.Add(mtf2);
                                CardSupplierLst.Items.Add(new ListViewItem(mtf2));
                                BankDepositPanel.Enabled = true;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση της καρτέλας του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
                
        private void BankDepositBtn_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(BankDepositPck.Value.ToShortDateString()) > Convert.ToDateTime(DateTime.Today.ToShortDateString()))
            {
                System.Windows.MessageBox.Show("Η ημερομηνία δεν μπορεί να είναι μεταγενέστερη της σημερινής.");
            }
            else if (chk.CheckPrice(BankDepositPriceTxt.Text) != "")
            {
                System.Windows.MessageBox.Show("Θα πρέπει να συμπληρώσετε ένα έγκυρο ποσοστό εμβάσματος.");
            }
            else if (IdTxt.Text == "")
            {
                System.Windows.MessageBox.Show("Θα πρέπει να επιλέξετε προμηθευτή.");
            }
            else
            {
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        DataTable dt = new DataTable();
                        string query = "select Debit from Suppliers where Id=@id";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", IdTxt.Text);
                        SearchAdapt.Fill(dt);
                        SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                        SqlCommand InsCmd1 = sqlcon.CreateCommand();
                        InsCmd1.Connection = sqlcon;
                        SqlCommand InsCmd2 = sqlcon.CreateCommand();
                        InsCmd2.Connection = sqlcon;
                        InsCmd1.Transaction = InsTrans;
                        InsCmd2.Transaction = InsTrans;
                        try
                        {
                            InsCmd1.CommandText = "insert into SupplierBankDeposits (Id, SupplierId,Doc, Date, Price) values((select dbo.nvl(Max(Id)+1,0) from dbo.SupplierBankDeposits), @supid, @doc, @date, @price)";
                            InsCmd1.Parameters.AddWithValue("@supid", IdTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@doc", BankDepositDocTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@date", BankDepositPck.Value);
                            InsCmd1.Parameters.AddWithValue("@price", BankDepositPriceTxt.Text);
                            InsCmd1.ExecuteNonQuery();

                            if (dt.Rows.Count == 1)
                            {
                                decimal oldDebit = Convert.ToDecimal(dt.Rows[0][0].ToString());
                                decimal newDebit = oldDebit - Convert.ToDecimal(BankDepositPriceTxt.Text);
                                InsCmd2.CommandText = "update Suppliers set Debit= @debit where Id=@id";
                                InsCmd2.Parameters.AddWithValue("@debit", newDebit);
                                InsCmd2.Parameters.AddWithValue("@id", IdTxt.Text);
                                InsCmd2.ExecuteNonQuery();
                            }
                            InsTrans.Commit();
                            System.Windows.MessageBox.Show("Το έμβασμα του προμηθευτή καταχωρήθηκε με επιτυχία.");
                            BankDepositPck.Value = System.DateTime.Today;
                            BankDepositDocTxt.Text = "";
                            BankDepositPriceTxt.Text = "";
                            RetrieveBtn_Click(null, null);
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                            try
                            {
                                InsTrans.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                System.Windows.MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
        
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            pagecount = 1;
            AA = 1;
            PreviewDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
            PrintPrev.Document = PreviewDoc;
            ((Form)PrintPrev).StartPosition = FormStartPosition.CenterScreen;
            PrintPrev.ShowDialog();
            /*
            PrintDial.Document = PrintDoc;
            PrintDial.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
            PrintDial.PrinterSettings.Copies = 2;
            DialogResult result = PrintDial.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintDoc.Print();
            }
            */
        }

        private void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(InvoicePlainImg, 0, 0, InvoicePlainImg.Width, InvoicePlainImg.Height);
            if (CardSupplierLst.Items.Count > 0)
            {
                PrintDoc_Elements(e);
            }
        }

        private void PreviewDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(InvoicePlainImg, 0, 0, InvoicePlainImg.Width, InvoicePlainImg.Height);
            if (CardSupplierLst.Items.Count > 0)
            {
                PrintDoc_Elements(e);
            }
        }

        private void PrintDoc_Elements(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(25,25,1044,50));
            e.Graphics.DrawString("Καρτέλα Κινήσεων Προμηθευτών", new Font("Arial", 16, System.Drawing.FontStyle.Bold), Brushes.Black, 400, 35);
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(25, 75, 120, 50));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(145, 75, 924, 50));
            e.Graphics.DrawString("Κωδικός", new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, 35, 85);
            e.Graphics.DrawString("Επωνυμία Προμηθευτή", new Font("Arial", 14, System.Drawing.FontStyle.Bold), Brushes.Black, 155, 85);
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(25, 125, 120, 50));
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(145, 125, 924, 50));
            e.Graphics.DrawString(IdTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 35, 135);
            e.Graphics.DrawString(NameTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 155, 135);
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(25, 175, 50, 50)); //Α/Α
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(75, 175, 110, 50)); //ΗΜΕΡ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(185, 175, 150, 50)); //ΠΑΡΑΣΤ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(335, 175, 404, 50)); //ΑΙΤΙΟΛ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(739, 175, 110, 50)); //ΧΡΕΩΣΗ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(849, 175, 110, 50)); //ΠΙΣΤΩΣΗ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(959, 175, 110, 50)); //ΥΠΟΛ
            e.Graphics.DrawString("Α/Α", new Font("Arial", 13, System.Drawing.FontStyle.Regular), Brushes.Black, 35, 185);
            e.Graphics.DrawString("ΗΜΕΡ/ΝΙΑ", new Font("Arial", 13, System.Drawing.FontStyle.Regular), Brushes.Black, 85, 185);
            e.Graphics.DrawString("ΠΑΡΑΣΤΑΤΙΚΟ", new Font("Arial", 13, System.Drawing.FontStyle.Regular), Brushes.Black, 195, 185);
            e.Graphics.DrawString("ΑΙΤΙΟΛΟΓΙΑ", new Font("Arial", 13, System.Drawing.FontStyle.Regular), Brushes.Black, 480, 185);
            e.Graphics.DrawString("ΧΡΕΩΣΗ", new Font("Arial", 13, System.Drawing.FontStyle.Regular), Brushes.Black, 745, 185);
            e.Graphics.DrawString("ΠΙΣΤΩΣΗ", new Font("Arial", 13, System.Drawing.FontStyle.Regular), Brushes.Black, 855, 185);
            e.Graphics.DrawString("ΠΙΣΤ.ΥΠΟΛ.", new Font("Arial", 13, System.Drawing.FontStyle.Regular), Brushes.Black, 963, 185);
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(25, 225, 50, 1250)); //Α/Α
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(75, 225, 110, 1250)); //ΗΜΕΡ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(185, 225, 150, 1250)); //ΠΑΡΑΣΤ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(335, 225, 404, 1250)); //ΑΙΤΙΟΛ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(739, 225, 110, 1250)); //ΧΡΕΩΣΗ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(849, 225, 110, 1250)); //ΠΙΣΤΩΣΗ
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(959, 225, 110, 1250)); //ΥΠΟΛ
            bool hasmorepages = false;
            int rest = lsview.Count+(1-pagecount)*36;
            int cnt = (pagecount-1) * 36;
            for (int i = 0; i < rest; i++)
            {
                var item = lsview[i+cnt];
                if (i == 36)
                {
                    hasmorepages = true;
                    break;
                }
                if (item[0]=="" && item[2].Substring(0,1)=="*")
                {
                    e.Graphics.DrawString(item[2], new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 345, 235 + i * 30);
                    e.Graphics.DrawString(chk.GrNumber(item[5]), new Font("Arial", 11, System.Drawing.FontStyle.Bold), Brushes.Black, new Rectangle(965, 235 + i * 30, 90, 24), format);
                }
                else if (item[2].Substring(0, 3) == "Ημ.")
                {
                    e.Graphics.DrawString(item[2], new Font("Arial", 11, System.Drawing.FontStyle.Underline), Brushes.Black, 345, 235 + i * 30);
                }
                else if (item[0] == "")
                {
                    e.Graphics.DrawString("  "+item[2], new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, 345, 235 + i * 30);
                }
                else
                {
                    e.Graphics.DrawString(AA.ToString(), new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, 35, 235 + i * 30);
                    e.Graphics.DrawString(item[0], new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, 85, 235 + i * 30);
                    e.Graphics.DrawString(item[1], new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, 195, 235 + i * 30);
                    e.Graphics.DrawString(item[2], new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, 345, 235 + i * 30);
                    e.Graphics.DrawString(chk.GrNumber(item[3]), new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(745, 235 + i * 30,90,24), format);
                    e.Graphics.DrawString(chk.GrNumber(item[4]), new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(855, 235 + i * 30, 90, 24), format);
                    e.Graphics.DrawString(chk.GrNumber(item[5]), new Font("Arial", 11, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(965, 235 + i * 30, 90, 24), format);
                    AA++;
                }
            }
            //ΕΚΤΥΠΩΣΗ ΑΡΙΘΜΙΣΗΣ ΣΕΛΙΔΩΝ 
            e.Graphics.DrawString("Σελίδα " + pagecount + " από " + (lsview.Count / 36 + 1), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 900, 1490);
            pagecount++;
            e.Graphics.DrawString(DateTime.Now.Day.ToString()+"/"+ DateTime.Now.Month.ToString()+"/"+ DateTime.Now.Year.ToString()+"   "+ DateTime.Now.ToShortTimeString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 55, 1490);
            if (hasmorepages == false)
            {
                pagecount = 1;
                AA = 1;
                GC.Collect();
            }
            e.HasMorePages = hasmorepages;
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

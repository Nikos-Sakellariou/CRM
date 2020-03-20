using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRMapp
{

    public partial class SalesPrint : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> ls = new List<string>();
        Dictionary<string, string> dc = new Dictionary<string, string>();
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
        double sumquant = 0;
        decimal sumval = 0;
        decimal sumdisc = 0;
        decimal sumvat = 0;
        int pagecount = 1;
        int proditems = 1;
        System.Drawing.Image InvoiceDrawImg;
        System.Drawing.Image InvoicePlainImg;
        private string DocName;
        private string DocId;
        private string DocIdId;
        private string DocSeries;
        private string DocDate;
        private string DocPayment;
        private string DocDisc;
        private string DocPrice;
        private string DocDisNotes;
        private string DocFrom;
        private string DocTo;
        private string DocReason;
        private string ReceiptCash;
        private string CustomerId;
        private string CustomerName;
        private string CustomerAfm;
        private string CustomerOccupation;
        private string CustomerAddress;
        private string CustomerTk;
        private string CustomerTax_office;
        private string CustomerPhone;
        private string CustomerEmail;
        private string CustomerPhone2;
        private string CustomerRegion;
        private DataTable DocProds=new DataTable();

        public SalesPrint()
        {
            con = chk.Initiallize_con();
            InitializeComponent();

        }
        
        private void RetrieveBtn_Click(object sender, EventArgs e)
        {
            if (SelectNameCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Θα πρέπει πρώτα να επιλέξετε το είδος του παραστατικού.");
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
                            string query = "";
                            if (SelectNameCmb.SelectedIndex == 0)
                            {
                                query = "select InvoiceId,InvoiceSeries,InvoiceDate,Id from CustomerInvoice where InvoiceDate>=@datefrom and InvoiceDate<=@dateto order by InvoiceId,InvoiceSeries";
                            }
                            else if (SelectNameCmb.SelectedIndex == 1)
                            {
                                query = "select DisNoteId,DisNoteSeries,DisNoteDate,Id from CustomerDisNote where DisNoteDate>=@datefrom and DisNoteDate<=@dateto order by DisNoteId,DisNoteSeries";
                            }
                            else if (SelectNameCmb.SelectedIndex == 2)
                            {
                                query = "select DebitInvoiceId,DebitInvoiceSeries,DebitInvoiceDate,Id from CustomerDebitInvoice where DebitInvoiceDate>=cast(@datefrom as Date) and DebitInvoiceDate<=cast(@dateto as Date) order by DebitInvoiceId,DebitInvoiceSeries";
                            }
                            else if (SelectNameCmb.SelectedIndex == 3)
                            {
                                query = "select ReceiptId,ReceiptSeries,ReceiptDate,Id from CustomerReceipt where ReceiptDate>=cast(@datefrom as Date) and ReceiptDate<=cast(@dateto as Date) order by ReceiptId,ReceiptSeries";
                            }
                            else if (SelectNameCmb.SelectedIndex == 4)
                            {
                                query = "select ReceiptId,ReceiptSeries,ReceiptDate,Id from MySupplierReceipt where ReceiptDate>=cast(@datefrom as Date) and ReceiptDate<=cast(@dateto as Date) order by ReceiptId,ReceiptSeries";
                            }
                            else if (SelectNameCmb.SelectedIndex == 5)
                            {
                                query = "select DisNoteId,DisNoteSeries,DisNoteDate,Id from SupplierReturnDisNote where DisNoteDate>=cast(@datefrom as Date) and DisNoteDate<=cast(@dateto as Date) order by DisNoteId,DisNoteSeries";
                            }
                            SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"datefrom", DateFrom.Value.ToShortDateString());
                            SearchAdapt.SelectCommand.Parameters.AddWithValue(@"dateto", DateTo.Value.ToShortDateString());
                            DataTable dt = new DataTable();
                            SearchAdapt.Fill(dt);
                            DocumentsLbx.Items.Clear();
                            dc.Clear();
                            for (int i = 0; (i < dt.Rows.Count && i < 41); i++)
                            {
                                DateTime dat = Convert.ToDateTime(dt.Rows[i][2].ToString());
                                DocumentsLbx.Items.Add(dt.Rows[i][0].ToString() + " / " + dt.Rows[i][1].ToString() + "      - " + dat.Date.Day + "/" + dat.Date.Month + "/" + dat.Date.Year);
                                dc.Add(i.ToString(), dt.Rows[i][3].ToString());
                                PrintBtn.Enabled = true;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση των παραστατικών. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
        
        private void PrintPreviewChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PrintPreviewChkBox.Checked)
            {
                PrintChkBox.Checked = false;
            }
        }

        private void PrintChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PrintChkBox.Checked)
            {
                PrintPreviewChkBox.Checked = false;
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (DocumentsLbx.SelectedItems.Count == 0)
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε ένα τουλάχιστον παραστατικό.");
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
                            string DocumId;
                            dc.TryGetValue(DocumentsLbx.Items.IndexOf(DocumentsLbx.SelectedItem).ToString(), out DocumId);
                            string query = "";
                            if (SelectNameCmb.SelectedIndex == 0)
                            {
                                DocName = "Τιμολόγιο - Δελτίο Αποστολής";
                                DocFrom = "Έδρα μας";
                                DocTo = "Έδρα πελάτη";
                                DocReason = "Πώληση";
                                query = "select a.Id,InvoiceId,InvoiceSeries,InvoiceDate,InvoicePayment,InvoiceDisc,InvoicePrice,InvoiceDisNotes,b.Id,Name,Occupation,Address,Tk,Afm,Tax_office,Phone,Email,dbo.FVAL_CUSTOMER_PHONE2(b.Id),Region from CustomerInvoice a,Customers b where a.Id=@id and CustomerId=b.Id";
                                SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                DataTable dt = new DataTable();
                                SearchAdapt.Fill(dt);
                                if (dt.Rows.Count == 1)
                                {
                                    DocId = dt.Rows[0][0].ToString();
                                    DocIdId = dt.Rows[0][1].ToString();
                                    DocSeries = dt.Rows[0][2].ToString();
                                    DocDate = dt.Rows[0][3].ToString();
                                    DocPayment = dt.Rows[0][4].ToString();
                                    DocDisc = dt.Rows[0][5].ToString();
                                    DocPrice = dt.Rows[0][6].ToString();
                                    DocDisNotes = dt.Rows[0][7].ToString();
                                    CustomerId = dt.Rows[0][8].ToString();
                                    CustomerName = dt.Rows[0][9].ToString();
                                    CustomerOccupation = dt.Rows[0][10].ToString();
                                    CustomerAddress = dt.Rows[0][11].ToString();
                                    CustomerTk = dt.Rows[0][12].ToString();
                                    CustomerAfm = dt.Rows[0][13].ToString();
                                    CustomerTax_office = dt.Rows[0][14].ToString();
                                    CustomerPhone = dt.Rows[0][15].ToString();
                                    CustomerEmail = dt.Rows[0][16].ToString();
                                    CustomerPhone2 = dt.Rows[0][17].ToString();
                                    CustomerRegion = dt.Rows[0][18].ToString();
                                    string query2 = "select ProductId,Description,ProductQuant,ProductPrice,ProductDisc,Unit from CustomerInvoiceProducts a,Products b where a.ProductId=b.Id and CustomerInvoiceId=@id order by a.Id";
                                    SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                                    SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                    SearchAdapt2.Fill(DocProds);
                                    proditems = DocProds.Rows.Count;
                                    InvoiceDrawImg=Properties.Resources.InvDraw;
                                    InvoicePlainImg= Properties.Resources.InvPlain;
                                }
                                if (DocDisNotes.Length>2)
                                {
                                    DocName = "Τιμολόγιο";
                                    DocFrom = "";
                                    DocTo = "";
                                }
                            }
                            else if (SelectNameCmb.SelectedIndex == 1)
                            {
                                DocName = "Δελτίο Αποστολής";
                                DocFrom = "Έδρα μας";
                                DocTo = "Έδρα πελάτη";
                                DocReason = "Πώληση";
                                query = "select a.Id,DisNoteId,DisNoteSeries,DisNoteDate,'','','','',b.Id,Name,Occupation,Address,Tk,Afm,Tax_office,Phone,Email,dbo.FVAL_CUSTOMER_PHONE2(b.Id),Region from CustomerDisNote a,Customers b where a.Id=@id and CustomerId=b.Id";
                                SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                DataTable dt = new DataTable();
                                SearchAdapt.Fill(dt);
                                if (dt.Rows.Count == 1)
                                {
                                    DocId = dt.Rows[0][0].ToString();
                                    DocIdId = dt.Rows[0][1].ToString();
                                    DocSeries = dt.Rows[0][2].ToString();
                                    DocDate = dt.Rows[0][3].ToString();
                                    DocPayment = dt.Rows[0][4].ToString();
                                    DocDisc = dt.Rows[0][5].ToString();
                                    DocPrice = dt.Rows[0][6].ToString();
                                    DocDisNotes = dt.Rows[0][7].ToString();
                                    CustomerId = dt.Rows[0][8].ToString();
                                    CustomerName = dt.Rows[0][9].ToString();
                                    CustomerOccupation = dt.Rows[0][10].ToString();
                                    CustomerAddress = dt.Rows[0][11].ToString();
                                    CustomerTk = dt.Rows[0][12].ToString();
                                    CustomerAfm = dt.Rows[0][13].ToString();
                                    CustomerTax_office = dt.Rows[0][14].ToString();
                                    CustomerPhone = dt.Rows[0][15].ToString();
                                    CustomerEmail = dt.Rows[0][16].ToString();
                                    CustomerPhone2 = dt.Rows[0][17].ToString();
                                    CustomerRegion = dt.Rows[0][18].ToString();
                                    string query2 = "select ProductId,Description,ProductQuant,'','',Unit from CustomerDisNoteProducts a,Products b where a.ProductId=b.Id and CustomerDisNoteId=@id order by a.Id";
                                    SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                                    SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                    SearchAdapt2.Fill(DocProds);
                                    proditems = DocProds.Rows.Count;
                                    InvoiceDrawImg = Properties.Resources.InvDraw;
                                    InvoicePlainImg = Properties.Resources.InvPlain;
                                }
                            }
                            else if (SelectNameCmb.SelectedIndex == 2)
                            {
                                DocName = "Πιστωτικό Τιμολόγιο";
                                DocFrom = "Έδρα πελάτη";
                                DocTo = "Έδρα μας";
                                DocReason = "Επιστροφή";
                                query = "select a.Id,DebitInvoiceId,DebitInvoiceSeries,DebitInvoiceDate,DebitInvoicePayment,DebitInvoiceDisc,DebitInvoicePrice,DebitInvoiceDisNotes,b.Id,Name,Occupation,Address,Tk,Afm,Tax_office,Phone,Email,dbo.FVAL_CUSTOMER_PHONE2(b.Id), Region from CustomerDebitInvoice a,Customers b where a.Id=@id and CustomerId=b.Id";
                                SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                DataTable dt = new DataTable();
                                SearchAdapt.Fill(dt);
                                if (dt.Rows.Count == 1)
                                {
                                    DocId = dt.Rows[0][0].ToString();
                                    DocIdId = dt.Rows[0][1].ToString();
                                    DocSeries = dt.Rows[0][2].ToString();
                                    DocDate = dt.Rows[0][3].ToString();
                                    DocPayment = dt.Rows[0][4].ToString();
                                    DocDisc = dt.Rows[0][5].ToString();
                                    DocPrice = dt.Rows[0][6].ToString();
                                    DocDisNotes = dt.Rows[0][7].ToString();
                                    CustomerId = dt.Rows[0][8].ToString();
                                    CustomerName = dt.Rows[0][9].ToString();
                                    CustomerOccupation = dt.Rows[0][10].ToString();
                                    CustomerAddress = dt.Rows[0][11].ToString();
                                    CustomerTk = dt.Rows[0][12].ToString();
                                    CustomerAfm = dt.Rows[0][13].ToString();
                                    CustomerTax_office = dt.Rows[0][14].ToString();
                                    CustomerPhone = dt.Rows[0][15].ToString();
                                    CustomerEmail = dt.Rows[0][16].ToString();
                                    CustomerPhone2 = dt.Rows[0][17].ToString();
                                    CustomerRegion = dt.Rows[0][18].ToString();
                                    string query2 = "select ProductId,Description,ProductQuant,ProductPrice,ProductDisc,Unit from CustomerDebitInvoiceProducts a,Products b where a.ProductId=b.Id and CustomerDebitInvoiceId=@id order by a.Id";
                                    SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                                    SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                    SearchAdapt2.Fill(DocProds);
                                    proditems = DocProds.Rows.Count;
                                    InvoiceDrawImg = Properties.Resources.InvDraw;
                                    InvoicePlainImg = Properties.Resources.InvPlain;
                                }
                            }
                            else if (SelectNameCmb.SelectedIndex == 3)
                            {
                                DocName = "Απόδειξη Είσπραξης Μετρητών";
                                DocFrom = "";
                                DocTo = "";
                                DocReason = "";
                                query = "select a.Id,ReceiptId,ReceiptSeries,ReceiptDate,'','',ReceiptPrice,'',b.Id,Name,Occupation,Address,Tk,Afm,Tax_office,Phone,Email,dbo.FVAL_CUSTOMER_PHONE2(b.Id),Region, ReceiptCash from CustomerReceipt a,Customers b where a.Id=@id and CustomerId=b.Id";
                                SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                DataTable dt = new DataTable();
                                SearchAdapt.Fill(dt);
                                if (dt.Rows.Count == 1)
                                {
                                    DocId = dt.Rows[0][0].ToString();
                                    DocIdId = dt.Rows[0][1].ToString();
                                    DocSeries = dt.Rows[0][2].ToString();
                                    DocDate = dt.Rows[0][3].ToString();
                                    DocPayment = dt.Rows[0][4].ToString();
                                    DocDisc = dt.Rows[0][5].ToString();
                                    DocPrice = dt.Rows[0][6].ToString();
                                    DocDisNotes = dt.Rows[0][7].ToString();
                                    CustomerId = dt.Rows[0][8].ToString();
                                    CustomerName = dt.Rows[0][9].ToString();
                                    CustomerOccupation = dt.Rows[0][10].ToString();
                                    CustomerAddress = dt.Rows[0][11].ToString();
                                    CustomerTk = dt.Rows[0][12].ToString();
                                    CustomerAfm = dt.Rows[0][13].ToString();
                                    CustomerTax_office = dt.Rows[0][14].ToString();
                                    CustomerPhone = dt.Rows[0][15].ToString();
                                    CustomerEmail = dt.Rows[0][16].ToString();
                                    CustomerPhone2 = dt.Rows[0][17].ToString();
                                    CustomerRegion = dt.Rows[0][18].ToString();
                                    ReceiptCash=dt.Rows[0][19].ToString();
                                    string query2 = "select ValueDocId,ValueDocDate,ValueDocPrice,ValueDocIssuer from ReceiptValueDocs where ReceiptKind='1' and ReceiptId=@id order by Id";
                                    SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                                    SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                    SearchAdapt2.Fill(DocProds);
                                    proditems = DocProds.Rows.Count;
                                    InvoiceDrawImg = Properties.Resources.RecDraw;
                                    InvoicePlainImg = Properties.Resources.RecDraw;
                                }

                            }
                            else if (SelectNameCmb.SelectedIndex == 4)
                            {
                                DocName = "Απόδειξη Πληρωμής";
                                DocFrom = "";
                                DocTo = "";
                                DocReason = "";
                                query = "select a.Id,ReceiptId,ReceiptSeries,ReceiptDate,'','',ReceiptPrice,'',b.Id,Name,Occupation,Address,Tk,Afm,Tax_office,Phone,Email,Phone2,Region,ReceiptCash from MySupplierReceipt a,Suppliers b where a.Id=@id and SupplierId=b.Id";
                                SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                DataTable dt = new DataTable();
                                SearchAdapt.Fill(dt);
                                if (dt.Rows.Count == 1)
                                {
                                    DocId = dt.Rows[0][0].ToString();
                                    DocIdId = dt.Rows[0][1].ToString();
                                    DocSeries = dt.Rows[0][2].ToString();
                                    DocDate = dt.Rows[0][3].ToString();
                                    DocPayment = dt.Rows[0][4].ToString();
                                    DocDisc = dt.Rows[0][5].ToString();
                                    DocPrice = dt.Rows[0][6].ToString();
                                    DocDisNotes = dt.Rows[0][7].ToString();
                                    CustomerId = dt.Rows[0][8].ToString();
                                    CustomerName = dt.Rows[0][9].ToString();
                                    CustomerOccupation = dt.Rows[0][10].ToString();
                                    CustomerAddress = dt.Rows[0][11].ToString();
                                    CustomerTk = dt.Rows[0][12].ToString();
                                    CustomerAfm = dt.Rows[0][13].ToString();
                                    CustomerTax_office = dt.Rows[0][14].ToString();
                                    CustomerPhone = dt.Rows[0][15].ToString();
                                    CustomerEmail = dt.Rows[0][16].ToString();
                                    CustomerPhone2 = dt.Rows[0][17].ToString();
                                    CustomerRegion = dt.Rows[0][18].ToString();
                                    ReceiptCash = dt.Rows[0][19].ToString();
                                    string query2 = "select ValueDocId,ValueDocDate,ValueDocPrice,ValueDocIssuer from ReceiptValueDocs where ReceiptKind='3' and ReceiptId=@id order by Id";
                                    SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                                    SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                    SearchAdapt2.Fill(DocProds);
                                    proditems = DocProds.Rows.Count;
                                    InvoiceDrawImg = Properties.Resources.SupRecDraw;
                                    InvoicePlainImg = Properties.Resources.SupRecDraw;
                                }
                            }
                            else if (SelectNameCmb.SelectedIndex == 5)
                            {
                                DocName = "Δελτίο Αποστολής";
                                DocFrom = "Έδρα μας";
                                DocTo = "Έδρα προμηθευτή";
                                DocReason = "Επιστροφή";
                                query = "select a.Id,DisNoteId,DisNoteSeries,DisNoteDate,'','','',DisNoteDocs,b.Id,Name,Occupation,Address,Tk,Afm,Tax_office,Phone,Email,Phone2,Region from SupplierReturnDisNote a,Suppliers b where a.Id=@id and SupplierId=b.Id";
                                SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                                SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                DataTable dt = new DataTable();
                                SearchAdapt.Fill(dt);
                                if (dt.Rows.Count == 1)
                                {
                                    DocId = dt.Rows[0][0].ToString();
                                    DocIdId = dt.Rows[0][1].ToString();
                                    DocSeries = dt.Rows[0][2].ToString();
                                    DocDate = dt.Rows[0][3].ToString();
                                    DocPayment = dt.Rows[0][4].ToString();
                                    DocDisc = dt.Rows[0][5].ToString();
                                    DocPrice = dt.Rows[0][6].ToString();
                                    DocDisNotes = dt.Rows[0][7].ToString();
                                    CustomerId = dt.Rows[0][8].ToString();
                                    CustomerName = dt.Rows[0][9].ToString();
                                    CustomerOccupation = dt.Rows[0][10].ToString();
                                    CustomerAddress = dt.Rows[0][11].ToString();
                                    CustomerTk = dt.Rows[0][12].ToString();
                                    CustomerAfm = dt.Rows[0][13].ToString();
                                    CustomerTax_office = dt.Rows[0][14].ToString();
                                    CustomerPhone = dt.Rows[0][15].ToString();
                                    CustomerEmail = dt.Rows[0][16].ToString();
                                    CustomerPhone2 = dt.Rows[0][17].ToString();
                                    CustomerRegion = dt.Rows[0][18].ToString();
                                    string query2 = "select ProductId,Description,ProductQuant,'','',Unit from SupplierReturnDisNoteProducts a,Products b where a.ProductId=b.Id and SupplierDisNoteId=@id order by a.Id";
                                    SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                                    SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", DocumId);
                                    SearchAdapt2.Fill(DocProds);
                                    proditems = DocProds.Rows.Count;
                                    InvoiceDrawImg = Properties.Resources.InvDraw;
                                    InvoicePlainImg = Properties.Resources.InvPlain;
                                }
                            }
                            if (DocId!="")
                            {
                                Do_Print();
                                Clear();
                            }
                        }
                        catch (Exception)
                        {
                            System.Windows.MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση περιγραφής/κωδικού ή Α/Α Τιμολογίου. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                        }
                        sqlcon.Close();
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                }
            }
        }

        private void Clear()
        {
            DocName = "";
            DocFrom = "";
            DocTo = "";
            DocReason = "";
            DocId = "";
            DocIdId = "";
            DocSeries = "";
            DocDate = "";
            DocPayment = "";
            DocDisc = "";
            DocPrice = "";
            DocDisNotes = "";
            ReceiptCash = "";
            CustomerId = "";
            CustomerName = "";
            CustomerOccupation = "";
            CustomerAddress = "";
            CustomerTk = "";
            CustomerAfm = "";
            CustomerTax_office = "";
            CustomerPhone = "";
            CustomerEmail = "";
            CustomerPhone2 = "";
            CustomerRegion = "";
            proditems = 1;
            DocProds.Clear();
            PrintBtn.Enabled = false;
            DocumentsLbx.Items.Clear();
            SelectNameCmb.SelectedIndex = -1;
            DateFrom.Value = DateTime.Today;
            DateTo.Value = DateTime.Today;
        }

        private void Do_Print()
        {
            if (PrintPreviewChkBox.Checked)
            {
                PreviewDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
                PrintPrev.Document = PreviewDoc;
                ((Form)PrintPrev).StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = PrintPrev.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PreviewDoc.Print();
                }

            }
            else if (PrintChkBox.Checked)
            {
                PrintDial.Document = PreviewDoc;
                PrintDial.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
                PrintDial.PrinterSettings.Copies = 2;
                DialogResult result = PrintDial.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PrintDoc.Print();
                }
            }
        }

        private void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(InvoicePlainImg, 0, 0, 1094, 1546);
            if (proditems > 0)
            {
                PrintDoc_Elements(e);
            }
        }

        private void PreviewDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(InvoiceDrawImg, 0, 0, 1094, 1546);
            if (proditems > 0)
            {
                PrintDoc_Elements(e);
            }
        }

        private void PreviewDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            PrintPrev.Document = PrintDoc;
        }

        private void PrintDoc_Elements(System.Drawing.Printing.PrintPageEventArgs e)
        {
            GC.Collect();
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΤΙΜΟΛΟΓΙΟΥ
            e.Graphics.DrawString(DocName, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 130+(SelectNameCmb.SelectedIndex == 4 ? 30 : (SelectNameCmb.SelectedIndex == 2 ? 10 : (SelectNameCmb.SelectedIndex == 1 ? 63 : (SelectNameCmb.SelectedIndex == 5 ? 63 :0)))), 250);
            e.Graphics.DrawString(DocSeries, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 588, 250);
            e.Graphics.DrawString(DocIdId, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 690, 250);
            e.Graphics.DrawString(Convert.ToDateTime(DocDate).Day.ToString() + '/' + Convert.ToDateTime(DocDate).Month.ToString() + '/' + Convert.ToDateTime(DocDate).Year.ToString(), new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 821, 250);
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΠΕΛΑΤΗ
            e.Graphics.DrawString(CustomerId, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170+(SelectNameCmb.SelectedIndex == 4 ? 50 : 0), 329);
            e.Graphics.DrawString(CustomerName, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 356);
            e.Graphics.DrawString(CustomerAddress, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 383);
            e.Graphics.DrawString(CustomerRegion, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 410);
            e.Graphics.DrawString(CustomerOccupation, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 438);
            e.Graphics.DrawString(CustomerAfm, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 466);
            e.Graphics.DrawString(CustomerEmail, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 493);
            e.Graphics.DrawString(CustomerPhone.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 329);
            e.Graphics.DrawString(CustomerPhone2.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 356);
            e.Graphics.DrawString(CustomerTk, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 411);
            e.Graphics.DrawString(CustomerTax_office, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 466);
            if (SelectNameCmb.SelectedIndex == 0 || SelectNameCmb.SelectedIndex == 1 || SelectNameCmb.SelectedIndex == 2 || SelectNameCmb.SelectedIndex == 5)
            {
                //ΕΚΤΥΠΩΣΗ ΛΟΙΠΩΝ ΣΤΟΙΧΕΙΩΝ ΤΙΜΟΛΟΓΙΟΥ
                e.Graphics.DrawString(DocDisNotes, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 307);   //SXET PARASTATIKA
                e.Graphics.DrawString(DocFrom, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 336);
                e.Graphics.DrawString(DocTo, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 365);
                e.Graphics.DrawString(DocReason, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 394);
                e.Graphics.DrawString(DocPayment, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 423);
                e.Graphics.DrawString((DocDisc=="" ?"" : chk.GrNumber(DocDisc) + " %"), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 452);
                //e.Graphics.DrawString(CustomerTax_office, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 804, 487);  METAFORIKO MESO
                //ΕΚΤΥΠΩΣΗ ΠΡΟΙΟΝΤΩΝ
                int rest = proditems;
                int cnt = DocProds.Rows.Count - proditems;
                bool hasmorepages = false;
                decimal vat = chk.Return_Vat(CustomerId);
                for (int i = 1; i <= rest; i++)
                {
                    if (i == 19)
                    {
                        hasmorepages = true;
                        break;
                    }
                    string qnt = DocProds.Rows[i-1 + cnt][2].ToString(); 
                    string val = DocProds.Rows[i-1 + cnt][3].ToString();
                    string disc = DocProds.Rows[i-1 + cnt][4].ToString();
                    if (val!= "")
                    {
                        decimal totval = Convert.ToDecimal(Math.Round(Convert.ToDouble(qnt) * Convert.ToDouble(val), 2));
                        decimal totdisc = Convert.ToDecimal(Math.Round(totval * Convert.ToDecimal(((disc == "") ? "0" : disc)) / 100, 2));
                        totdisc += Convert.ToDecimal(Math.Round((totval - totdisc) * Convert.ToDecimal(DocDisc) / 100, 2));
                        decimal totdiscval = totval - totdisc;
                        decimal totvat = Convert.ToDecimal(Math.Round(totdiscval * vat / 100, 2));
                        e.Graphics.DrawString(chk.GrNumber(val), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(632, 558 + i * 25, 58, 25), format);
                        e.Graphics.DrawString(chk.GrNumber((totval).ToString()), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(696, 558 + i * 25, 72, 25), format);
                        e.Graphics.DrawString(((disc == "0") ? "" : chk.GrNumber(disc.ToString())), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(780, 558 + i * 25, 60, 25), format);
                        e.Graphics.DrawString(((totdisc == 0) ? "" : chk.GrNumber(totdisc.ToString())), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(847, 558 + i * 25, 55, 25), format);
                        e.Graphics.DrawString(chk.GrNumber((totdiscval).ToString()), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(908, 558 + i * 25, 63, 25), format);
                        e.Graphics.DrawString((vat).ToString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(976, 558 + i * 25, 24, 25), format);
                        e.Graphics.DrawString(chk.GrNumber((totvat).ToString()), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(1006, 558 + i * 25, 52, 25), format);
                        sumval += Convert.ToDecimal(totval);
                        sumdisc += Convert.ToDecimal(totdisc);
                        sumvat += Convert.ToDecimal(totvat);
                    }
                    string descr = DocProds.Rows[i - 1 + cnt][1].ToString();
                    string Unit = DocProds.Rows[i - 1 + cnt][5].ToString();
                    if (descr.Length > 60)
                    {
                        descr = descr.Substring(0, 60);
                    }
                    if (Unit.Length > 4)
                    {
                        Unit = Unit.Substring(0, 4);
                    }
                    e.Graphics.DrawString(DocProds.Rows[i - 1 + cnt][0].ToString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 33, 558 + i * 25);
                    e.Graphics.DrawString(descr, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 120, 558 + i * 25);
                    e.Graphics.DrawString(Unit, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 520, 558 + i * 25);
                    e.Graphics.DrawString(chk.GrQuant(qnt), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(561, 558 + i * 25, 65, 25), format);
                    proditems--;
                    sumquant += Convert.ToDouble(qnt);
                }
                //e.Graphics.DrawString(PrevCreditCustomerTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 145, 1052);
                //ΕΚΤΥΠΩΣΗ ΑΡΙΘΜΙΣΗΣ ΣΕΛΙΔΩΝ ΤΙΜΟΛΟΓΙΟΥ
                e.Graphics.DrawString("Σελίδα " + pagecount + " από " + (DocProds.Rows.Count / 18 + 1), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 45, 1505);
                pagecount++;
                //ΕΚΤΥΠΩΣΗ ΣΥΝΟΛΩΝ ΤΙΜΟΛΟΓΙΟΥ
                if (hasmorepages == false)
                {
                    e.Graphics.DrawString(chk.GrQuant(sumquant.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 645, 1052);
                    e.Graphics.DrawString(chk.GrNumber(sumval.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1059, 120, 25), format);
                    e.Graphics.DrawString(chk.GrNumber(sumdisc.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1105, 120, 25), format);
                    e.Graphics.DrawString(chk.GrNumber((sumval - sumdisc).ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1151, 120, 25), format);
                    e.Graphics.DrawString(chk.GrNumber(sumvat.ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(924, 1197, 120, 25), format);
                    e.Graphics.DrawString(chk.GrNumber((sumval - sumdisc + sumvat).ToString()), new Font("Arial", 12, System.Drawing.FontStyle.Bold), Brushes.Black, new Rectangle(924, 1273, 120, 25), format);
                    //e.Graphics.DrawString((Convert.ToDecimal(PrevCreditCustomerTxt.Text) + (PayInvoiceCmb.GetItemText(PayInvoiceCmb.SelectedItem) == "Πίστωση" ? sumval - sumdisc + sumvat : 0)).ToString(), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 391, 1052);
                    proditems = DocProds.Rows.Count;
                    sumquant = 0;
                    sumval = 0;
                    sumdisc = 0;
                    sumvat = 0;
                    pagecount = 1;
                    GC.Collect();
                }
                e.HasMorePages = hasmorepages;
            }
            else if (SelectNameCmb.SelectedIndex == 3 || SelectNameCmb.SelectedIndex == 4)
            {
                //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΑΠΟΔΕΙΞΗΣ
                for (int i = 1; i <= proditems; i++)
                {
                    e.Graphics.DrawString(DocProds.Rows[i - 1][0].ToString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 110, 560 + i * 25);
                    e.Graphics.DrawString(Convert.ToDateTime(DocProds.Rows[i - 1][1].ToString()).Day.ToString() + '/' + Convert.ToDateTime(DocProds.Rows[i - 1][1].ToString()).Month.ToString() + '/' + Convert.ToDateTime(DocProds.Rows[i - 1][1].ToString()).Year.ToString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 375, 560 + i * 25);
                    e.Graphics.DrawString(chk.GrNumber(DocProds.Rows[i - 1][2].ToString()), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(585, 560 + i * 25, 100, 25), format);
                    e.Graphics.DrawString(DocProds.Rows[i - 1][3].ToString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 725, 560 + i * 25);
                }
                e.Graphics.DrawString(chk.GrNumber(ReceiptCash), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(828, 1105, 100, 25), format);
                e.Graphics.DrawString(chk.GrNumber(DocPrice), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(828, 1178, 100, 25), format);
                string fullname1 = "";
                string fullname2 = "";
                if (DocPrice != "")
                {
                    try
                    {
                        int decim = 0;
                        decimal price = decimal.Parse(DocPrice);
                        if (price != Math.Truncate(price))
                        {
                            decim = (int)((price - Math.Truncate(price)) * 100);
                        }
                        fullname1 = chk.Number_fullname(Convert.ToInt32(Math.Truncate(price))) + " ευρώ";
                        fullname2 = (decim == 0 ? "" : " και " + chk.Number_fullname(decim) + " λεπτά");
                    }
                    catch (Exception)
                    {
                    }
                }
                e.Graphics.DrawString(fullname1, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 43, 1080);
                e.Graphics.DrawString(fullname2, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 43, 1108);
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace CRMapp
{

    public partial class ReceiptToSupplier : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        List<string> ls = new List<string>();
        Dictionary<string,string> dc = new Dictionary<string,string>();
        StringFormat format = new StringFormat() { Alignment = StringAlignment.Far };
        Checks chk = new Checks();
        Image RecDrawImg = Properties.Resources.SupRecDraw;
        private string SupplierOccupation;
        private string SupplierAddress;
        private string SupplierTk;
        private string SupplierTax_office;
        private string SupplierPhone;
        private string SupplierEmail;
        private string SupplierRegion;
        private string SupplierPhone2;

        public ReceiptToSupplier()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetData();
        }

        private void GetData()
        {
            SeriesReceiptTxt.Text = chk.Return_SupplierReceiptSeries();
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        GC.Collect();
                        string query = "select right('00000'+dbo.nvl(Max(right(ReceiptId,5))+1,1),5)  from MySupplierReceipt where ReceiptSeries=@series";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"series", chk.Return_SupplierReceiptSeries());
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            IdReceiptTxt.Text = "ΠΛΗ-" + dt.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση του Α/Α απόδειξης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
        }

        void FormClose(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }


        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            bool blanks = false;
            bool falsedate = false;
            for (int i = 1; i <= Convert.ToInt16(ValueDocItemsTxt.Text); i++)
            {
                if (ProductsPanel.Controls.Find("ValueDocIdTxt" + i, true).First().Text != "" && ProductsPanel.Controls.Find("ValueDocIssuerTxt" + i, true).First().Text != "")
                {
                    if (ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text != "" && ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text != "0")
                    {
                        if (ProductsPanel.Controls.Find("ValueDocIdTxt" + i, true).First().Text == "")
                        {
                            blanks = true;
                        }
                        else if (ProductsPanel.Controls.Find("ValueDocIssuerTxt" + i, true).First().Text == "")
                        {
                            blanks = true;
                        }
                        else if (((DateTimePicker)ProductsPanel.Controls.Find("ValueDocDatePkr" + i, true).First()).Value < DateTimeReceiptPicker.Value)
                        {
                            falsedate = true;
                        }
                    }
                }
            }
            if (IdSupplierTxt.Text == "")
            {
                MessageBox.Show("Θα πρέπει να επιλέξετε Προμηθευτή.");
            }
            else if (DateTimeReceiptPicker.Value > System.DateTime.Today.AddDays(1))
            {
                MessageBox.Show("Η ημερομηνία δεν μπορεί να είναι μεταγενέστερη της σημερινής.");
            }
            else if (Convert.ToDouble(PriceReceiptTxt.Text) == 0)
            {
                MessageBox.Show("Θα πρέπει να συμπληρώσετε τα ποσά των αξιογράφων ή/και των μετρητών.");
            }
            else if (blanks == true)
            {
                MessageBox.Show("Θα πρέπει να συμπληρώσετε αριθμό αξιογράφου και εκδότη.");
            }
            else if (falsedate == true)
            {
                MessageBox.Show("Η ημερομηνία λήξης αξιογράφου δεν μπορεί να είναι προγενέστερη της ημερομηνίας της απόδειξης.");
            }
            else
                using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
                {
                    try
                    {
                        sqlcon.Open();
                        DataTable dt2 = new DataTable();
                        {
                            string query = "select Debit from Suppliers where Id=@id";
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query, sqlcon);
                            SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", IdSupplierTxt.Text);
                            SearchAdapt2.Fill(dt2);
                        }
                        SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                        SqlCommand InsCmd1 = sqlcon.CreateCommand();
                        InsCmd1.Connection = sqlcon;
                        SqlCommand InsCmd2 = sqlcon.CreateCommand();
                        InsCmd2.Connection = sqlcon;
                        InsCmd1.Transaction = InsTrans;
                        InsCmd2.Transaction = InsTrans;
                        try
                        { 
                            InsCmd1.CommandText = "insert into MySupplierReceipt (Id, SupplierId, ReceiptId, ReceiptSeries, ReceiptDate, ReceiptPrice, ReceiptCash, ReceiptNotes) values((select dbo.nvl(Max(Id)+1,0) from dbo.MySupplierReceipt), @supid, @reid, @ser, @date, @price, @cash, @not)";
                            InsCmd1.Parameters.AddWithValue("@supid", IdSupplierTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@reid", IdReceiptTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@ser", SeriesReceiptTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@date", DateTimeReceiptPicker.Value);
                            InsCmd1.Parameters.AddWithValue("@price", PriceReceiptTxt.Text);
                            InsCmd1.Parameters.AddWithValue("@cash", (CashTxt.Text == "" ? "0" : CashTxt.Text));
                            InsCmd1.Parameters.AddWithValue("@not", NotesTxt.Text);
                            InsCmd1.ExecuteNonQuery();
                                
                            if (dt2.Rows.Count==1)
                            {
                                decimal oldDebit = Convert.ToDecimal(dt2.Rows[0][0].ToString());
                                decimal newDebit = oldDebit - Convert.ToDecimal(PriceReceiptTxt.Text);
                                InsCmd2.CommandText = "update Suppliers set Debit= @debit where Id=@id";
                                InsCmd2.Parameters.AddWithValue("@debit", newDebit);
                                InsCmd2.Parameters.AddWithValue("@id", IdSupplierTxt.Text);
                                InsCmd2.ExecuteNonQuery();
                            }

                            for (int i = 1; i <= Convert.ToInt16(ValueDocItemsTxt.Text); i++)
                            {
                                if (ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text != "")
                                {
                                    SqlCommand InsCmd3 = sqlcon.CreateCommand();
                                    InsCmd3.Connection = sqlcon;
                                    InsCmd3.Transaction = InsTrans;
                                    InsCmd3.CommandText = "insert into ReceiptValueDocs (Id,ReceiptId,ReceiptKind,ValueDocId,ValueDocDate,ValueDocPrice,ValueDocIssuer) values ((select dbo.nvl(Max(Id)+1,0) from ReceiptValueDocs),(select dbo.nvl(Max(Id),0) from dbo.MySupplierReceipt),3,@id,@date,@price,@issuer)";
                                    InsCmd3.Parameters.AddWithValue("@id", ProductsPanel.Controls.Find("ValueDocIdTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@date", ((DateTimePicker)ProductsPanel.Controls.Find("ValueDocDatePkr" + i, true).First()).Value);
                                    InsCmd3.Parameters.AddWithValue("@price", ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text);
                                    InsCmd3.Parameters.AddWithValue("@issuer", ProductsPanel.Controls.Find("ValueDocIssuerTxt" + i, true).First().Text);
                                    InsCmd3.ExecuteNonQuery();
                                }
                            }
                            InsTrans.Commit();
                            MessageBox.Show("Η απόδειξη του προμηθευτή καταχωρήθηκε με επιτυχία.");
                            Do_Print();
                            ClearValues();
                            GetData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.GetType() + ": " + ex.Message + "\n Commit Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση της απόδειξης του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                            try
                            {
                                InsTrans.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show(ex2.GetType() + ": " + ex2.Message + "\n Rollback Exception \n Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην καταχώρηση της απόδειξης του προμηθευτή. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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


        private void ClearValues()
        {

            this.SuspendLayout();
            IdSupplierTxt.Text = "";
            NameSupplierTxt.Text = "";
            AfmSupplierTxt.Text = "";
            SupplierOccupation = "";
            SupplierAddress = "";
            SupplierTk = "";
            SupplierTax_office = "";
            SupplierPhone = "";
            SupplierEmail = "";
            SupplierRegion = "";
            SupplierPhone2 = "";
            PriceReceiptTxt.Text = "";
            CashTxt.Text = "";
            NotesTxt.Text = "";
            DateTimeReceiptPicker.Value = System.DateTime.Today;
            ValueDocIdTxt1.Text = "";
            ValueDocDatePkr1.Value = System.DateTime.Today;
            ValueDocPriceTxt1.Text = "";
            ValueDocIssuerTxt1.Text = "";
            for (int i = Convert.ToInt16(ValueDocItemsTxt.Text); i >= 2; i--)
            {
                RemoveValueDocBtn_Click(null, null);
            }
            this.ResumeLayout(false);
        }


        private void ValueDocPriceTxt1_TextChanged(object sender, EventArgs e)
        {
            ValueDocPriceTxt1.Text = ValueDocPriceTxt1.Text.Replace(',', '.');
            ValueDocPriceTxt1.SelectionStart = ValueDocPriceTxt1.Text.Length;
            if (ValueDocPriceTxt1.Text == "")
            {
                ValueDocPriceTxt1.Text = "0";
            }
            if (chk.CheckPrice(CashTxt.Text) == "" || CashTxt.Text == "")
            {
                bool error = false;
                double sumprice = (CashTxt.Text == "" ? 0 : Convert.ToDouble(CashTxt.Text));
                for (int i = 1; i <= Convert.ToInt16(ValueDocItemsTxt.Text); i++)
                {
                    if (chk.CheckPrice(ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text) == "" || ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text == "")
                    {
                        if (chk.CheckPrice(ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text) == "")
                        {
                            sumprice += Convert.ToDouble(ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text);
                        }
                    }
                    else
                    {
                        error = true;
                    }
                }
                if (error == false)
                {
                    PriceReceiptTxt.Text = sumprice.ToString();
                }
                else
                {
                    PriceReceiptTxt.Text = "";
                }
            }
            else
            {
                PriceReceiptTxt.Text = "";
            }
        }

        private void CashTxt_TextChanged(object sender, EventArgs e)
        {
            CashTxt.Text = CashTxt.Text.Replace(',', '.');
            CashTxt.SelectionStart = CashTxt.Text.Length;
            if (CashTxt.Text == "")
            {
                CashTxt.Text = "0";
            }
            if (chk.CheckPrice(CashTxt.Text) == "")
            {
                bool error = false;
                double sumprice = (CashTxt.Text == "" ? 0 : Convert.ToDouble(CashTxt.Text));
                for (int i = 1; i <= Convert.ToInt16(ValueDocItemsTxt.Text); i++)
                {
                    if (chk.CheckPrice(ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text) == "" || ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text == "")
                    {
                        if (chk.CheckPrice(ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text) == "")
                        {
                            sumprice += Convert.ToDouble(ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text);
                        }
                    }
                    else
                    {
                        error = true;
                    }
                }
                if (error == false)
                {
                    PriceReceiptTxt.Text = sumprice.ToString();
                }
                else
                {
                    PriceReceiptTxt.Text = "";
                }
            }
            else
            {
                PriceReceiptTxt.Text = "";
            }
        }

        private void AddValueDocBtn_Click(object sender, EventArgs e)
        {

            this.SuspendLayout();
            this.ProductsPanel.Height += 21;
            this.Height += 21;
            int i = Convert.ToInt16(ValueDocItemsTxt.Text);
            if (i == 1)
            {
                this.RemoveValueDocBtn.Visible = true;
            }
            i++;
            ValueDocItemsTxt.Text = i.ToString();
            this.AddValueDocBtn.Location = new Point(AddValueDocBtn.Location.X, AddValueDocBtn.Location.Y + 21);
            this.RemoveValueDocBtn.Location = new Point(RemoveValueDocBtn.Location.X, RemoveValueDocBtn.Location.Y + 21);
            this.ProdPanel.Height += 21;
            System.Windows.Forms.Label AaTxt = new System.Windows.Forms.Label();
            System.Windows.Forms.TextBox ValueDocIdTxt = new System.Windows.Forms.TextBox();
            System.Windows.Forms.DateTimePicker ValueDocDatePkr = new System.Windows.Forms.DateTimePicker();
            System.Windows.Forms.TextBox ValueDocPriceTxt = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TextBox ValueDocIssuerTxt = new System.Windows.Forms.TextBox();
            AaTxt.Text = i.ToString();
            AaTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            AaTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            AaTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            AaTxt.Name = "AaTxt" + i;
            AaTxt.Location = new Point(AaTxt1.Location.X, AaTxt1.Location.Y + 21 * (i - 1));
            AaTxt.Size = AaTxt1.Size;
            AaTxt.Font = AaTxt1.Font;
            ValueDocIdTxt.Name = "ValueDocIdTxt" + i;
            ValueDocIdTxt.Location = new Point(ValueDocIdTxt1.Location.X, ValueDocIdTxt1.Location.Y + 21 * (i - 1));
            ValueDocIdTxt.Size = ValueDocIdTxt1.Size;
            ValueDocIdTxt.Font = ValueDocIdTxt1.Font;
            ValueDocDatePkr.Name = "ValueDocDatePkr" + i;
            ValueDocDatePkr.Location = new Point(ValueDocDatePkr1.Location.X, ValueDocDatePkr1.Location.Y + 21 * (i - 1));
            ValueDocDatePkr.Size = ValueDocDatePkr1.Size;
            ValueDocDatePkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            ValueDocDatePkr.CustomFormat = "dd/MM/yyyy";
            ValueDocDatePkr.Value = System.DateTime.Today;
            ValueDocDatePkr.MinimumSize = ValueDocDatePkr1.MinimumSize;
            ValueDocDatePkr.Font = ValueDocDatePkr1.Font;
            ValueDocPriceTxt.Name = "ValueDocPriceTxt" + i;
            ValueDocPriceTxt.Location = new Point(ValueDocPriceTxt1.Location.X, ValueDocPriceTxt1.Location.Y + 21 * (i - 1));
            ValueDocPriceTxt.Size = ValueDocPriceTxt1.Size;
            ValueDocPriceTxt.Font = ValueDocPriceTxt1.Font;
            ValueDocIssuerTxt.Name = "ValueDocIssuerTxt" + i;
            ValueDocIssuerTxt.Location = new Point(ValueDocIssuerTxt1.Location.X, ValueDocIssuerTxt1.Location.Y + 21 * (i - 1));
            ValueDocIssuerTxt.Size = ValueDocIssuerTxt1.Size;
            ValueDocIssuerTxt.Font = ValueDocIssuerTxt1.Font;
            this.ProductsPanel.Controls.Add(AaTxt);
            this.ProductsPanel.Controls.Add(ValueDocIdTxt);
            this.ProductsPanel.Controls.Add(ValueDocDatePkr);
            this.ProductsPanel.Controls.Add(ValueDocPriceTxt);
            this.ProductsPanel.Controls.Add(ValueDocIssuerTxt);
            this.ProdPanel.SendToBack();
            {
                ValueDocPriceTxt.TextChanged += (object sender1, EventArgs e1) =>
                {
                    ValueDocPriceTxt.Text = ValueDocPriceTxt.Text.Replace(',', '.');
                    ValueDocPriceTxt.SelectionStart = ValueDocPriceTxt.Text.Length;
                    ValueDocPriceTxt1.Text = ValueDocPriceTxt1.Text.Replace(',', '.');
                    ValueDocPriceTxt1.SelectionStart = ValueDocPriceTxt1.Text.Length;
                    if (ValueDocPriceTxt.Text == "")
                    {
                        ValueDocPriceTxt.Text = "0";
                    }
                    if (chk.CheckPrice(CashTxt.Text) == "" || CashTxt.Text == "")
                    {
                        bool error = false;
                        double sumprice = (CashTxt.Text == "" ? 0 : Convert.ToDouble(CashTxt.Text));
                        for (int j = 1; j <= Convert.ToInt16(ValueDocItemsTxt.Text); j++)
                        {
                            if (chk.CheckPrice(ProductsPanel.Controls.Find("ValueDocPriceTxt" + j, true).First().Text) == "" || ProductsPanel.Controls.Find("ValueDocPriceTxt" + j, true).First().Text == "")
                            {
                                if (chk.CheckPrice(ProductsPanel.Controls.Find("ValueDocPriceTxt" + j, true).First().Text) == "")
                                {
                                    sumprice += Convert.ToDouble(ProductsPanel.Controls.Find("ValueDocPriceTxt" + j, true).First().Text);
                                }
                            }
                            else
                            {
                                error = true;
                            }
                        }
                        if (error == false)
                        {
                            PriceReceiptTxt.Text = sumprice.ToString();
                        }
                        else
                        {
                            PriceReceiptTxt.Text = "";
                        }
                    }
                    else
                    {
                        PriceReceiptTxt.Text = "";
                    }
                };
            }
            this.ResumeLayout(false);
        }

        private void RemoveValueDocBtn_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            int i = Convert.ToInt16(ValueDocItemsTxt.Text);
            if (i == 2)
            {
                this.ValueDocItemsTxt.Visible = false;
            }
            this.AddValueDocBtn.Location = new Point(AddValueDocBtn.Location.X, AddValueDocBtn.Location.Y - 21);
            this.RemoveValueDocBtn.Location = new Point(RemoveValueDocBtn.Location.X, RemoveValueDocBtn.Location.Y - 21);
            this.ProdPanel.Height -= 21;
            this.ProductsPanel.Height -= 21;
            this.Height -= 21;
            this.ProductsPanel.Controls.Remove(Controls.Find("AaTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("ValueDocIdTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("ValueDocDatePkr" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("ValueDocPriceTxt" + i, true).First());
            this.ProductsPanel.Controls.Remove(Controls.Find("ValueDocIssuerTxt" + i, true).First());
            i--;
            ValueDocItemsTxt.Text = i.ToString();
            this.ResumeLayout(false);
        }


        private void PreviewDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GC.Collect();
            e.Graphics.DrawImage(RecDrawImg, 0, 0, 1094, 1546);
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΑΠΟΔΕΙΞΗΣ
            e.Graphics.DrawString("Απόδειξη Πληρωμής", new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 160, 250);
            e.Graphics.DrawString(SeriesReceiptTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 588, 250);
            e.Graphics.DrawString(IdReceiptTxt.Text, new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 690, 250);
            e.Graphics.DrawString(DateTimeReceiptPicker.Value.Day.ToString() + '/' + DateTimeReceiptPicker.Value.Month.ToString() + '/' + DateTimeReceiptPicker.Value.Year.ToString(), new Font("Arial", 14, System.Drawing.FontStyle.Regular), Brushes.Black, 821, 250);
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΠΡΟΜΗΘΕΥΤΗ
            e.Graphics.DrawString(IdSupplierTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 220, 329);
            e.Graphics.DrawString(NameSupplierTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 356);
            e.Graphics.DrawString(SupplierAddress, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 383);
            e.Graphics.DrawString(SupplierRegion, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 410);
            e.Graphics.DrawString(SupplierOccupation, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 438);
            e.Graphics.DrawString(AfmSupplierTxt.Text, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 466);
            e.Graphics.DrawString(SupplierEmail, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 170, 493);
            e.Graphics.DrawString(SupplierPhone.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 329);
            e.Graphics.DrawString(SupplierPhone2.Substring(0, 10), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 356);
            e.Graphics.DrawString(SupplierTk, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 411);
            e.Graphics.DrawString(SupplierTax_office, new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, 460, 466);
            //ΕΚΤΥΠΩΣΗ ΣΤΟΙΧΕΙΩΝ ΑΠΟΔΕΙΞΗΣ
            DateTime dt;
            for (int i = 1; i <= Convert.ToInt16(ValueDocItemsTxt.Text); i++)
            {

                if (ProductsPanel.Controls.Find("ValueDocIdTxt" + i, true).First().Text != "" && ProductsPanel.Controls.Find("ValueDocIssuerTxt" + i, true).First().Text != "")
                {
                    if (ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text != "" && ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text != "0")
                    {
                        dt = ((DateTimePicker)ProductsPanel.Controls.Find("ValueDocDatePkr" + i, true).First()).Value;
                        e.Graphics.DrawString(ProductsPanel.Controls.Find("ValueDocIdTxt" + i, true).First().Text, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 110, 560 + i * 25);
                        e.Graphics.DrawString(dt.Day.ToString() + '/' + dt.Month.ToString() + '/' + dt.Year.ToString(), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 375, 560 + i * 25);
                        e.Graphics.DrawString(chk.GrNumber(ProductsPanel.Controls.Find("ValueDocPriceTxt" + i, true).First().Text), new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(585, 560 + i * 25, 100, 25), format);
                        e.Graphics.DrawString(ProductsPanel.Controls.Find("ValueDocIssuerTxt" + i, true).First().Text, new Font("Arial", 10, System.Drawing.FontStyle.Regular), Brushes.Black, 725, 560 + i * 25);
                    }
                }
            }
            //ΕΚΤΥΠΩΣΗ ΣΥΝΟΛΩΝ ΑΠΟΔΕΙΞΗΣ
            e.Graphics.DrawString(chk.GrNumber(CashTxt.Text), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(828, 1105, 100, 25), format);
            e.Graphics.DrawString(chk.GrNumber(PriceReceiptTxt.Text), new Font("Arial", 12, System.Drawing.FontStyle.Regular), Brushes.Black, new Rectangle(828, 1178, 100, 25), format);
            string fullname1 = "";
            string fullname2 = "";
            if (PriceReceiptTxt.Text != "")
            {
                try
                {
                    int decim = 0;
                    decimal price = decimal.Parse(PriceReceiptTxt.Text);
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
            if (NotesTxt.Text != "")
            {
                string Note1 = NotesTxt.Text;
                string Note2 = "";
                if (Note1.Length > 70)
                {
                    Note2 = Note1.Substring(70);
                    Note1 = Note1.Substring(0, 70);
                    if (Note2.Length > 70)
                    {
                        Note2 = Note2.Substring(0, 70);
                    }
                }
                e.Graphics.DrawString(Note1, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 39, 1180);
                e.Graphics.DrawString(Note2, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 39, 1205);
            }
            e.Graphics.DrawString(fullname1, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 43, 1080);
            e.Graphics.DrawString(fullname2, new Font("Arial", 11, System.Drawing.FontStyle.Italic), Brushes.Black, 43, 1108);
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

        private void Do_Print()
        {
            if (PrintPreviewChkBox.Checked)
            {
                PreviewDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
                PrintPrev.Document = PreviewDoc;
                ((Form)PrintPrev).StartPosition = FormStartPosition.CenterScreen;
                PrintPrev.ShowDialog();

            }
            else if (PrintChkBox.Checked)
            {
                PrintDial.Document = PreviewDoc;
                PrintDial.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Custom", 1094, 1546);
                PrintDial.PrinterSettings.Copies = 2;
                DialogResult result = PrintDial.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PreviewDoc.Print();
                }
            }
        }

        private void PriceReceiptTxt_TextChanged(object sender, EventArgs e)
        {
            PriceReceiptTxt.Text = PriceReceiptTxt.Text.Replace(',', '.');
            PriceReceiptTxt.SelectionStart = PriceReceiptTxt.Text.Length;
        }

        private void SearchSupplierBtn_Click(object sender, EventArgs e)
        {
            OrderFindSupplier FindSupplier = new OrderFindSupplier();
            FindSupplier.FormClosing += (object sender2, FormClosingEventArgs e2) =>
            {
                if (FindSupplier.ReturnSupplierId() != null)
                {
                    this.IdSupplierTxt.Text = FindSupplier.ReturnSupplierId();
                    this.NameSupplierTxt.Text = FindSupplier.ReturnSupplierName();
                    this.AfmSupplierTxt.Text = FindSupplier.ReturnSupplierAfm();
                    SupplierOccupation = FindSupplier.ReturnSupplierOccupation();
                    SupplierAddress = FindSupplier.ReturnSupplierAddress();
                    SupplierTk = FindSupplier.ReturnSupplierTk();
                    SupplierTax_office = FindSupplier.ReturnSupplierTax_office();
                    SupplierPhone = FindSupplier.ReturnSupplierPhone();
                    SupplierEmail = FindSupplier.ReturnSupplierEmail();
                    SupplierPhone2 = FindSupplier.ReturnSupplierPhone2();
                    SupplierRegion = FindSupplier.ReturnSupplierRegion();
                }
            };
            FindSupplier.FormClosed += new FormClosedEventHandler(FormClose);
            FindSupplier.FormClosed += (object sender3, FormClosedEventArgs e3) =>
            {
                this.SuspendLayout();
                FindSupplier.Dispose();
                this.ResumeLayout(false);
            };
            this.Enabled = false;
            FindSupplier.ShowDialog();
        }
    }
}

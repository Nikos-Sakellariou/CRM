using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class PreviewReceiptCustomer : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();

        public PreviewReceiptCustomer(string Id)
        {
            GC.Collect();
            con = chk.Initiallize_con();
            InitializeComponent();
            GetData(Id);
        }


        private void GetData(string Id)
        {
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select CustomerId,Name,Afm,ReceiptId,ReceiptSeries,ReceiptDate,ReceiptPrice,ReceiptCash from CustomerReceipt a, Customers b where a.CustomerId=b.Id and a.Id=@id";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"id", Id);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            IdCustomerTxt.Text = dt.Rows[0][0].ToString();
                            NameCustomerTxt.Text = dt.Rows[0][1].ToString();
                            AfmCustomerTxt.Text = dt.Rows[0][2].ToString();
                            IdInvoiceTxt.Text = dt.Rows[0][3].ToString();
                            SeriesInvoiceTxt.Text = dt.Rows[0][4].ToString();
                            DateTimeInvoicePicker.Text = dt.Rows[0][5].ToString();
                            PriceReceiptTxt.Text = dt.Rows[0][6].ToString();
                            CashTxt.Text = dt.Rows[0][7].ToString();
                        }
                        string query2 = "select ValueDocId,ValueDocDate,ValueDocPrice,ValueDocIssuer from ReceiptValueDocs where ReceiptKind=1 and ReceiptId=@id";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", Id);
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (i==0)
                            {
                                ValueDocIdTxt1.Text = dt2.Rows[i][0].ToString();
                                ValueDocDatePkr1.Text = dt2.Rows[i][1].ToString();
                                ValueDocPriceTxt1.Text = dt2.Rows[i][2].ToString();
                                ValueDocIssuerTxt1.Text = dt2.Rows[i][3].ToString();
                            }
                            else
                            {
                                this.SuspendLayout();
                                this.ProductsPanel.Height += 21;
                                this.Height += 21;
                                this.ProdPanel.Height += 21;
                                System.Windows.Forms.Label AaTxt = new System.Windows.Forms.Label();
                                System.Windows.Forms.TextBox ValueDocIdTxt = new System.Windows.Forms.TextBox();
                                System.Windows.Forms.DateTimePicker ValueDocDatePkr = new System.Windows.Forms.DateTimePicker();
                                System.Windows.Forms.TextBox ValueDocPriceTxt = new System.Windows.Forms.TextBox();
                                System.Windows.Forms.TextBox ValueDocIssuerTxt = new System.Windows.Forms.TextBox();
                                AaTxt.Text = (i + 1).ToString();
                                AaTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                                AaTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                AaTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                                AaTxt.Name = "AaTxt" + (i + 1);
                                AaTxt.Location = new Point(AaTxt1.Location.X, AaTxt1.Location.Y + 21 * i);
                                AaTxt.Size = AaTxt1.Size;
                                AaTxt.Font = AaTxt1.Font;
                                ValueDocIdTxt.Name = "ValueDocIdTxt" + (i + 1);
                                ValueDocIdTxt.Location = new Point(ValueDocIdTxt1.Location.X, ValueDocIdTxt1.Location.Y + 21 * i);
                                ValueDocIdTxt.Size = ValueDocIdTxt1.Size;
                                ValueDocIdTxt.Font = ValueDocIdTxt1.Font;
                                ValueDocIdTxt.ReadOnly = true;
                                ValueDocDatePkr.Name = "ValueDocDatePkr" + (i + 1);
                                ValueDocDatePkr.Location = new Point(ValueDocDatePkr1.Location.X, ValueDocDatePkr1.Location.Y + 21 * i);
                                ValueDocDatePkr.Size = ValueDocDatePkr1.Size;
                                ValueDocDatePkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                                ValueDocDatePkr.CustomFormat = "dd/MM/yyyy";
                                ValueDocDatePkr.Value = System.DateTime.Today;
                                ValueDocDatePkr.MinimumSize = ValueDocDatePkr1.MinimumSize;
                                ValueDocDatePkr.Font = ValueDocDatePkr1.Font;
                                ValueDocDatePkr.Enabled = false;
                                ValueDocPriceTxt.Name = "ValueDocPriceTxt" + (i + 1);
                                ValueDocPriceTxt.Location = new Point(ValueDocPriceTxt1.Location.X, ValueDocPriceTxt1.Location.Y + 21 * i);
                                ValueDocPriceTxt.Size = ValueDocPriceTxt1.Size;
                                ValueDocPriceTxt.Font = ValueDocPriceTxt1.Font;
                                ValueDocPriceTxt.ReadOnly = true;
                                ValueDocIssuerTxt.Name = "ValueDocIssuerTxt" + (i + 1);
                                ValueDocIssuerTxt.Location = new Point(ValueDocIssuerTxt1.Location.X, ValueDocIssuerTxt1.Location.Y + 21 * i);
                                ValueDocIssuerTxt.Size = ValueDocIssuerTxt1.Size;
                                ValueDocIssuerTxt.Font = ValueDocIssuerTxt1.Font;
                                ValueDocIssuerTxt.ReadOnly = true;
                                ValueDocIdTxt.Text = dt2.Rows[i][0].ToString();
                                ValueDocDatePkr.Text = dt2.Rows[i][1].ToString();
                                ValueDocPriceTxt.Text = dt2.Rows[i][2].ToString();
                                ValueDocIssuerTxt.Text = dt2.Rows[i][3].ToString();
                                this.panel2.Controls.Add(AaTxt);
                                this.panel2.Controls.Add(ValueDocIdTxt);
                                this.panel2.Controls.Add(ValueDocDatePkr);
                                this.panel2.Controls.Add(ValueDocPriceTxt);
                                this.panel2.Controls.Add(ValueDocIssuerTxt);
                                this.ProdPanel.SendToBack();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην αναζήτηση περιγραφής/κωδικού ή Α/Α Απόδειξης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
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
}

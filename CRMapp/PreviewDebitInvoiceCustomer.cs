using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class PreviewDebitInvoiceCustomer : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();

        public PreviewDebitInvoiceCustomer(string Id)
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
                        string query = "select CustomerId,Name,Afm,DebitInvoiceId,DebitInvoiceSeries,DebitInvoiceDate,DebitInvoicePayment,DebitInvoiceDisc,DebitInvoicePrice from CustomerDebitInvoice a, Customers b where a.CustomerId=b.Id and a.Id=@id";
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
                            PayInvoiceTxt.Text = dt.Rows[0][6].ToString();
                            DiscInvoiceTxt.Text = dt.Rows[0][7].ToString();
                            PriceInvoiceTxt.Text = dt.Rows[0][8].ToString();
                        }
                        string query2 = "select ProductId,Description,ProductQuant,ProductPrice,ProductDisc,LongDescr from CustomerDebitInvoiceProducts a, Products b where a.ProductId=b.Id and a.CustomerDebitInvoiceId=@id";
                        SqlDataAdapter SearchAdapt2 = new SqlDataAdapter(query2, sqlcon);
                        SearchAdapt2.SelectCommand.Parameters.AddWithValue(@"id", Id);
                        DataTable dt2 = new DataTable();
                        SearchAdapt2.Fill(dt2);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            ToolTip Tool = new ToolTip();
                            if (i==0)
                            {
                                IdProdTxt1.Text = dt2.Rows[0][0].ToString();
                                DescrProdTxt1.Text = dt2.Rows[0][1].ToString();
                                QuantProdTxt1.Text = dt2.Rows[0][2].ToString();
                                ValueProdTxt1.Text = dt2.Rows[0][3].ToString();
                                DiscProdTxt1.Text = dt2.Rows[0][4].ToString();
                                Tool.Active = true;
                                Tool.InitialDelay = 100;
                                Tool.ReshowDelay = 100;
                                Tool.IsBalloon = true;
                                Tool.ToolTipIcon = ToolTipIcon.Info;
                                Tool.ToolTipTitle = "Αναλυτική Περιγραφή";
                                Tool.SetToolTip(DescrProdTxt1, dt2.Rows[0][5].ToString());
                                Tool.SetToolTip(IdProdTxt1, dt2.Rows[0][5].ToString());

                            }
                            else
                            {
                                this.SuspendLayout();
                                this.ProductsPanel.Height += 21;
                                this.Height += 21;
                                this.ProdPanel.Height += 21;
                                System.Windows.Forms.Label AaTxt = new System.Windows.Forms.Label();
                                System.Windows.Forms.TextBox IdProdTxt = new System.Windows.Forms.TextBox();
                                System.Windows.Forms.TextBox DescrProdTxt = new System.Windows.Forms.TextBox();
                                System.Windows.Forms.TextBox QuantProdTxt = new System.Windows.Forms.TextBox();
                                System.Windows.Forms.TextBox ValueProdTxt = new System.Windows.Forms.TextBox();
                                System.Windows.Forms.TextBox DiscProdTxt = new System.Windows.Forms.TextBox();
                                AaTxt.Text = (i+1).ToString();
                                AaTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                                AaTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                AaTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                                AaTxt.Name = "AaTxt" + (i + 1);
                                AaTxt.Location = new System.Drawing.Point(AaTxt1.Location.X, AaTxt1.Location.Y + 21 * i);
                                AaTxt.Size = AaTxt1.Size;
                                AaTxt.Font = AaTxt1.Font;
                                IdProdTxt.Name = "IdProdTxt" + (i + 1);
                                IdProdTxt.Location = new System.Drawing.Point(IdProdTxt1.Location.X, IdProdTxt1.Location.Y + 21 * i);
                                IdProdTxt.Size = IdProdTxt1.Size;
                                IdProdTxt.Font = IdProdTxt1.Font;
                                IdProdTxt.ReadOnly = true;
                                DescrProdTxt.Name = "DescrProdTxt" + (i + 1);
                                DescrProdTxt.Location = new System.Drawing.Point(DescrProdTxt1.Location.X, DescrProdTxt1.Location.Y + 21 * i);
                                DescrProdTxt.Size = DescrProdTxt1.Size;
                                DescrProdTxt.Font = DescrProdTxt1.Font;
                                DescrProdTxt.ReadOnly = true;
                                QuantProdTxt.Name = "QuantProdTxt" + (i + 1);
                                QuantProdTxt.Location = new System.Drawing.Point(QuantProdTxt1.Location.X, QuantProdTxt1.Location.Y + 21 * i);
                                QuantProdTxt.Size = QuantProdTxt1.Size;
                                QuantProdTxt.Font = QuantProdTxt1.Font;
                                QuantProdTxt.ReadOnly = true;
                                ValueProdTxt.Name = "ValueProdTxt" + (i + 1);
                                ValueProdTxt.Location = new System.Drawing.Point(ValueProdTxt1.Location.X, ValueProdTxt1.Location.Y + 21 * i);
                                ValueProdTxt.Size = ValueProdTxt1.Size;
                                ValueProdTxt.Font = ValueProdTxt1.Font;
                                ValueProdTxt.ReadOnly = true;
                                DiscProdTxt.Name = "DiscProdTxt" + (i + 1);
                                DiscProdTxt.Location = new System.Drawing.Point(DiscProdTxt1.Location.X, DiscProdTxt1.Location.Y + 21 * i);
                                DiscProdTxt.Size = DiscProdTxt1.Size;
                                DiscProdTxt.Font = DiscProdTxt1.Font;
                                DiscProdTxt.ReadOnly = true;
                                IdProdTxt.Text = dt2.Rows[i][0].ToString();
                                DescrProdTxt.Text = dt2.Rows[i][1].ToString();
                                QuantProdTxt.Text = dt2.Rows[i][2].ToString();
                                ValueProdTxt.Text = dt2.Rows[i][3].ToString();
                                DiscProdTxt.Text = dt2.Rows[i][4].ToString();
                                Tool.Active = true;
                                Tool.InitialDelay = 100;
                                Tool.ReshowDelay = 100;
                                Tool.IsBalloon = true;
                                Tool.ToolTipIcon = ToolTipIcon.Info;
                                Tool.ToolTipTitle = "Αναλυτική Περιγραφή";
                                Tool.SetToolTip(DescrProdTxt, dt2.Rows[i][5].ToString());
                                Tool.SetToolTip(IdProdTxt, dt2.Rows[i][5].ToString());
                                this.ProductsPanel.Controls.Add(AaTxt);
                                this.ProductsPanel.Controls.Add(IdProdTxt);
                                this.ProductsPanel.Controls.Add(DescrProdTxt);
                                this.ProductsPanel.Controls.Add(QuantProdTxt);
                                this.ProductsPanel.Controls.Add(ValueProdTxt);
                                this.ProductsPanel.Controls.Add(DiscProdTxt);
                                this.ProdPanel.SendToBack();
                                this.ResumeLayout(false);
                            }
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
}

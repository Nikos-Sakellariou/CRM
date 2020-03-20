using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CRMapp
{

    public partial class Settings : System.Windows.Forms.UserControl
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        List<string> MyProf;

        public Settings()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
            GetData();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Enabled = true;
            OccupationTxt.Enabled = true;
            AfmTxt.Enabled = true;
            Tax_officeTxt.Enabled = true;
            AddressTxt.Enabled = true;
            TkTxt.Enabled = true;
            Phone1Txt.Enabled = true;
            Phone2Txt.Enabled = true;
            EmailTxt.Enabled = true;
            VatTxt.Enabled = true;
            MaxTotalCreditTxt.Enabled = true;
            MaxTotalDebitTxt.Enabled = true;
            InvoiceSeriesTxt.Enabled = true;
            DebitInvoiceSeriesTxt.Enabled = true;
            DisNoteSeriesTxt.Enabled = true;
            ReceiptSeriesTxt.Enabled = true;
            SupplierReceiptSeriesTxt.Enabled = true;
            SaveBtn.Visible = true;
            CancelBtn.Visible = true;
            EditBtn.Visible = false;
            MyProf = new List<string>(new string[] {
            NameTxt.Text,
            OccupationTxt.Text,
            AfmTxt.Text,
            Tax_officeTxt.Text,
            AddressTxt.Text,
            TkTxt.Text,
            Phone1Txt.Text,
            Phone2Txt.Text,
            EmailTxt.Text,
            VatTxt.Text,
            MaxTotalCreditTxt.Text,
            MaxTotalDebitTxt.Text,
            InvoiceSeriesTxt.Text,
            DebitInvoiceSeriesTxt.Text,
            DisNoteSeriesTxt.Text,
            ReceiptSeriesTxt.Text,
            SupplierReceiptSeriesTxt.Text});
        }
        
        private void GetData()
        {
            this.SuspendLayout();
            using (SqlConnection sqlcon = new SqlConnection(con.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select Name,Occupation,Afm,Tax_office,Address,Tk,Phone1,Phone2,Email,Vat,MaxTotalCredit,MaxTotalDebit,InvoiceSeries,DebitInvoiceSeries,DisNoteSeries,ReceiptSeries,SupplierReceiptSeries from MyProfile";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count==1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                NameTxt.Text = dt.Rows[0][0].ToString();
                                OccupationTxt.Text = dt.Rows[0][1].ToString();
                                AfmTxt.Text = dt.Rows[0][2].ToString();
                                Tax_officeTxt.Text = dt.Rows[0][3].ToString();
                                AddressTxt.Text = dt.Rows[0][4].ToString();
                                TkTxt.Text = dt.Rows[0][5].ToString();
                                Phone1Txt.Text = dt.Rows[0][6].ToString();
                                Phone2Txt.Text = dt.Rows[0][7].ToString();
                                EmailTxt.Text = dt.Rows[0][8].ToString();
                                VatTxt.Text = dt.Rows[0][9].ToString();
                                MaxTotalCreditTxt.Text = dt.Rows[0][10].ToString();
                                MaxTotalDebitTxt.Text = dt.Rows[0][11].ToString();
                                InvoiceSeriesTxt.Text = dt.Rows[0][12].ToString();
                                DebitInvoiceSeriesTxt.Text = dt.Rows[0][13].ToString();
                                DisNoteSeriesTxt.Text = dt.Rows[0][14].ToString();
                                ReceiptSeriesTxt.Text = dt.Rows[0][15].ToString();
                                SupplierReceiptSeriesTxt.Text = dt.Rows[0][16].ToString();
                            }
                        }
                        EditBtn.Visible = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση των δεδομένων. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            this.ResumeLayout(false);
        }
       
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            NameTxt.Text=MyProf[0];
            OccupationTxt.Text = MyProf[1];
            AfmTxt.Text = MyProf[2];
            Tax_officeTxt.Text = MyProf[3];
            AddressTxt.Text = MyProf[4];
            TkTxt.Text = MyProf[5];
            Phone1Txt.Text = MyProf[6];
            Phone2Txt.Text = MyProf[7];
            EmailTxt.Text = MyProf[8];
            VatTxt.Text = MyProf[9];
            MaxTotalCreditTxt.Text = MyProf[10];
            MaxTotalDebitTxt.Text = MyProf[11];
            InvoiceSeriesTxt.Text = MyProf[12];
            DebitInvoiceSeriesTxt.Text = MyProf[13];
            DisNoteSeriesTxt.Text = MyProf[14];
            ReceiptSeriesTxt.Text = MyProf[15];
            SupplierReceiptSeriesTxt.Text = MyProf[16];
            NameTxt.Enabled = false;
            OccupationTxt.Enabled = false;
            AfmTxt.Enabled = false;
            Tax_officeTxt.Enabled = false;
            AddressTxt.Enabled = false;
            TkTxt.Enabled = false;
            Phone1Txt.Enabled = false;
            Phone2Txt.Enabled = false;
            EmailTxt.Enabled = false;
            VatTxt.Enabled = false;
            MaxTotalCreditTxt.Enabled = false;
            MaxTotalDebitTxt.Enabled = false;
            InvoiceSeriesTxt.Enabled = false;
            DebitInvoiceSeriesTxt.Enabled = false;
            DisNoteSeriesTxt.Enabled = false;
            ReceiptSeriesTxt.Enabled = false;
            SupplierReceiptSeriesTxt.Enabled = false;
            SaveBtn.Visible = false;
            CancelBtn.Visible = false;
            EditBtn.Visible = true;
            MyProf.Clear();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            List<string> NewMyProf = new List<string>(new string[] {
            NameTxt.Text,
            OccupationTxt.Text,
            AfmTxt.Text,
            Tax_officeTxt.Text,
            AddressTxt.Text,
            TkTxt.Text,
            Phone1Txt.Text,
            Phone2Txt.Text,
            EmailTxt.Text,
            VatTxt.Text,
            MaxTotalCreditTxt.Text,
            MaxTotalDebitTxt.Text,
            InvoiceSeriesTxt.Text,
            DebitInvoiceSeriesTxt.Text,
            DisNoteSeriesTxt.Text,
            ReceiptSeriesTxt.Text,
            SupplierReceiptSeriesTxt.Text});
            int cnt = 0;
            for (int i = 0; i < NewMyProf.Count; i++)
            {
                if (NewMyProf[i]!= MyProf[i])
                {
                    cnt++;
                }
            }
            if (cnt==0)
            {
                MessageBox.Show("Δεν έχετε πραγματοποιήσει κάποια αλλαγή στα στοιχεία.");
            }
            else
            {
                string errorMessages = "";
                if (NameTxt.Text == "" || OccupationTxt.Text == "" || AfmTxt.Text == "" || AddressTxt.Text == "" || TkTxt.Text == "" || Tax_officeTxt.Text == "" || VatTxt.Text == "" || Phone1Txt.Text == "" || InvoiceSeriesTxt.Text == "" || DebitInvoiceSeriesTxt.Text == "" || DisNoteSeriesTxt.Text == "" || ReceiptSeriesTxt.Text == "" || SupplierReceiptSeriesTxt.Text == "")
                {
                    errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα απαραίτητα πεδία.\n";
                }
                else
                {
                    errorMessages += chk.CheckAfm(AfmTxt.Text);
                    errorMessages += chk.CheckTk(TkTxt.Text);
                    errorMessages += chk.CheckPhone(Phone1Txt.Text);
                    errorMessages += chk.CheckVat(VatTxt.Text);
                    if (Phone2Txt.Text != "")
                    {
                        errorMessages += chk.CheckPhone(Phone2Txt.Text);
                    }
                    if (EmailTxt.Text != "")
                    {
                        errorMessages += chk.CheckEmail(EmailTxt.Text);
                    }
                    if (MaxTotalCreditTxt.Text != "")
                    {
                        errorMessages += chk.CheckMaxCredit(MaxTotalCreditTxt.Text);
                    }
                    if (MaxTotalDebitTxt.Text != "")
                    {
                        errorMessages += chk.CheckMaxDebit(MaxTotalDebitTxt.Text);
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
                            sqlcon.Open();
                            
                                SqlCommand Cmd = sqlcon.CreateCommand();
                                Cmd.Connection = sqlcon;
                                if (MyProf[0]=="")
                                {
                                    Cmd.CommandText = "insert into MyProfile (Name,Occupation,Address,Tk,Afm,Tax_office,Phone1,Phone2,Email,Vat,MaxTotalCredit,MaxTotalDebit,InvoiceSeries,DebitInvoiceSeries,DisNoteSeries,ReceiptSeries,SupplierReceiptSeries) values (@name,@occup, @addr, @tk, @afm, @to,@phon1,@phon2,@mail,@vat, @maxcredit,@maxdebit,@invser,@dinvser,@disnser,@recser,@suprecser)";
                                }
                                else
                                {
                                    Cmd.CommandText = "update MyProfile set Name=@name, Occupation=@occup, Address = @addr, Tk = @tk, Afm = @afm, Tax_office = @to,  Phone1= @phon1,  Phone2= @phon2,  Email= @mail,  Vat= @vat,  MaxTotalCredit= @maxcredit,  MaxTotalDebit= @maxdebit, InvoiceSeries=@invser, DebitInvoiceSeries=@dinvser, DisNoteSeries=@disnser, ReceiptSeries=@recser, SupplierReceiptSeries=@suprecser";
                                }
                                Cmd.Parameters.AddWithValue("@name", NameTxt.Text);
                                Cmd.Parameters.AddWithValue("@occup", OccupationTxt.Text);
                                Cmd.Parameters.AddWithValue("@addr", AddressTxt.Text);
                                Cmd.Parameters.AddWithValue("@tk", TkTxt.Text);
                                Cmd.Parameters.AddWithValue("@afm", AfmTxt.Text);
                                Cmd.Parameters.AddWithValue("@to", Tax_officeTxt.Text);
                                Cmd.Parameters.AddWithValue("@phon1", Phone1Txt.Text);
                                Cmd.Parameters.AddWithValue("@phon2", Phone2Txt.Text);
                                Cmd.Parameters.AddWithValue("@mail", EmailTxt.Text);
                                Cmd.Parameters.AddWithValue("@vat", VatTxt.Text);
                                Cmd.Parameters.AddWithValue("@maxcredit", ((MaxTotalCreditTxt.Text == "") ? "0" : MaxTotalCreditTxt.Text));
                                Cmd.Parameters.AddWithValue("@maxdebit", ((MaxTotalDebitTxt.Text == "") ? "0" : MaxTotalDebitTxt.Text));
                                Cmd.Parameters.AddWithValue("@invser", InvoiceSeriesTxt.Text);
                                Cmd.Parameters.AddWithValue("@dinvser", DebitInvoiceSeriesTxt.Text);
                                Cmd.Parameters.AddWithValue("@disnser", DisNoteSeriesTxt.Text);
                                Cmd.Parameters.AddWithValue("@recser", ReceiptSeriesTxt.Text);
                                Cmd.Parameters.AddWithValue("@suprecser", SupplierReceiptSeriesTxt.Text);
                                Cmd.ExecuteNonQuery();
                                MessageBox.Show("Οι αλλαγές πραγματοποιήθηκαν με επιτυχία.");
                                NameTxt.Enabled = false;
                                OccupationTxt.Enabled = false;
                                AfmTxt.Enabled = false;
                                Tax_officeTxt.Enabled = false;
                                AddressTxt.Enabled = false;
                                TkTxt.Enabled = false;
                                Phone1Txt.Enabled = false;
                                Phone2Txt.Enabled = false;
                                EmailTxt.Enabled = false;
                                VatTxt.Enabled = false;
                                MaxTotalCreditTxt.Enabled = false;
                                MaxTotalDebitTxt.Enabled = false;
                                InvoiceSeriesTxt.Enabled = false;
                                DebitInvoiceSeriesTxt.Enabled = false;
                                DisNoteSeriesTxt.Enabled = false;
                                ReceiptSeriesTxt.Enabled = false;
                                SupplierReceiptSeriesTxt.Enabled = false;
                                SaveBtn.Visible = false;
                                CancelBtn.Visible = false;
                                MyProf.Clear();
                                GetData();
                            
                            sqlcon.Close();
                    }
                }
            }
        }

        private void MaxTotalCreditTxt_TextChanged(object sender, EventArgs e)
        {
            MaxTotalCreditTxt.Text = MaxTotalCreditTxt.Text.Replace(',', '.');
            MaxTotalCreditTxt.SelectionStart = MaxTotalCreditTxt.Text.Length;
        }

        private void MaxTotalDebitTxt_TextChanged(object sender, EventArgs e)
        {
            MaxTotalDebitTxt.Text = MaxTotalDebitTxt.Text.Replace(',', '.');
            MaxTotalDebitTxt.SelectionStart = MaxTotalDebitTxt.Text.Length;
        }

        private void InvoiceSeriesTxt_TextChanged(object sender, EventArgs e)
        {
            InvoiceSeriesTxt.Text = InvoiceSeriesTxt.Text.ToUpper();
            InvoiceSeriesTxt.SelectionStart = InvoiceSeriesTxt.Text.Length;
        }

        private void DisNoteSeriesTxt_TextChanged(object sender, EventArgs e)
        {
            DisNoteSeriesTxt.Text = DisNoteSeriesTxt.Text.ToUpper();
            DisNoteSeriesTxt.SelectionStart = DisNoteSeriesTxt.Text.Length;
        }

        private void ReceiptSeriesTxt_TextChanged(object sender, EventArgs e)
        {
            ReceiptSeriesTxt.Text = ReceiptSeriesTxt.Text.ToUpper();
            ReceiptSeriesTxt.SelectionStart = ReceiptSeriesTxt.Text.Length;
        }

        private void DebitInvoiceSeriesTxt_TextChanged(object sender, EventArgs e)
        {
            DebitInvoiceSeriesTxt.Text = DebitInvoiceSeriesTxt.Text.ToUpper();
            DebitInvoiceSeriesTxt.SelectionStart = DebitInvoiceSeriesTxt.Text.Length;
        }

        private void SupplierReceiptSeriesTxt_TextChanged(object sender, EventArgs e)
        {

            SupplierReceiptSeriesTxt.Text = SupplierReceiptSeriesTxt.Text.ToUpper();
            SupplierReceiptSeriesTxt.SelectionStart = SupplierReceiptSeriesTxt.Text.Length;
        }
    }
}

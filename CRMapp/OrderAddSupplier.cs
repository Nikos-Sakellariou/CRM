using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMapp
{
    public partial class OrderAddSupplier : Form
    {
        SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
        Checks chk = new Checks();
        private string SupplierId;
        private string SupplierName;
        private string SupplierAfm;
        private string SupplierDebit;
        private string SupplierOccupation;
        private string SupplierAddress;
        private string SupplierTk;
        private string SupplierTax_office;
        private string SupplierPhone;
        private string SupplierPhone2;
        private string SupplierRegion;
        private string SupplierManager;
        private string SupplierEmail;

        public OrderAddSupplier()
        {
            con = chk.Initiallize_con();
            InitializeComponent();
        }
        public string ReturnSupplierId()
        {
            return SupplierId;
        }
        public string ReturnSupplierName()
        {
            return SupplierName;
        }
        public string ReturnSupplierAfm()
        {
            return SupplierAfm;
        }
        public string ReturnSupplierDebit()
        {
            return SupplierDebit;
        }
        public string ReturnSupplierOccupation()
        {
            return SupplierOccupation;
        }
        public string ReturnSupplierAddress()
        {
            return SupplierAddress;
        }
        public string ReturnSupplierTk()
        {
            return SupplierTk;
        }
        public string ReturnSupplierTax_office()
        {
            return SupplierTax_office;
        }
        public string ReturnSupplierPhone()
        {
            return SupplierPhone;
        }
        public string ReturnSupplierEmail()
        {
            return SupplierEmail;
        }
        public string ReturnSupplierRegion()
        {
            return SupplierRegion;
        }
        public string ReturnSupplierManager()
        {
            return SupplierManager;
        }
        public string ReturnSupplierPhone2()
        {
            return SupplierPhone2;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            string errorMessages = "";
            if (NameTxt.Text == "" || OccupationTxt.Text == "" || AfmTxt.Text == "" || AddressTxt.Text == "" || RegionTxt.Text == "" || TkTxt.Text == "" || PhoneTxt.Text == "" || Tax_officeTxt.Text == "")
            {
                errorMessages = "- Θα πρέπει να συμπληρώσετε όλα τα πεδία με αστερίσκο (*).\n"; ;
            }
            else
            {
                errorMessages += chk.CheckAfm(AfmTxt.Text);
                errorMessages += chk.CheckTk(TkTxt.Text);
                errorMessages += chk.CheckPhone(PhoneTxt.Text);
                if (Phone2Txt.Visible == true)
                {
                    errorMessages += chk.CheckPhone(Phone2Txt.Text);
                }
                if (DebitTxt.Text != "")
                {
                    errorMessages += chk.CheckCredit(DebitTxt.Text);
                }
                if (MaxDebitTxt.Text != "")
                {
                    errorMessages += chk.CheckMaxCredit(MaxDebitTxt.Text);
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
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter("select * from Suppliers where Afm=@parameter", sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue(@"parameter", AfmTxt.Text);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Υπάρχει ήδη καταχωρημένος προμηθευτής με το Α.Φ.Μ. που έχετε εισάγει.");
                        }
                        else
                        {
                            SqlDataAdapter SearchAdapt2 = new SqlDataAdapter("select dbo.nvl(Max(Id)+1,1000) from Suppliers", sqlcon);
                            DataTable dt2 = new DataTable();
                            SearchAdapt2.Fill(dt2);
                            if (dt2.Rows.Count == 1)
                            {
                                SupplierId = dt2.Rows[0][0].ToString();
                            }
                            SqlTransaction InsTrans = sqlcon.BeginTransaction("InsertTransaction");
                            SqlCommand InsCmd1 = sqlcon.CreateCommand();
                            InsCmd1.Connection = sqlcon;
                            InsCmd1.Transaction = InsTrans;
                            try
                            {
                                InsCmd1.CommandText = "insert into Suppliers(Id, Name, Occupation, Address, Tk, Afm, Tax_office,Phone,Email, Debit,Region,Manager,Phone2) values((select dbo.nvl(Max(Id)+1,1000) from dbo.Suppliers), @name, @occup, @addr, @tk, @afm, @to,@phone,@mail, @debit, @reg, @man, @phone2)";
                                InsCmd1.Parameters.AddWithValue("@name", NameTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@occup", OccupationTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@addr", AddressTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@tk", TkTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@afm", AfmTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@to", Tax_officeTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@phone", PhoneTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@mail", EmailTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@debit", ((DebitTxt.Text == "") ? "0" : DebitTxt.Text));
                                InsCmd1.Parameters.AddWithValue("@reg", RegionTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@man", ManagerTxt.Text);
                                InsCmd1.Parameters.AddWithValue("@phone2", Phone2Txt.Text);
                                InsCmd1.ExecuteNonQuery();
                                if (MaxDebitTxt.Text != "")
                                {
                                    InsCmd1.CommandText = "insert into SuppliersMaxDebit (Id,SupplierId,MaxDebit) values ((select dbo.nvl(Max(Id)+1,0) from SuppliersMaxDebit),(select dbo.nvl(Max(Id),1000) from dbo.Suppliers),@maxdebit)";
                                    InsCmd1.Parameters.AddWithValue("@maxdebit", MaxDebitTxt.Text);
                                    InsCmd1.ExecuteNonQuery();
                                }
                                InsTrans.Commit();
                                MessageBox.Show("Ο Προμηθευτής προστέθηκε με επιτυχία.");
                                SupplierName = NameTxt.Text;
                                SupplierAfm = AfmTxt.Text;
                                SupplierDebit = DebitTxt.Text;
                                SupplierOccupation = OccupationTxt.Text;
                                SupplierAddress = AddressTxt.Text;
                                SupplierTk = TkTxt.Text;
                                SupplierTax_office = Tax_officeTxt.Text;
                                SupplierPhone = PhoneTxt.Text;
                                SupplierEmail = EmailTxt.Text;
                                SupplierRegion = RegionTxt.Text;
                                SupplierManager = ManagerTxt.Text;
                                SupplierPhone2 = Phone2Txt.Text;
                                ClearValues();
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
                        this.Close();
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
            NameTxt.Text = "";
            OccupationTxt.Text = "";
            AfmTxt.Text = "";
            AddressTxt.Text = "";
            TkTxt.Text = "";
            Tax_officeTxt.Text = "";
            DebitTxt.Text = "";
            MaxDebitTxt.Text = "";
            PhoneTxt.Text = "";
            EmailTxt.Text = "";
            RegionTxt.Text = "";
            ManagerTxt.Text = "";
            Phone2Txt.Text = "";
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

        private void AddPhoneBtn_Click(object sender, EventArgs e)
        {
            if (Phone2Txt.Visible == true)
            {
                Phone2Txt.Visible = false;
                Phone2Txt.Text = "";
            }
            else
            {
                Phone2Txt.Visible = true;
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

namespace CRMapp
{

    class Checks
    {
        CultureInfo elgr = CultureInfo.CreateSpecificCulture("el-GR");

        static Regex ValidEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                    + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                    + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z\.]*[a-z]$";
            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }
        public string CheckAfm(string Text)
        {
            string Message = "";
            if (Text.Length != 9)
            {
                Message += "- Το Α.Φ.Μ. θα πρέπει να αποτελείται από 9 ψηφία.\n";
            }
            else if (is_int(Text) == false)
            {
                Message += "- Το Α.Φ.Μ. δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            else
            {
                Message += Check_afm_valid(Text);
            }
            return Message;
        }

        public string CheckTk(string Text)
        {
            string Message = "";
            if (Text.Length != 5)
            {
                Message += "- O T.K. θα πρέπει να αποτελείται από 5 ψηφία.\n";
            }
            else if (is_int(Text) == false)
            {
                Message += "- O T.K. δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            return Message;
        }

        public string CheckVat(string Text)
        {
            string Message = "";
            if (Text.Length > 2 || Text.Length < 1)
            {
                Message += "- O Φ.Π.Α. θα πρέπει να αποτελείται από το πολύ 2 ψηφία.\n";
            }
            else if (is_int(Text) == false)
            {
                Message += "- O Φ.Π.Α. δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            return Message;
        }

        public string CheckPhone(string Text)
        {
            string Message = "";
            if (Text.Length < 10)
            {
                Message += "- O αριθμός τηλεφώνου θα πρέπει να αποτελείται από 10 ψηφία.\n";
            }
            else if (is_int(Text.Substring(0, 5)) == false || is_int(Text.Substring(5,5)) == false)
            {
                Message += "- O αριθμός τηλεφώνου δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            else if (Text.Substring(0, 1) != "2" && Text.Substring(0, 2) != "69")
            {
                Message += "- O αριθμός τηλεφώνου που καταχωρήσατε δεν είναι έγκυρος.\n";
            }
            return Message;
        }

        public string CheckCredit(string Text)
        {
            string Message = "";
            if (Text.Length > 10)
            {
                Message += "- Το πιστωτικό υπόλοιπο είναι πολύ μεγάλο.\n";
            }
            else if (is_double(Text) == false)
            {
                Message += "- Το πιστωτικό υπόλοιπο δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            return Message;
        }

        public string CheckMaxCredit(string Text)
        {
            string Message = "";
            if (Text.Length > 15)
            {
                Message += "- Το μέγιστο πιστωτικό υπόλοιπο είναι πολύ μεγάλο.\n";
            }
            else if (is_double(Text) == false)
            {
                Message += "- Το μέγιστο πιστωτικό υπόλοιπο δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            return Message;
        }
        public string CheckDebit(string Text)
        {
            string Message = "";
            if (Text.Length > 10)
            {
                Message += "- Το χρεωστικό υπόλοιπο είναι πολύ μεγάλο.\n";
            }
            else if (is_double(Text) == false)
            {
                Message += "- Το χρεωστικό υπόλοιπο δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            return Message;
        }

        public string CheckMaxDebit(string Text)
        {
            string Message = "";
            if (Text.Length > 15)
            {
                Message += "- Το μέγιστο χρεωστικό υπόλοιπο είναι πολύ μεγάλο.\n";
            }
            else if (is_double(Text) == false)
            {
                Message += "- Το μέγιστο χρεωστικό υπόλοιπο δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            return Message;
        }

        public string CheckEmail(string Text)
        {
            string Message = "";
            if (ValidEmailRegex.IsMatch(Text)==false)
            {
                Message += "- Το email που καταχωρήσατε δεν είναι έγκυρο.\n";
            }
            return Message;
        }

        public string CheckQuant(string Text)
        {
            string Message = "";
            if (Text.Length > 10)
            {
                Message += "- Η ποσότητα του προϊόντος που δηλώσατε είναι πολύ μεγάλη.\n";
            }
            else if (is_double(Text) == false)
            {
                Message += "- Η ποσότητα του προϊόντος δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            else if (Text.Length == 0)
            {
                Message += "- Θα πρέπει να συμπληρώσετε την ποσότητα του προϊόντος.\n";
            }
            return Message;
        }

        public string CheckPrice(string Text)
        {
            string Message = "";
            try
            {
                double Dbl = Convert.ToDouble(Text);
            }
            catch (Exception)
            {
                Message += "- Το ποσό που δηλώσατε δεν είναι έγκυρο.\n";
            }
            return Message;
        }

        public string CheckMinStock(string Text)
        {
            string Message = "";
            if (Text.Length > 10)
            {
                Message += "- Το ελάχιστο stock του προϊόντος που δηλώσατε είναι πολύ μεγάλο.\n";
            }
            else if (is_double(Text) == false)
            {
                Message += "- Το ελάχιστο stock του προϊόντος δεν μπορεί να περιέχει χαρακτήρες ή ειδικά σύμβολα.\n";
            }
            return Message;
        }

        public string GrNumber(string Text)
        {
            string result = "";
            decimal decim = 0;
            try
            {
                decim = Convert.ToDecimal(Text);
                result = decim.ToString("#,0.00", elgr);
            }
            catch (Exception)
            {
                result = Text;
            }
            return result;
        }

        public string GrQuant(string Text)
        {
            string result = "";
            decimal decim = 0;
            try
            {
                decim = Convert.ToDecimal(Text);
                result = decim.ToString("#,0.000", elgr);
            }
            catch (Exception)
            {
                result = Text;
            }
            return result;
        }

        private string Check_afm_valid(string v_afm)
        {
            int v_sum = 0;
            int v_residual;
            int v_lastdigit;
            for (int v_i = 1; v_i < 9; v_i++)
            {
                v_sum = v_sum + (int)Math.Pow(2, (9 - v_i)) * Convert.ToInt16(v_afm.Substring(v_i - 1, 1));
            }
            v_residual = v_sum % 11;
            if (v_residual == 10)
            {
                v_lastdigit = 0;
            }
            else
            {
                v_lastdigit = v_residual;
            }
            if (v_lastdigit != Convert.ToInt16(v_afm.Substring(8, 1)))
            {
                return "- Το Α.Φ.Μ. που καταχωρήσατε δεν είναι έγκυρο.\n";
            }
            else
            {
                return "";
            }
        }

        private bool is_int(string num)
        {
            int chk;
            try
            {
                chk = Convert.ToInt32(num);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        private bool is_double(string num)
        {
            double chk;
            try
            {
                chk = Convert.ToDouble(num);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static string RemoveDiacritics(string s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public SqlConnectionStringBuilder Initiallize_con()
        {
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CRM_DB.mdf;Integrated Security=True;Connect Timeout=30";
            con.UserID = "admin";
            con.Password = "admin";
            return con;
        }

        public decimal Return_Vat(string CustId)
        {
            decimal vat = 0;
            using (SqlConnection sqlcon = new SqlConnection(Initiallize_con().ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select dbo.FVAL_CUSTOMER_VAT(@id,(select Vat from MyProfile))";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        SearchAdapt.SelectCommand.Parameters.AddWithValue("@id", CustId);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            vat = Convert.ToDecimal(dt.Rows[0][0].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση του ΦΠΑ. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            return vat;
        }

        public string Return_InvoiceSeries()
        {
            string invser = "";
            using (SqlConnection sqlcon = new SqlConnection(Initiallize_con().ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select InvoiceSeries from MyProfile";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            invser = dt.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση της σειράς του τιμολογίου. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            return invser;
        }
        public string Return_DebitInvoiceSeries()
        {
            string dinvser = "";
            using (SqlConnection sqlcon = new SqlConnection(Initiallize_con().ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select DebitInvoiceSeries from MyProfile";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            dinvser = dt.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση της σειράς του πιστωτικού τιμολογίου. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            return dinvser;
        }

        public string Return_DisNoteSeries()
        {
            string disnser = "";
            using (SqlConnection sqlcon = new SqlConnection(Initiallize_con().ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select DisNoteSeries from MyProfile";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            disnser = dt.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση της σειράς του δελτίου αποστολής. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            return disnser;
        }

        public string Return_ReceiptSeries()
        {
            string recser = "";
            using (SqlConnection sqlcon = new SqlConnection(Initiallize_con().ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select ReceiptSeries from MyProfile";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            recser = dt.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση της σειράς της απόδειξης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            return recser;
        }

        public string Return_SupplierReceiptSeries()
        {
            string recser = "";
            using (SqlConnection sqlcon = new SqlConnection(Initiallize_con().ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    try
                    {
                        string query = "select SupplierReceiptSeries from MyProfile";
                        SqlDataAdapter SearchAdapt = new SqlDataAdapter(query, sqlcon);
                        DataTable dt = new DataTable();
                        SearchAdapt.Fill(dt);
                        if (dt.Rows.Count == 1)
                        {
                            recser = dt.Rows[0][0].ToString();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Παρουσιάστηκε κάποιο μη αναμενόμενο σφάλμα στην ανάκτηση της σειράς της απόδειξης πληρωμής. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                    }
                    sqlcon.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Σφάλμα σύνδεσης. Παρακαλώ επικοινωνήστε με το διαχειριστή του συστήματος.");
                }
            }
            return recser;
        }

        public string Number_fullname(int number)
        {
            if (number == 0)
            {
                return "μηδέν";
            }
            if (number < 0)
            {
                return "μείον " + Number_fullname(Math.Abs(number));
            }
            string fullname = "";
            if ((number / 1000000) == 1)
            {
                fullname += Number_fullname(number / 1000000) + " εκατομμύριο ";
                number %= 1000000;
            }
            else if ((number / 1000000) > 1)
            {
                fullname += Number_fullname(number / 1000000) + " εκατομμύρια ";
                number %= 1000000;
            }
            if ((number / 1000) == 1)
            {
                fullname += " χίλια ";
                number %= 1000;
            }
            else if ((number / 1000) > 1)
            {
                fullname += Number_fullname(number / 1000).Replace("κόσια", "κόσιες").Replace("τέσσερα", "τέσσερις").Replace("τρία", "τρεις").Replace("ένα", "μία") + " χιλιάδες ";
                number %= 1000;
            }
            if ((number / 100) == 1)
            {
                fullname += " εκατό ";
                number %= 100;
            }
            else if ((number / 100) == 2)
            {
                fullname += " διακόσια ";
                number %= 100;
            }
            else if ((number / 100) == 3)
            {
                fullname += " τριακόσια ";
                number %= 100;
            }
            else if ((number / 100) == 4)
            {
                fullname += " τετρακόσια ";
                number %= 100;
            }
            else if ((number / 100) == 5)
            {
                fullname += " πεντακόσια ";
                number %= 100;
            }
            else if ((number / 100) == 6)
            {
                fullname += " εξακόσια ";
                number %= 100;
            }
            else if ((number / 100) == 7)
            {
                fullname += " επτακόσια ";
                number %= 100;
            }
            else if ((number / 100) == 8)
            {
                fullname += " οκτακόσια ";
                number %= 100;
            }
            else if ((number / 100) == 9)
            {
                fullname += " εννιακόσια ";
                number %= 100;
            }
            if ((number / 10) == 1)
            {
                fullname += " δέκα ";
                number %= 10;
            }
            else if ((number / 10) == 2)
            {
                fullname += " είκοσι ";
                number %= 10;
            }
            else if ((number / 10) == 3)
            {
                fullname += " τριάντα ";
                number %= 10;
            }
            else if ((number / 10) == 4)
            {
                fullname += " σαράντα ";
                number %= 10;
            }
            else if ((number / 10) == 5)
            {
                fullname += " πενήντα ";
                number %= 10;
            }
            else if ((number / 10) == 6)
            {
                fullname += " εξήντα ";
                number %= 10;
            }
            else if ((number / 10) == 7)
            {
                fullname += " εβδομήντα ";
                number %= 10;
            }
            else if ((number / 10) == 8)
            {
                fullname += " ογδόντα ";
                number %= 10;
            }
            else if ((number / 10) == 9)
            {
                fullname += " εννενήντα ";
                number %= 10;
            }

            if (number == 1)
            {
                fullname += " ένα ";
            }
            else if (number == 2)
            {
                fullname += " δύο ";
            }
            else if (number == 3)
            {
                fullname += " τρία ";
            }
            else if (number == 4)
            {
                fullname += " τέσσερα ";
            }
            else if (number == 5)
            {
                fullname += " πέντε ";
            }
            else if (number == 6)
            {
                fullname += " έξι ";
            }
            else if (number == 7)
            {
                fullname += " επτά ";
            }
            else if (number == 8)
            {
                fullname += " οκτώ ";
            }
            else if (number == 9)
            {
                fullname += " εννιά ";
            }

            return fullname;
        }
    }
}

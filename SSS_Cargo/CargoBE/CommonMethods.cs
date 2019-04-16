using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CargoBE
{
    public static class CommonMethods
    {
        private const string initVector = "tu89geji340t89u2";
        private const string encryptKey = "$S$c@Rg0";
        private const int keysize = 256;

        #region Unique Codes & Random Strings

        public static string GenerateUserPassword()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            int length = 8;
            string password = string.Empty;

            string characters = numbers + alphabets + small_alphabets + numbers;

            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (password.IndexOf(character) != -1);
                password += character;
            }

            return password;
        }

        #endregion

        #region Encryption & Decryption

        public static string Encrypt(string Text)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
            PasswordDeriveBytes password = new PasswordDeriveBytes(encryptKey, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Encrypted = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(Encrypted);
        }

        public static string Decrypt(string EncryptedText)
        {
            string str = string.Empty;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(encryptKey, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[DeEncryptedText.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            str = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return str;
        }

        public static string URLKeyEncrypt(string clearText)
        {
            string encryptionkey = "R@cru!t@r#V!ll@";

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionkey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText.Replace("/", "-").Replace("+", "%20");
        }

        public static string URLKeyDecrypt(string cipherText)
        {
            string encryptionkey = "R@cru!t@r#V!ll@";
            cipherText = cipherText.Replace(" ", "+").Replace("%20", "+").Replace("-", "/");

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionkey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private static byte[] key = { };

        /// <summary>
        /// Declare the Local Variable
        /// </summary>
        private static byte[] iV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        /// <summary>
        /// Decrypts the specified string to decrypt.
        /// </summary>
        /// <param name="stringToDecrypt">The string to decrypt.</param>
        /// <param name="decryptionKey">The decryption key.</param>
        /// <returns>Return string</returns>
        public static string DecryptValue(string stringToDecrypt, string decryptionKey)
        {
            ///// byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            byte[] inputByteArray;
            try
            {
                stringToDecrypt = stringToDecrypt.Replace(" ", "+");
                stringToDecrypt = stringToDecrypt.Replace("-", "/");
                key = System.Text.Encoding.UTF8.GetBytes(Left(decryptionKey, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, iV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Lefts the specified param.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <param name="length">The length.</param>
        /// <returns>Return string</returns>
        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }

        /// <summary>
        /// Rights the specified param.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <param name="length">The length.</param>
        /// <returns>Return string</returns>
        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }

        /// <summary>
        /// Encrypts the specified string to encrypt.
        /// </summary>
        /// <param name="stringToEncrypt">The string to encrypt.</param>
        /// <param name="encryptionKey">The encryption key.</param>
        /// <returns>Return string</returns>
        public static string EncryptValue(string stringToEncrypt, string encryptionKey)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(Left(encryptionKey, 8));
                DESCryptoServiceProvider encrypt = new DESCryptoServiceProvider();
                byte[] inputByteArrayEncrypt = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encrypt.CreateEncryptor(key, iV), CryptoStreamMode.Write);
                cryptoStream.Write(inputByteArrayEncrypt, 0, inputByteArrayEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray()).Replace("/", "-");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        #endregion

        #region Error Logs

        public static void ErrorMessage(string Message)
        {
            try
            {
                StackTrace st = new StackTrace(true);
                StackFrame frame = st.GetFrame(0);//Get the first stack frame
                int LineNumber = frame.GetFileLineNumber();//Get the line number from the stack frame

                if (!File.Exists(HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["ErrorMessages"])))
                {
                    StreamWriter sw = File.CreateText(HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["ErrorMessages"]));
                    StoreDetails(sw, Message, LineNumber);
                    sw.Close();

                }
                else
                {
                    StreamWriter sw;
                    sw = File.AppendText(HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["ErrorMessages"]));
                    StoreDetails(sw, Message, LineNumber);
                    sw.Close();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static DateTime GetIndiaTime()
        {
            TimeZoneInfo IND_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime currentTime = DateTime.Now;
            return TimeZoneInfo.ConvertTime(currentTime, IND_ZONE);
        }

        private static void StoreDetails(StreamWriter sw, string Message, int LineNumber)
        {
            DateTime ourTime = GetIndiaTime();
            try
            {
                sw.WriteLine("    " + Message + "      | LineNumber :    " + LineNumber);
                sw.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region Send Email

        public static void SendEmail(string toEmailIds, string subject, string body)
        {
            try
            {
                Thread email = new Thread(delegate ()
                {
                    if (ConfigurationSettings.AppSettings["SendMailFrom"].ToString() == "1")//To send email from Gmail
                    {
                        SendEmailFromGmail(toEmailIds, subject, body);
                    }
                    else
                    {
                        SendMailFromGodaddy(toEmailIds, subject, body);
                    }
                });
                email.IsBackground = true;
                email.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendEmail(string toEmailIds, string subject, string body, string[] attachments, string[] cc)
        {
            try
            {
                Thread email = new Thread(delegate ()
                {
                    if (ConfigurationSettings.AppSettings["SendMailFrom"].ToString() == "1")//To send email from Gmail
                    {
                        SendEmailFromGmail(toEmailIds, subject, body, attachments, cc);
                    }
                    else
                    {
                        SendMailFromGodaddy(toEmailIds, subject, body, attachments, cc);
                    }
                });
                email.IsBackground = true;
                email.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async void SendEmailFromGmail(string toEmailIds, string subject, string body)
        {
            string emailFrom = ConfigurationSettings.AppSettings["FromMailId"].ToString();
            string password = ConfigurationSettings.AppSettings["FromMailPassword"].ToString();
            string emailTo = toEmailIds;

            using (MailMessage mail = new MailMessage(emailFrom, emailTo))
            {
                mail.Subject = subject;
                mail.Body = body;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(emailFrom, password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                try
                {
                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async void SendEmailFromGmail(string toEmailIds, string subject, string body, string[] attachments, string[] cc)
        {
            string emailFrom = ConfigurationSettings.AppSettings["FromMailId"].ToString();
            string password = ConfigurationSettings.AppSettings["FromMailPassword"].ToString();
            string emailTo = toEmailIds;

            using (MailMessage mail = new MailMessage(emailFrom, emailTo))
            {
                mail.Subject = subject;
                mail.Body = body;
                if (attachments != null && attachments.Length > 0)
                {

                    foreach (string attachment in attachments)
                    {
                        if (!string.IsNullOrEmpty(attachment))
                        {
                            mail.Attachments.Add(new Attachment(attachment));
                        }
                    }
                }
                if (cc != null && cc.Length > 0)
                {
                    foreach (string c in cc)
                    {
                        mail.CC.Add(new MailAddress(c));
                    }
                }
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(emailFrom, password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                try
                {
                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async void SendMailFromGodaddy(string toEmailIds, string subject, string body)
        {
            string emailFrom = ConfigurationSettings.AppSettings["FromMailId"].ToString();
            string password = ConfigurationSettings.AppSettings["FromMailPassword"].ToString();
            //Create the msg object to be sent
            MailMessage msg = new MailMessage();
            //Add your email address to the recipients
            msg.To.Add(toEmailIds);
            //Configure the address we are sending the mail from
            msg.From = new MailAddress(emailFrom);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Host = "relay-hosting.secureserver.net";
            client.Credentials = new System.Net.NetworkCredential(emailFrom, password);
            client.EnableSsl = false;
            client.Port = 25;
            client.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
            try
            {
                await client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async void SendMailFromGodaddy(string toEmailIds, string subject, string body, string[] attachments, string[] cc)
        {
            string emailFrom = ConfigurationSettings.AppSettings["FromMailId"].ToString();
            string password = ConfigurationSettings.AppSettings["FromMailPassword"].ToString();
            //Create the msg object to be sent
            MailMessage msg = new MailMessage();
            //Add your email address to the recipients
            msg.To.Add(toEmailIds);
            //Configure the address we are sending the mail from
            msg.From = new MailAddress(emailFrom);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            if (attachments.Length > 0)
            {
                foreach (string attachment in attachments)
                {
                    msg.Attachments.Add(new Attachment(HttpContext.Current.Server.MapPath(attachment)));
                }
            }
            if (cc.Length > 0)
            {
                foreach (string c in cc)
                {
                    msg.CC.Add(new MailAddress(c));
                }
            }
            SmtpClient client = new SmtpClient();
            client.Host = "relay-hosting.secureserver.net";
            client.Credentials = new System.Net.NetworkCredential(emailFrom, password);
            client.EnableSsl = false;
            client.Port = 25;
            client.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
            try
            {
                await client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MailMessage mail = e.UserState as MailMessage;

            if (!e.Cancelled && e.Error != null)
            {

            }
        }

        public static bool IsValidEmailId(string email)
        {
            string expresion;
            expresion = @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z";
            bool isEmail = Regex.IsMatch(email.Trim(), expresion);
            if (!isEmail)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void SendMultipleEmail(string toEmailIds, string subject, string body)
        {
            try
            {
                Thread email = new Thread(delegate ()
                {
                    if (ConfigurationSettings.AppSettings["SendMailFrom"].ToString() == "1")//To send email from Gmail
                    {
                        SendMutipleEmailsFromGmail(toEmailIds, subject, body);
                    }
                    else
                    {
                        SendMultipleMailFromGodaddy(toEmailIds, subject, body);
                    }
                });
                email.IsBackground = true;
                email.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async void SendMutipleEmailsFromGmail(string toEmailIds, string subject, string body)
        {
            string emailFrom = ConfigurationSettings.AppSettings["FromMailId"].ToString();
            string password = ConfigurationSettings.AppSettings["FromMailPassword"].ToString();
            string emailTo = toEmailIds;

            using (MailMessage mail = new MailMessage(emailFrom, emailTo))
            {
                mail.Subject = subject;
                mail.Body = body;

                //Adding Multiple recipient email id logic
                string[] Multi = toEmailIds.Split(','); //spiliting input Email id string with comma(,)
                foreach (string Multiemailid in Multi)
                {
                    mail.To.Add(new MailAddress(Multiemailid)); //adding multi reciver's Email Id
                }
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(emailFrom, password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                try
                {
                    await smtp.SendMailAsync(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static async void SendMultipleMailFromGodaddy(string toEmailIds, string subject, string body)
        {
            string emailFrom = ConfigurationSettings.AppSettings["FromMailId"].ToString();
            string password = ConfigurationSettings.AppSettings["FromMailPassword"].ToString();
            //Create the msg object to be sent
            MailMessage msg = new MailMessage();
            //Add your email address to the recipients

            //Adding Multiple recipient email id logic
            string[] Multi = toEmailIds.Split(','); //spiliting input Email id string with comma(,)
            foreach (string Multiemailid in Multi)
            {
                msg.To.Add(Multiemailid);
                //Configure the address we are sending the mail from
            }
            msg.From = new MailAddress(emailFrom);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();

            client.Host = "relay-hosting.secureserver.net";
            client.Credentials = new System.Net.NetworkCredential(emailFrom, password);
            client.EnableSsl = false;
            client.Port = 25;
            client.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
            try
            {
                await client.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Send SMS

        public static void SendSMS(string mobilenumber, string message)
        {
            string SMSAPIKey = ConfigurationSettings.AppSettings["SMSAPIKey"].ToString();
            string SMSSenderName = ConfigurationSettings.AppSettings["SMSSenderName"].ToString();

            try
            {
                string url = "http://sms.adeep.in/api/v4/?api_key=" + SMSAPIKey + "&method=sms&message=" + message + "&to=" + mobilenumber + "&sender=" + SMSSenderName;
                string resultData = string.Empty;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    resultData = reader.ReadToEnd();
                }
                //emailresult = JsonConvert.DeserializeObject<EmailVerificationResponse>(resultData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

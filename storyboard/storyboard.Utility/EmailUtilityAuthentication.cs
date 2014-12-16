using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace storyboard.Utility
{
    /// <summary>
    /// This class is using System.Net.Mail 
    /// Don't need to pass credentials for SMTP server here.
    /// </summary>
    public class EmailUtilityAuthentication
    {
        #region [Email Methods]
        private static SmtpClient oSmtpClient = new SmtpClient(ConfigurationSettings.SmtpHost);

        /// <summary>
        /// Send mail with minimum settings
        /// </summary>
        /// <param name="sFromAddress"></param>
        /// <param name="sToAddress"></param>
        /// <param name="sSubject"></param>
        /// <param name="sBody"></param>
        /// <returns></returns>
        public static Boolean SendMail(string sFromAddress, string sToAddress, string sSubject, string sBody, bool isHtmlBody)
        {
            Boolean oResult = false;

            //Code for testing mail send from localhost
            //if (oSmtpClient != null)
            //    oSmtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

            //Collect credential to send mail.
            string sNetworkUserName = "";
            string sNetworkPassword = "";

            if (!string.IsNullOrEmpty(ConfigurationSettings.EmailCredentialUserName))
                sNetworkUserName = ConfigurationSettings.EmailCredentialUserName;
            if (!string.IsNullOrEmpty(ConfigurationSettings.EmailCredentialPassword))
                sNetworkPassword = ConfigurationSettings.EmailCredentialPassword;

            /* Email with Authentication */
            if (sNetworkUserName != string.Empty && sNetworkPassword != string.Empty)
            {
                oSmtpClient.UseDefaultCredentials = true;
                /*oSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                oSmtpClient.Credentials = new NetworkCredential(sNetworkUserName, sNetworkPassword);*/
            }

            try
            {
                string sFromDisplayName = "";
                if (!string.IsNullOrEmpty(ConfigurationSettings.AdminDisplayName))
                    sFromDisplayName = ConfigurationSettings.AdminDisplayName;

                MailAddress fromAddress = new MailAddress(sFromAddress, sFromDisplayName);
                MailAddress toAddress = new MailAddress(sToAddress, "");

                //MailMessage oMessage = new MailMessage(sFromAddress, sToAddress, sSubject, sBody);
                MailMessage oMessage = new MailMessage(fromAddress, toAddress);

                //Send copy of email mail out to admin.
                oMessage.Bcc.Add(ConfigurationSettings.SendMailCopyToAdmin);
                oSmtpClient.Port = ConfigurationSettings.ClientPort;
                oSmtpClient.EnableSsl = ConfigurationSettings.EnableSSL;
                if (sSubject != string.Empty)
                    oMessage.Subject = sSubject;

                oMessage.Body = sBody;
                oMessage.IsBodyHtml = isHtmlBody;

                if (oMessage.From != null && oMessage.To != null && oMessage.Body != string.Empty)
                {
                    //Send email
                    //oSmtpClient.EnableSsl = true;
                    oSmtpClient.Send(oMessage);
                    oResult = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oResult;
        }
        public static Boolean SendMail(string ToAddress, string ToName, string FromAddress, string EmailSubject, string EmailBody, bool isHtmlBody)
        {
            Boolean oResult = false;

            #region
            string sNetworkUserName = "";
            string sNetworkPassword = "";

            if (!string.IsNullOrEmpty(ConfigurationSettings.EmailCredentialUserName))
                sNetworkUserName = ConfigurationSettings.EmailCredentialUserName;
            if (!string.IsNullOrEmpty(ConfigurationSettings.EmailCredentialPassword))
                sNetworkPassword = ConfigurationSettings.EmailCredentialPassword;

            /* Email with Authentication */
            if (sNetworkUserName != string.Empty && sNetworkPassword != string.Empty)
            {
                oSmtpClient.UseDefaultCredentials = true;
            }
            #endregion

            #region Try catch Send mail
            try
            {
                string sFromDisplayName = "";
                if (!string.IsNullOrEmpty(ConfigurationSettings.AdminDisplayName))
                    sFromDisplayName = ConfigurationSettings.AdminDisplayName;

                MailAddress fromAddress = new MailAddress(FromAddress, "");
                MailAddress toAddress = new MailAddress(ToAddress, ToName);
                MailMessage oMessage = new MailMessage(fromAddress, toAddress);
                oSmtpClient.Port = ConfigurationSettings.ClientPort;
                oSmtpClient.EnableSsl = ConfigurationSettings.EnableSSL;
                if (ConfigurationSettings.SendMailCopyToAdmin.ToStringSafe().ToLower() == "true")
                {
                    oMessage.Bcc.Add(ConfigurationSettings.SendMailCopyToAdmin);
                }

                if (EmailSubject != string.Empty)
                    oMessage.Subject = EmailSubject;

                oMessage.Body = EmailBody;
                oMessage.IsBodyHtml = isHtmlBody;

                if (oMessage.From != null && oMessage.To != null && oMessage.Body != string.Empty)
                {

                    oSmtpClient.Send(oMessage);
                    oResult = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion Try Catch
            return oResult;
        }

        #endregion
    }
}

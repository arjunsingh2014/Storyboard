using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Mail;

namespace storyboard.Utility
{
    /// <summary>
    /// This class is using System.Web.Mail 
    /// Don't need to pass credentials for SMTP server here.
    /// </summary>
    public static class EmailUtility
    {
        #region [Email Methods]
        //private static SmtpClient oSmtpClient = new SmtpClient(ConfigurationManager.AppSettings["SmtpHost"].ToString());
        /// <summary>
        /// Send mail with minimum settings
        /// </summary>
        /// <param name="sFromAddress"></param>
        /// <param name="sToAddress"></param>
        /// <param name="sSubject"></param>
        /// <param name="sBody"></param>
        /// <returns></returns>
        public static Boolean SendMail(string sFromAddress, string sToAddress, string sSubject, string sBody)
        {
            Boolean oResult = false;
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
                //oSmtpClient.UseDefaultCredentials = true;
            }
            try
            {
                string sFromDisplayName = "";
                if (!string.IsNullOrEmpty(ConfigurationSettings.AdminDisplayName))
                    sFromDisplayName = ConfigurationSettings.AdminDisplayName;

                //MailAddress fromAddress = new MailAddress(sFromAddress, sFromDisplayName);
                //MailAddress toAddress = new MailAddress(sToAddress, "");

                //MailMessage oMessage = new MailMessage(sFromAddress, sToAddress, sSubject, sBody);
                MailMessage oMessage = new MailMessage();//fromAddress, toAddress
                oMessage.From = sFromAddress;
                oMessage.To = sToAddress;
                //Send copy of email mail out to admin.
                string bccaddress = string.Empty;
                if (!string.IsNullOrEmpty(ConfigurationSettings.SendMailCopyToAdmin))
                    bccaddress = ConfigurationSettings.SendMailCopyToAdmin;
                if (!string.IsNullOrEmpty(bccaddress))
                    oMessage.Bcc = bccaddress;
                if (sSubject != string.Empty)
                    oMessage.Subject = sSubject;
                oMessage.Body = sBody;
                oMessage.BodyFormat = MailFormat.Html;
                if (oMessage.From != null && oMessage.To != null && oMessage.Body != string.Empty)
                {
                    SmtpMail.SmtpServer = ConfigurationSettings.SmtpHost;
                    SmtpMail.Send(oMessage);
                    //oSmtpClient.Send(oMessage);
                    oResult = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResult;
        }

        public static Boolean SendMail(string ToAddress, string ToName, string FromAddress, string EmailSubject, string EmailBody)
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
                //oSmtpClient.UseDefaultCredentials = true;
            }
            #endregion
            #region Try catch Send mail
            try
            {
                string sFromDisplayName = "";
                if (!string.IsNullOrEmpty(ConfigurationSettings.AdminDisplayName))
                    sFromDisplayName = ConfigurationSettings.AdminDisplayName;

                //MailAddress fromAddress = new MailAddress(FromAddress, "");
                //MailAddress toAddress = new MailAddress(ToAddress, ToName);
                MailMessage oMessage = new MailMessage();//fromAddress, toAddress
                oMessage.From = FromAddress;
                oMessage.To = ToAddress;

                string bccaddress = string.Empty;
                //Send copy of email mail out to admin.
                if (!string.IsNullOrEmpty(ConfigurationSettings.SendMailCopyToAdmin))
                    bccaddress = ConfigurationSettings.SendMailCopyToAdmin;
                if (!string.IsNullOrEmpty(bccaddress))
                    oMessage.Bcc = bccaddress;

                if (EmailSubject != string.Empty)
                    oMessage.Subject = EmailSubject;
                oMessage.Body = EmailBody;
                //oMessage.IsBodyHtml = true;
                oMessage.BodyFormat = MailFormat.Html;
                if (oMessage.From != null && oMessage.To != null && oMessage.Body != string.Empty)
                {
                    SmtpMail.SmtpServer = ConfigurationSettings.SmtpHost;
                    SmtpMail.Send(oMessage);
                    //oSmtpClient.Send(oMessage);
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

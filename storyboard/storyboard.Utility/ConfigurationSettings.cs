using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace storyboard.Utility
{
    public static class ConfigurationSettings
    {
        #region Get Config Settings

        public static string ConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["DBConnection"].ToStringSafe();
            }
        }

        public static string SmtpHost
        {
            get
            {
                return WebConfigurationManager.AppSettings["SmtpHost"].Trim().ToStringSafe();
            }
        }

        public static string EmailCredentialUserName
        {
            get
            {
                return WebConfigurationManager.AppSettings["EmailCredentialUserName"].Trim().ToStringSafe();
            }
        }

        public static string EmailCredentialPassword
        {
            get
            {
                return WebConfigurationManager.AppSettings["EmailCredentialPassword"].Trim().ToStringSafe();
            }
        }

        public static string AdminDisplayName
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminDisplayName"].Trim().ToStringSafe();
            }
        }


        public static string SendMailCopyToAdmin
        {
            get
            {
                return WebConfigurationManager.AppSettings["SendMailCopyToAdmin"].Trim().ToStringSafe();
            }
        }

        public static int ClientPort
        {
            get
            {
                return WebConfigurationManager.AppSettings["ClientPort"].Trim().ToIntSafe();
            }
        }

        public static bool EnableSSL
        {
            get
            {
                return WebConfigurationManager.AppSettings["EnableSSL"].Trim().ToBooleanSafe();
            }
        }

        #endregion
    }
}

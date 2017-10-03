using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Email
{
    public class EmailSenderOption
    {
        /// <summary>
        /// Default from address.
        /// </summary>
        public string DefaultFromAddress
        {
            get;
        }

        /// <summary>
        /// Default display name.
        /// </summary>
        public string DefaultFromDisplayName
        {
            get;
        }
        /// <summary>
        /// SMTP Host name/IP.
        /// </summary>
        public string Host
        {
            get;
        }

        /// <summary>
        /// SMTP Port.
        /// </summary>
        public int Port
        {
            get;
        }

        /// <summary>
        /// User name to login to SMTP server.
        /// </summary>
        public string UserName
        {
            get;
        }

        /// <summary>
        /// Password to login to SMTP server.
        /// </summary>
        public string Password
        {
            get;
        }

        /// <summary>
        /// Domain name to login to SMTP server.
        /// </summary>
        public string Domain
        {
            get;
        }

        /// <summary>
        /// Is SSL enabled?
        /// </summary>
        public bool EnableSsl
        {
            get;
        }

        /// <summary>
        /// Use default credentials?
        /// </summary>
        public bool UseDefaultCredentials
        {
            get;
        }
    }
}

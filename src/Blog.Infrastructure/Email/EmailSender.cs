using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Email
{
    /// <summary>
    /// This class can be used as base to implement <see cref="T:Abp.Net.Mail.IEmailSender" />.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private EmailSenderOption _option;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="option">Configuration</param>
        protected EmailSender(IOptionsSnapshot<EmailSenderOption> option)
        {
            this._option = option.Value;

        }

        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await this.SendAsync(this._option.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            this.Send(this._option.DefaultFromAddress, to, subject, body, isBodyHtml);
        }

        public async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            await this.SendAsync(new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = isBodyHtml
            }, true);
        }

        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            this.Send(new MailMessage(from, to, subject, body)
            {
                IsBodyHtml = isBodyHtml
            }, true);
        }

        public async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                this.NormalizeMail(mail);
            }
            await this.SendEmailAsync(mail);
        }

        public void Send(MailMessage mail, bool normalize = true)
        {
            if (normalize)
            {
                this.NormalizeMail(mail);
            }
            this.SendEmail(mail);
        }

        /// <summary>
        /// Normalizes given email.
        /// Fills <see cref="P:System.Net.Mail.MailMessage.From" /> if it's not filled before.
        /// Sets encodings to UTF8 if they are not set before.
        /// </summary>
        /// <param name="mail">Mail to be normalized</param>
        protected virtual void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty())
            {
                mail.From = new MailAddress(this._option.DefaultFromAddress, this._option.DefaultFromDisplayName, Encoding.UTF8);
            }
            if (mail.HeadersEncoding == null)
            {
                mail.HeadersEncoding = Encoding.UTF8;
            }
            if (mail.SubjectEncoding == null)
            {
                mail.SubjectEncoding = Encoding.UTF8;
            }
            if (mail.BodyEncoding == null)
            {
                mail.BodyEncoding = Encoding.UTF8;
            }
        }
        public SmtpClient BuildClient()
        {
            var host = _option.Host;
            var port = _option.Port;

            var smtpClient = new SmtpClient(host, port);
            try
            {
                if (_option.EnableSsl)
                {
                    smtpClient.EnableSsl = true;
                }

                if (_option.UseDefaultCredentials)
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    smtpClient.UseDefaultCredentials = false;

                    var userName = _option.UserName;
                    if (!userName.IsNullOrEmpty())
                    {
                        var password = _option.Password;
                        var domain = _option.Domain;
                        smtpClient.Credentials = !domain.IsNullOrEmpty()
                            ? new NetworkCredential(userName, password, domain)
                            : new NetworkCredential(userName, password);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        protected async Task SendEmailAsync(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mail);
            }
        }

        protected void SendEmail(MailMessage mail)
        {
            using (SmtpClient smtpClient = this.BuildClient())
            {
                smtpClient.Send(mail);
            }
        }
    }
}

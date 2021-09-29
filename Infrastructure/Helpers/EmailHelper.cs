using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmailHelper
    {
        #region properties        
        private string _to { get; set; }
        private string _from { get; set; }
        private string _subject { get; set; }
        private string _plainTextMesage { get; set; }
        private string _htmlMessage { get; set; }
        private string _replyTo { get; set; }
        public IConfiguration Configuration { get; }

        #endregion

        /// <summary>
        /// Email send
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="plainTextMesage"></param>
        /// <param name="htmlMessage"></param>
        /// <param name="replyTo">Optional</param>
        public EmailHelper(IConfiguration configuration, string to, string from, string subject, string plainTextMesage, string htmlMessage, string replyTo = null)
        {
            _to = to;
            _from = from;
            _subject = subject;
            _plainTextMesage = plainTextMesage;
            _htmlMessage = htmlMessage;
            _replyTo = replyTo;
            Configuration = configuration;
        }

        public EmailHelper()
        {
        }

        public async Task<bool> SendEmailAsync()
        {
            try
            {
                EmailSender objEmailSender = new EmailSender(Configuration);
                bool result = await objEmailSender.SendEmailAsync(null, _to, _from, _subject, _plainTextMesage, _htmlMessage, _replyTo);
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error:" + ex.Message);
                return false;
            }
        }

    }

    public class SmtpOptions
    {
        public SmtpOptions()
        {

        }
        public string Server { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }
        public bool RequiresAuthentication { get; set; }
        public string PreferredEncoding { get; set; } = string.Empty;

    }

    public class EmailSender
    {
        SmtpOptions defaultOptions = null;
        public IConfiguration Configuration { get; }
        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
            defaultOptions = new SmtpOptions();
        }



        public async Task<bool> SendEmailAsync(SmtpOptions smtpOptions, string to, string from,
                                               string subject, string plainTextMessage, string htmlMessage, string replyTo = null)
        {
            bool hasPlainText = !string.IsNullOrWhiteSpace(plainTextMessage);
            bool hasHtml = !string.IsNullOrWhiteSpace(htmlMessage);
            var message = new MimeMessage();

            #region Default Configuration
            if (smtpOptions == null)
            {
                smtpOptions = defaultOptions;
            }
            #endregion

            #region Argument Exceptions
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("no To address provided");
            }
            if (string.IsNullOrWhiteSpace(from))
            {
                throw new ArgumentException("no from address provided");
            }
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("no subject provided");
            }
            if (string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("no message provided");
            }
            #endregion

            message.From.Add(new MailboxAddress("", from));
            if (!string.IsNullOrWhiteSpace(replyTo))
            {
                message.ReplyTo.Add(new MailboxAddress("", replyTo));
            }
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            if (hasPlainText)
            {
                bodyBuilder.TextBody = plainTextMessage;
            }
            if (hasHtml)
            {
                bodyBuilder.HtmlBody = htmlMessage;
            }

            message.Body = bodyBuilder.ToMessageBody();
            smtpOptions.Server = Configuration["SMTP:EmailServer"].ToString() == null ? string.Empty : Configuration["SMTP:EmailServer"].ToString();
            smtpOptions.Port = Convert.ToInt32(Configuration["SMTP:EmailPort"]);
            smtpOptions.User = Configuration["SMTP:EmailFrom"].ToString() == null ? string.Empty : Configuration["SMTP:EmailFrom"].ToString();
            smtpOptions.Password = Configuration["SMTP:EmailPassword"].ToString() == null ? string.Empty : Configuration["SMTP:EmailPassword"].ToString(); ;
            smtpOptions.UseSsl = Convert.ToBoolean(Configuration["SMTP:EmailUseSsl"]);
            smtpOptions.RequiresAuthentication = Convert.ToBoolean(Configuration["SMTP:EmailRequiresAuthentication"]);

            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(
                        smtpOptions.Server,
                        smtpOptions.Port,
                        SecureSocketOptions.StartTlsWhenAvailable).ConfigureAwait(true);

                    // XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (smtpOptions.RequiresAuthentication)
                    {
                        await client.AuthenticateAsync(smtpOptions.User, smtpOptions.Password)
                                    .ConfigureAwait(false);
                    }
                    await client.SendAsync(message).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error:" + ex.Message);
                throw;
            }
        }
    }
}

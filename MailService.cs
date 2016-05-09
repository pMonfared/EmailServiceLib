using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using EmailServiceLibrary.Enums;
using EmailServiceLibrary.Models;

namespace EmailServiceLibrary
{
    public class MailService : IMailService
    {
        public SendingMailResult Send(EmailDocument email, EmailConfiguration smtpServerInfo)
        {
            var mail = new System.Net.Mail.MailMessage
            {
                From = new System.Net.Mail.MailAddress(email.From, email.DisplayName),
            };

            //set the addresses 
            foreach (var a in email.To)
            {
                mail.To.Add(a);
            }


            //set the content 
            mail.Subject = email.Subject;
            mail.IsBodyHtml = true;
            mail.Body = email.Body;
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(HtmlToText.ConvertHtml(email.Body), System.Text.Encoding.UTF8, MediaTypeNames.Text.Plain));
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(email.Body, System.Text.Encoding.UTF8, MediaTypeNames.Text.Html));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //send the message 
            var smtp = new System.Net.Mail.SmtpClient(smtpServerInfo.SmtpServer);

            var credentials = new System.Net.NetworkCredential(smtpServerInfo.Username, smtpServerInfo.Password);
            smtp.Port = smtpServerInfo.Port;
            smtp.Credentials = credentials;

            try
            {
                smtp.Send(mail);
                return SendingMailResult.Successful;
            }
            catch (SmtpFailedRecipientException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                {
                    // Display message like 'Mail box is busy', 'Mailbox is unavailable' or 'Transaction is failed'
                    Thread.Sleep(5000);
                    smtp.Send(mail);
                    return SendingMailResult.Successful;
                }
                else
                {
                    foreach (var a in email.To)
                    {
                        throw new Exception("Smtp Failed Recipient Exception : Don't Send Mail From: " + mail.From + " To: " + a + " With Subject: " + mail.Subject);
                    }
                    return SendingMailResult.Faild;
                   
                }
            }
            catch 
            {
                foreach (var a in email.To)
                {
                    throw new Exception("Smtp Failed Other Exception : Don't Send Mail From: " + mail.From + " To: " + a + " With Subject: " + mail.Subject);
                }
                return SendingMailResult.Faild;
            }
        }

        public async Task<bool> SendAsync(EmailDocument email, EmailConfiguration smtpServerInfo)
        {
            var mail = new System.Net.Mail.MailMessage
            {
                From = new System.Net.Mail.MailAddress(email.From, email.DisplayName),
            };

            //set the addresses 
            foreach (var a in email.To)
            {
                mail.To.Add(a);
            }


            //set the content 
            mail.Subject = email.Subject;
            mail.IsBodyHtml = true;
            mail.Body = email.Body;
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(HtmlToText.ConvertHtml(email.Body), System.Text.Encoding.UTF8, MediaTypeNames.Text.Plain));
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(email.Body, System.Text.Encoding.UTF8, MediaTypeNames.Text.Html));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //send the message 
            var smtp = new System.Net.Mail.SmtpClient(smtpServerInfo.SmtpServer);

            var credentials = new System.Net.NetworkCredential(smtpServerInfo.Username, smtpServerInfo.Password);
            smtp.Port = smtpServerInfo.Port;
            smtp.Credentials = credentials;
            try
            {
                var result = await Task.FromResult(smtp.SendMailAsync(mail));
                return result.IsCompleted;
            }
            catch (SmtpFailedRecipientException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                {
                    // Display message like 'Mail box is busy', 'Mailbox is unavailable' or 'Transaction is failed'
                    Thread.Sleep(5000);
                    smtp.Send(mail);
                    return true;
                }
                else
                {
                    foreach (var a in email.To)
                    {
                        throw new Exception("Smtp Failed Recipient Exception : Don't Send Mail From: " + mail.From + " To: " + a + " With Subject: " + mail.Subject);
                    }
                    return false;

                }
            }
            catch
            {
                foreach (var a in email.To)
                {
                    throw new Exception("Smtp Failed Other Exception : Don't Send Mail From: " + mail.From + " To: " + a + " With Subject: " + mail.Subject);
                }
                return false;
            }

        }

        public async Task<SendingMailResult> SendAsyncWithReturn(EmailDocument email, EmailConfiguration smtpServerInfo)
        {
            var mail = new System.Net.Mail.MailMessage
            {
                From = new System.Net.Mail.MailAddress(email.From, email.DisplayName),
            };

            //set the addresses 
            foreach (var a in email.To)
            {
                mail.To.Add(a);
            }


            //set the content 
            mail.Subject = email.Subject;
            mail.IsBodyHtml = true;
            mail.Body = email.Body;
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(HtmlToText.ConvertHtml(email.Body), System.Text.Encoding.UTF8, MediaTypeNames.Text.Plain));
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(email.Body, System.Text.Encoding.UTF8, MediaTypeNames.Text.Html));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //send the message 
            var smtp = new System.Net.Mail.SmtpClient(smtpServerInfo.SmtpServer);

            var credentials = new System.Net.NetworkCredential(smtpServerInfo.Username, smtpServerInfo.Password);
            smtp.Port = smtpServerInfo.Port;
            smtp.Credentials = credentials;

            try
            {
                await smtp.SendMailAsync(mail);
                return SendingMailResult.Successful;
            }
            catch (SmtpFailedRecipientException ex)
            {
                var statusCode = ex.StatusCode;

                if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                {
                    // Display message like 'Mail box is busy', 'Mailbox is unavailable' or 'Transaction is failed'
                    Thread.Sleep(5000);
                    smtp.Send(mail);
                    return SendingMailResult.Successful;
                }
                else
                {
                    foreach (var a in email.To)
                    {
                        throw new Exception("Smtp Failed Recipient Exception : Don't Send Mail From: " + mail.From + " To: " + a + " With Subject: " + mail.Subject);
                    }
                    return SendingMailResult.Faild;

                }
            }
            catch
            {
                foreach (var a in email.To)
                {
                    throw new Exception("Smtp Failed Other Exception : Don't Send Mail From: " + mail.From + " To: " + a + " With Subject: " + mail.Subject);
                }
                return SendingMailResult.Faild;
            }
        }
    }
}

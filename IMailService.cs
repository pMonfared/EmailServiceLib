using System.Threading.Tasks;
using EmailServiceLibrary.Enums;
using EmailServiceLibrary.Models;

namespace EmailServiceLibrary
{
    public interface IMailService
    {
        SendingMailResult Send(EmailDocument email, EmailConfiguration smtpServerInfo);
        Task<bool> SendAsync(EmailDocument email, EmailConfiguration smtpServerInfo);
        Task<SendingMailResult> SendAsyncWithReturn(EmailDocument email, EmailConfiguration smtpServerInfo);
    }
}
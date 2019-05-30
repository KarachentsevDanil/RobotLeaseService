using RLS.BLL.Configurations;
using RLS.BLL.DTOs.Internal.Messages;
using RLS.BLL.Services.Contracts.Internal;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Internal.Messages
{
    public class EmailSendService : IMessageSendService<EmailMessage>
    {
        private readonly ISendGridClient _client;

        private readonly EmailSenderConfiguration _configuration;

        public EmailSendService(ISendGridClient client, EmailSenderConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task SendMessageAsync(EmailMessage message, CancellationToken ct = default)
        {
            EmailAddress from = new EmailAddress(_configuration.SendFromEmail);
            EmailAddress to = new EmailAddress(message.SendToEmail);

            SendGridMessage sendGridMessage =
                MailHelper.CreateSingleEmail(from, to, message.Subject, message.Message, message.Message);

            await _client.SendEmailAsync(sendGridMessage, ct);
        }
    }
}
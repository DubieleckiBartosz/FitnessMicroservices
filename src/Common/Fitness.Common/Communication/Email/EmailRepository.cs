namespace Fitness.Common.Communication.Email;

public class EmailRepository : IEmailRepository
{
    private readonly ILoggerManager<EmailRepository> _loggerManager;

    public EmailRepository(ILoggerManager<EmailRepository> loggerManager)
    {
        _loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
    }

    public async Task SendEmailAsync(EmailDetails email, EmailOptions emailOptions)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress(email.FromName ?? emailOptions.FromName, emailOptions.FromAddress));

        foreach (var recipient in email.Recipients)
        {
            mailMessage.To.Add(MailboxAddress.Parse(recipient));
            _loggerManager.LogInformation($"Recipient: {recipient}");
        }

        mailMessage.Subject = email.Subject;
        mailMessage.Body = new TextPart(TextFormat.Html) { Text = email.Body };

        try
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(emailOptions.Host, emailOptions.Port, true);
                await smtpClient.AuthenticateAsync(emailOptions.User, emailOptions.Password);
                await smtpClient.SendAsync(mailMessage);
                await smtpClient.DisconnectAsync(true);
            }

            _loggerManager.LogInformation($"Send email.");
        }
        catch (Exception ex)
        {
            _loggerManager.LogError($"From email service: {ex.Message}");
            throw new FitnessApplicationException("Email failed", "Email Exception",
                HttpStatusCode.InternalServerError);
        }
    }
}
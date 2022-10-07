namespace Fitness.Common.Communication.Email;

public interface IEmailRepository
{
    Task SendEmailAsync(EmailDetails email, EmailOptions emailOptions);
}
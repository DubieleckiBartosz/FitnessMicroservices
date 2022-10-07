using Fitness.Common.Communication.Email;
using Identity.Application.IdentityTemplates;

namespace Identity.Application.Services;

public class IdentityEmailService : IIdentityEmailService
{
    private const string service = "Security service";
    private readonly IEmailRepository _emailRepository;
    private readonly EmailOptions _options;

    public IdentityEmailService(IEmailRepository emailRepository, IOptions<EmailOptions> options)
    {
        _emailRepository = emailRepository ?? throw new ArgumentNullException(nameof(emailRepository));
        _options = options.Value;
    }

    public async Task SendEmailAfterCreateNewAccountAsync(string recipient, string code, string userName)
    {
        var dictData = TemplateCreator.TemplateRegisterAccount(userName, code);
        var template = Templates.GetConfirmAccountTemplate().GetTemplateReplaceData(dictData);

        await SendAsync(recipient, template, MessageSubjects.AccountConfirmation);
    }

    public async Task SendEmailResetPasswordAsync(string recipient, string resetToken, string origin)
    {
        var dictData = TemplateCreator.TemplateResetPassword(resetToken, origin);
        var template = Templates.GetResetPasswordTemplate().GetTemplateReplaceData(dictData);

        await SendAsync(recipient, template, MessageSubjects.ResetPassword);
    }

    private async Task SendAsync(string recipient, string template, string subject)
    {
        var mail = CreateEmailForSend(new List<string> {recipient}, service, template, subject);
        await _emailRepository.SendEmailAsync(mail, _options);
    }

    private EmailDetails CreateEmailForSend(List<string> recipients, string from, string body, string subjectMail)
    {
        return new EmailDetails
        {
            Body = body,
            Subject = subjectMail,
            Recipients = recipients,
            FromName = from
        };
    }
}
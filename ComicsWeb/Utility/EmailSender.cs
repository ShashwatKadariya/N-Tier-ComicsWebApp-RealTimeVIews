using Microsoft.AspNetCore.Identity.UI.Services;

namespace ComicsWeb.Utility;

public class EmailSender: IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // TODO: LOGIC TO SEND EMAIl
        return Task.CompletedTask;
    }
}
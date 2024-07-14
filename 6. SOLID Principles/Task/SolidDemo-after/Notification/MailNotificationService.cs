using SolidDemo_after.Interfaces;

namespace SolidDemo_after.Notification;

internal class MailNotificationService : IMailNotificationService
{
    public bool Send(string notificationContent)
    {
        //actual mail notification implementation.
        return true;
    }
}

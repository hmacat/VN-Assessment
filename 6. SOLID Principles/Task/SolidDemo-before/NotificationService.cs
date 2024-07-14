namespace SolidDemo_before;

//Let's say we are only interested in sending
// an email so we implemented the mail notification but other parts are not implemented.
internal class NotificationService : INotificationService
{
    public bool SendMailNotification(string notificationContent)
    {
        //actual mail notification implementation.
        return true;
    }

    public bool SendPushNotification(string notificationContent)
    {
        //signs of ISP validation
        throw new NotImplementedException();
    }

    public bool SendSmsNotification(string notificationContent)
    {
        //signs of ISP validation
        throw new NotImplementedException();
    }
}

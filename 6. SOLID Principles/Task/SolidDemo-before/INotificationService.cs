namespace SolidDemo_before;

public interface INotificationService
{
    bool SendMailNotification(string notificationContent);
    bool SendSmsNotification(string notificationContent);
    bool SendPushNotification(string notificationContent);
}

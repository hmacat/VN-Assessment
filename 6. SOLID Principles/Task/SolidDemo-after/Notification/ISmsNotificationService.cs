namespace SolidDemo_after.Interfaces;

// ISP is applied and INotificationService is seperated into three interfaces.
public interface ISmsNotificationService
{
    bool Send(string notificationContent);
}

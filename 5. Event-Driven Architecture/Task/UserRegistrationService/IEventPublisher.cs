namespace UserRegistrationService;

// Publisher interface
public interface IEventPublisher
{
    void PublishUserRegisteredEvent(User user);
}

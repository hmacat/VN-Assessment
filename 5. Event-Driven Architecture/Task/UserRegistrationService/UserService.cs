namespace UserRegistrationService;

// Service to handle user registration
public class UserService
{
    private readonly IEventPublisher _eventPublisher;

    public UserService(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public void RegisterUser()
    {
        var user = CreateUser();
        // actual registration implementation may store the user to the database.
        _eventPublisher.PublishUserRegisteredEvent(user);
    }

    private static User CreateUser()
    {
        var id = Guid.NewGuid();
        return new User
        {
            Id = id,
            Email = $"{id}@example.com"
        };
    }
}
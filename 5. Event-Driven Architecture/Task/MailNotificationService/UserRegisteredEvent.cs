﻿namespace MailNotificationService;

public class UserRegisteredEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
}

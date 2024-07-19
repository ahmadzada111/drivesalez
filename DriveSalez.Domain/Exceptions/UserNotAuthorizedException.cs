namespace DriveSalez.Domain.Exceptions;

public class UserNotAuthorizedException : Exception
{
    public UserNotAuthorizedException(string message) : base(message)
    {
        
    }
}
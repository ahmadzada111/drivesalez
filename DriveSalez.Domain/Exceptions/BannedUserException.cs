namespace DriveSalez.Domain.Exceptions;

public class BannedUserException : Exception
{
    public BannedUserException(string message) : base(message)
    {
        
    }
}
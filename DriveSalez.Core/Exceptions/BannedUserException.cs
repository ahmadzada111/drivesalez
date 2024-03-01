namespace DriveSalez.Core.Exceptions;

public class BannedUserException : Exception
{
    public BannedUserException(string message) : base(message)
    {
        
    }
}
namespace DriveSalez.Domain.Exceptions;

public class EmailNotConfirmedException : Exception
{
    public EmailNotConfirmedException(string message) : base(message)
    {
        
    }
}
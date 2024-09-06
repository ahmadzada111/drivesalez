namespace DriveSalez.SharedKernel.Utilities;

public record EmailMetadata
{
    public string ToAddress { get; init; }
    
    public string Subject { get; init; }
    
    public string? Body { get; init; }
    
    public string? AttachmentPath { get; init; }
    
    public bool IsHtml { get; init; }
    
    public EmailMetadata(string toAddress, string subject, string? body = "",
        string? attachmentPath = "", bool isHtml = false)
    {
        ToAddress = toAddress;
        Subject = subject;
        Body = body;
        AttachmentPath = attachmentPath;
        IsHtml = isHtml;
    }
}
using DriveSalez.Application.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Jobs;

public class StartImageAnalyzerJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger _logger;
    private readonly IComputerVisionService _computerVisionService;
    private readonly IEmailService _emailService;
    private readonly IFileService _fileService;
    
    public StartImageAnalyzerJob(ApplicationDbContext dbContext, ILogger<StartImageAnalyzerJob> logger,
        IComputerVisionService computerVisionService, IEmailService emailService, IFileService fileService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _computerVisionService = computerVisionService;
        _emailService = emailService;
        _fileService = fileService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"{typeof(StartImageAnalyzerJob)} job started");
        
        var expiredAnnouncements = await _dbContext.Announcements
            .Where(a => a.AnnouncementState == AnnouncementState.Pending)
            .Include(a => a.Owner)
            .Include(a => a.ImageUrls)
            .ToListAsync();

        foreach (var announcement in expiredAnnouncements)
        {
            var result = await _computerVisionService.AnalyzeImagesAsync(announcement.ImageUrls);

            if (!result)
            {
                string subject = "Important Notice Regarding Your Announcement";
                string body =
                    $"Dear {announcement.Owner.FirstName} {announcement.Owner.LastName}, We hope this message finds you well. We regret to inform you that one of your recent announcements on our platform has been removed as it was found to be in violation of our user agreements and policies." +
                    $"\n\nWe take the quality and appropriateness of content very seriously to ensure a safe and enjoyable experience for all our users. After a thorough review, it was determined that the content of your announcement did not adhere to our guidelines." +
                    $"\n\nIf you have any questions or concerns regarding this removal or if you would like more information about our policies, please don't hesitate to reach out to our support team. We are here to assist you and provide clarification on any issues." +
                    $"\n\nThank you for your understanding and cooperation." +
                    $"\n\nBest regards," +
                    $"\n\nDriveSalez Team";

                await _fileService.DeleteAllFilesAsync(announcement.Owner.Id);
                
                _dbContext.Remove(announcement);
                _dbContext.RemoveRange(announcement.ImageUrls);
                
                await _dbContext.SaveChangesAsync();
                await _emailService.SendEmailAsync(announcement.Owner.Email, subject, body);
            }
        }
        
        _logger.LogInformation($"{typeof(StartImageAnalyzerJob)} job finished");
    }
}
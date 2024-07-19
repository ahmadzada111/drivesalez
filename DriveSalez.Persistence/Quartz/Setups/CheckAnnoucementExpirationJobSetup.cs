using DriveSalez.Persistence.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Setups;

public class CheckAnnouncementExpirationJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var checkAnnouncementStateKey = new JobKey(nameof(CheckAnnouncementExpirationJob));
        options
            .AddJob<CheckAnnouncementExpirationJob>(builder => builder.WithIdentity(checkAnnouncementStateKey))
            .AddTrigger(trigger => trigger
                .ForJob(checkAnnouncementStateKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24).RepeatForever()));
    }
}
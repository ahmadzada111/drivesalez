using DriveSalez.Infrastructure.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace DriveSalez.Infrastructure.Quartz.Setups;

public class LookForExpiredPremiumAnnouncementJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var lookForExpiredPremiumAnnouncementsKey = new JobKey(nameof(LookForExpiredPremiumAnnouncementsJob));
        options
            .AddJob<LookForExpiredPremiumAnnouncementsJob>(builder => builder.WithIdentity(lookForExpiredPremiumAnnouncementsKey))
            .AddTrigger(trigger => trigger
                .ForJob(lookForExpiredPremiumAnnouncementsKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24).RepeatForever()));
    }
}
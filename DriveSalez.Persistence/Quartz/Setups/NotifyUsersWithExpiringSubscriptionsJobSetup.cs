using DriveSalez.Persistence.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Setups;

public class NotifyUsersWithExpiringSubscriptionsJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var notifyUsersWithExpiringSubscriptionsKey = new JobKey(nameof(NotifyUsersWithExpiringSubscriptionsJob));
        options
            .AddJob<NotifyUsersWithExpiringSubscriptionsJob>(builder => builder.WithIdentity(notifyUsersWithExpiringSubscriptionsKey))
            .AddTrigger(trigger => trigger
                .ForJob(notifyUsersWithExpiringSubscriptionsKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24).RepeatForever()));
    }
}
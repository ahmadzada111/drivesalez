using DriveSalez.Infrastructure.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace DriveSalez.Infrastructure.Quartz.Setups;

public class NotifyUserAboutSubscriptionCancellationJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var notifyUserAboutSubscriptionCancellationKey= new JobKey(nameof(NotifyUserAboutSubscriptionCancellationJob));
        options.AddJob<NotifyUserAboutSubscriptionCancellationJob>(builder => builder.WithIdentity(notifyUserAboutSubscriptionCancellationKey))
            .AddTrigger(trigger => trigger
                .ForJob(notifyUserAboutSubscriptionCancellationKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24).RepeatForever()));
    }
}
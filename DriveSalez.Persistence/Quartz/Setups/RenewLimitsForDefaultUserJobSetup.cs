using DriveSalez.Persistence.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Setups;

public class RenewLimitsForDefaultUserJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var renewLimitsForDefaultUserKey = new JobKey(nameof(RenewLimitsForDefaultUserJob));
        options.AddJob<RenewLimitsForDefaultUserJob>(builder => builder.WithIdentity(renewLimitsForDefaultUserKey))
            .AddTrigger(trigger => trigger
                .ForJob(renewLimitsForDefaultUserKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24).RepeatForever()));
    }
}
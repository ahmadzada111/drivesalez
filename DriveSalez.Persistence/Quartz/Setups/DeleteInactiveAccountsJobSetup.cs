using DriveSalez.Persistence.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Setups;

public class DeleteInactiveAccountsJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var deleteInactiveAccountsKey = new JobKey(nameof(DeleteInactiveAccountsJob));
        options
            .AddJob<DeleteInactiveAccountsJob>(builder => builder.WithIdentity(deleteInactiveAccountsKey))
            .AddTrigger(trigger => trigger
                .ForJob(deleteInactiveAccountsKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24).RepeatForever()));
    }
}
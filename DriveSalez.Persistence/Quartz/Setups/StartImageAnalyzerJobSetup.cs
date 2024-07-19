using DriveSalez.Persistence.Quartz.Jobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace DriveSalez.Persistence.Quartz.Setups;

public class StartImageAnalyzerJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var startImageAnalyzerKey = new JobKey(nameof(StartImageAnalyzerJob));
        options
            .AddJob<StartImageAnalyzerJob>(builder => builder.WithIdentity(startImageAnalyzerKey))
            .AddTrigger(trigger => trigger
                .ForJob(startImageAnalyzerKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(1).RepeatForever()));
    }
}
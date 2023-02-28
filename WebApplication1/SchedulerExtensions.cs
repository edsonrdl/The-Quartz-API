using Quartz;
using WebApplication1.Jobs;

namespace WebApplication1;

public static class SchedulerExtensions
{
    public static void AddQuartzService(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    }

    public static async Task AddQuartzSchedules(this WebApplication app)
    {
        var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
        var scheduler = await schedulerFactory.GetScheduler();

        await scheduler.AddQuartzSchedulerSimple();
        await scheduler.AddQuartzSchedulerCalendar();
        await scheduler.AddQuartzSchedulerDailyTime();
        await scheduler.AddQuartzSchedulerCron();
    }
    private static async Task AddQuartzSchedulerSimple(this IScheduler scheduler)
    {
        var job = JobBuilder.Create<SimpleJob>()
            .WithIdentity("simpleJob", "simpleGroup")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("simpleTrigger", "simpleGroup")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
    private static async Task AddQuartzSchedulerCalendar(this IScheduler scheduler)
    {
        var job = JobBuilder.Create<CalendarJob>()
            .WithIdentity("calendarJob", "calendarGroup")
            .Build();
        var trigger = TriggerBuilder.Create()
            .WithIdentity("calendarTrigger", "calendarGroup")
            .WithCalendarIntervalSchedule(x => x
                .WithIntervalInDays(3))
            .StartAt(new DateTimeOffset(new DateTime(2023, 02, 28, 13, 02, 00)))
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }

    private static async Task AddQuartzSchedulerDailyTime(this IScheduler scheduler)
    {
        var job = JobBuilder.Create<DailyJob>()
            .WithIdentity("dailyTimeJob", "dailyTimeGroup")
            .Build();
        var trigger = TriggerBuilder.Create()
            .WithDailyTimeIntervalSchedule(s =>
                s.OnEveryDay()
                    .StartingDailyAt(new TimeOfDay(13, 00))
                    .EndingDailyAt(new TimeOfDay(15, 00)))
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(30))
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }
    private static async Task AddQuartzSchedulerCron(this IScheduler scheduler)
    {
        var job = JobBuilder.Create<CronJob>()
            .WithIdentity("cronJob", "cronGroup")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("cronTrigger", "cronGroup")
            .WithCronSchedule("0 59 14 * * ?")
            .ForJob("cronJob", "cronGroup")
            .Build();

        await scheduler.ScheduleJob(job, trigger);
    }

}
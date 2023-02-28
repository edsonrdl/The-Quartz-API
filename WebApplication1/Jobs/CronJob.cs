using Quartz;
using Serilog;

namespace WebApplication1.Jobs;

public class CronJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"CronJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}
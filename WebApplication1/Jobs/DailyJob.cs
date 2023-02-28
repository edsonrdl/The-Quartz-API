using Quartz;
using Serilog;

namespace WebApplication1.Jobs;

public class DailyJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"DailyJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}
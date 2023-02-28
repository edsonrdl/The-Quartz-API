using Quartz;
using Serilog;

namespace WebApplication1.Jobs;

public class CalendarJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"CalendarJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}
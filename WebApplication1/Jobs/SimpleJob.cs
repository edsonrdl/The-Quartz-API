using Quartz;
using Serilog;

namespace WebApplication1.Jobs;

public class SimpleJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Log.Information($"SimpleJob: {DateTime.Now}");
        await Task.CompletedTask;
    }
}
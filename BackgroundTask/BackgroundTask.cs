﻿using Kitchen.Kitchen;

namespace Kitchen.BackgroundTask;

public class BackgroundTask : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Timer _timer;

    public BackgroundTask(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(5000);
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var scoped = scope.ServiceProvider.GetRequiredService<IKitchen>();
            scoped.RunKitchen(stoppingToken);
        }
    }
}
﻿using Kitchen.Kitchen;
using Kitchen.Repositories.CookRepository;
using Kitchen.Repositories.OrderListRepository;
using Kitchen.Services.CookService;
using Kitchen.Services.OrderService;

namespace Kitchen.Settings;
public class Startup
{
    private IConfiguration ConfigRoot { get; }

    public Startup(IConfiguration configuration)
    {
        ConfigRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddLogging(config => config.ClearProviders());

        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<ICookService, CookService>();
        
        services.AddSingleton<IOrderListRepository, OrderListRepository>();
        services.AddSingleton<ICookRepository, CookRepository>();
        
        services.AddSingleton<IKitchen, Kitchen.Kitchen>();
        services.AddHostedService<BackgroundTask.BackgroundTask>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
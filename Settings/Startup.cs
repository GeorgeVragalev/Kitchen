using Kitchen.Services;

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
        services.AddHostedService<BackgroundTask.BackgroundTask>();
        services.AddSingleton<IKitchenService, KitchenService>();
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
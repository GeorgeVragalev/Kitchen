using Kitchen.Kitchen;
using Kitchen.Repositories;
using Kitchen.Services;
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
        services.AddScoped<IKitchenService, KitchenService>();
        services.AddScoped<IOrderListRepository, OrderListRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IKitchen, Kitchen.Kitchen>();
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
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using StockData.Base;
using StockData.Base.DbContexts;
using StockData.Service;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var migrationAssemblyName = typeof(Worker).Assembly.FullName;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();


try
{
    Log.Information("Application Starting up");
    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .UseSerilog()
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new WorkerModule(connectionString, migrationAssemblyName));
            builder.RegisterModule(new BaseModule(connectionString, migrationAssemblyName));
        })
        .ConfigureServices((services) =>
        {
            services.AddHostedService<Worker>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<StockDataScrapingDbContext>(option =>
                option.UseSqlServer(connectionString, m => m.MigrationsAssembly(migrationAssemblyName)));

        })
        .Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up Failed!");
}
finally
{
    Log.CloseAndFlush();
}

//dotnet ef migrations add CreatingTables --project StockData.Worker --context StockDataScrapingDbContext
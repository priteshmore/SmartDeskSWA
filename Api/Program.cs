using Api.DB;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Add DbContext with SQL Server
        //var configuration = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        //    .AddEnvironmentVariables()
        //    .Build();

        //string connectionString = configuration["ConnectionStrings__DefaultConnection"];

        //if (string.IsNullOrEmpty(connectionString))
        //{
        //    throw new InvalidOperationException("Database connection string is missing.");
        //}

        //services.AddDbContext<SmartDeskDBContext>(options =>
        //    options.UseNpgsql(connectionString));
    })
    .Build();

host.Run();

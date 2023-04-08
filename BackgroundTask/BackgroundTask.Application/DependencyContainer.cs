using BackgroundTask.Application.Interfaces.IServices;
using BackgroundTask.Application.Services;
using BackgroundTask.Application.Services.Background;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Application;

public static class DependencyContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWriteFileService, WriteFileService>();

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<WriteFileTask>()
                .AddHostedService<WriteFileWithIntervalTask>()
                .AddHostedService<WriteFileWithServiceTask>();

        return services;
    }
}

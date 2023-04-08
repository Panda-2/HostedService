using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Application.Services.Background;

/// <summary>
/// Clase que escribira en un archivo de texto una unica vez por cada momento
/// </summary>
public class WriteFileTask : IHostedService
{
    private readonly IHostEnvironment _env;
    private readonly string _fileName;

    public WriteFileTask(IHostEnvironment env, IConfiguration configuration)
    {
        _env = env;
        _fileName = configuration["Filenames:FileName_1"];
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Writer("Start Process");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Writer("End Process");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Método que escribira dentro del archivo
    /// </summary>
    /// <param name="message">Texto a agregar</param>
    private void Writer(string message)
    {
        var path = $@"{_env.ContentRootPath}\wwwroot\{_fileName}";
        //append: Indica que no reemplazara el archivo, solo agregara contenido
        using (StreamWriter writer = new(path, append: true))
        {
            writer.WriteLine(message);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Application.Services.Background;

/// <summary>
/// Clase que escribira en un archivo cada 5 segundos de forma recurrente
/// </summary>
public class WriteFileWithIntervalTask : IHostedService, IDisposable
{
    private readonly IHostEnvironment _env;
    private readonly string _fileName;
    private readonly int _period;
    private Timer _timer;

    public WriteFileWithIntervalTask(IHostEnvironment env, IConfiguration configuration)
    {
        _env = env;
        _fileName = configuration["Filenames:FileName_2"];
        _period = int.Parse(configuration["BackgroundTaskExecutionPeriod:fileWrite_Seconds"]);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        //Timer: constructor con 4 parametros:
        //  callback => método que ejecutara y que obligatoriamente debe contener un parametro state de tipo object
        //  state => estado inicial
        //  dueTime => delay inicial
        //  period => periodo o intervalo en el que queremos que se ejecute nuestro método
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(_period));
        Writer("Start Process");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        //Eliminamos el timer, indicamos que comience en el tiempo -1 y un periodo de 0
        _timer?.Change(Timeout.Infinite, 0);
        Writer("End Process");
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        string now = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        Writer("Running in Processg: " + now);
    }

    private void Writer(string message)
    {
        var path = $@"{_env.ContentRootPath}\wwwroot\{_fileName}";
        using (StreamWriter writer = new(path, append: true))
        {
            writer.WriteLine(message);
        }
    }
}

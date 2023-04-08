using BackgroundTask.Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Application.Services.Background;

/// <summary>
/// Clase que escribira desde un servicio a las 2:30 am todos los días, es decur se ejecutara cada 24 hrs. en la hora que se desee
/// </summary>
public class WriteFileWithServiceTask : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly int _period;

    private Timer _timer;

    public WriteFileWithServiceTask(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _period = int.Parse(configuration["BackgroundTaskExecutionPeriod:FileWriteByService_Hours"]);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Action action = ConfigureTimer;
        Task.Run(action);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private void ConfigureTimer()
    {
        /*Calculamos el tiempo para ejecutar la primera vez y el tiempo de espera para configurar el temporizador*/

        //obtenemos el dia actual con 00hrs.
        DateTime today = DateTime.Today;

        //le agregamos un día y el tiempo para que cumpla con la hora que queremos que se ejecute, en mi caso le agrego 2 hrs. y 30 min
        DateTime nextRunTime = today.AddDays(1).AddHours(2).AddMinutes(30);

        //obtenemos la fecha y hora actual
        DateTime curTime = DateTime.Now;

        //Obtenemos el tiempo restante para la hora de ejecución
        TimeSpan firstInterval = nextRunTime.Subtract(curTime);

        //Configuramos el tiempo de espera para que se ejecute a la hora deseada
        Task task = Task.Delay(firstInterval);
        task.Wait();
        DoWork(null);

        //Seteamos el _timer para que se ejecute, y se repetira cada 24rs.
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(_period));
    }

    private async void DoWork(object state)
    {
        string now = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            //Obtenemos nuestra interfaz que contiene el método
            IWriteFileService iWriteService = scope.ServiceProvider.GetService<IWriteFileService>();

            //Ejecutamos el método que necesitamos
            await iWriteService.WriteFileAsync(now);
        }
    }
}

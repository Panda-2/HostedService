# HostedService
Ejecución de tareas en segundo plano

## HostedService
Es un servicio que nos permite ejecutar tareas recurrentes en nuestra aplicación, el cual se ejecuta al inicio y al final. Para implementar tenemos que heredar de la interfaz **IHostedService** e implementar 2 métodos:

- **StartAsync(CancellationToken cancellationToken):** Se ejecuta cuando se levanta la aplicación

- **StopAsync(CancellationToken cancellationToken):** Se ejecuta cuando el host de la aplicación esta realizando un cierre estable, hay situaciones excepcionales en las que este método no se ejecutara.

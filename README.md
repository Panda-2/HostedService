# HostedService
Ejecución de tareas en segundo plano

## HostedService
Es un servicio que nos permite ejecutar tareas recurrentes en nuestra aplicación, el cual se ejecuta al inicio y al final. Para implementar tenemos que heredar de la interfaz **IHostedService** e implementar 2 métodos:

- **StartAsync(CancellationToken cancellationToken):** Se ejecuta cuando se levanta la aplicación

- **StopAsync(CancellationToken cancellationToken):** Se ejecuta cuando el host de la aplicación esta realizando un cierre estable, hay situaciones excepcionales en las que este método no se ejecutara.


## Timer
Proporciona un mecanismo para ejecutar un método en un subproceso de grupo de subprocesos a intervalos específicos. Esta clase no puede heredarse. Para este caso usaremos su constructor de 4 parametros

- **callback:** método que debe tener como parametro un `'state'` de tipo `'object'`
- **state:** representa el estado inicial, puede ser null
- **dueTime:** cantidad de tiempo de retraso antes de que se ejecute el callback.
  - Con `'TimeOut.infinite'` evitamos que inicie el temporizador
  - Con `'TimeSpan.Zero'` iniciamos inmediatamente
- **period:** Intervalo de tiempo entre las invocaciones del callback

## Nota:


|                                                                              |
|------------------------------------------------------------------------------|
|Tomar en cuenta la intensidad de los recursos que se consumira por las tareas, si son muy pesadas mejor usar un servicio windows |
|Al tener el servicio en background y junto a la API, por temas del IIS si tu aplicación tiene un tiempo de inactividad de 20 min aprox se detendra el servicio|

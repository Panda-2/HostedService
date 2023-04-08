using BackgroundTask.Application.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Application.Services;

public class WriteFileService : IWriteFileService
{
    private readonly string _path;

    public WriteFileService(IHostEnvironment env, IConfiguration configuration)
    {
        string contentRootPath = env.ContentRootPath;
        string fileName = configuration["Filenames:FileName_3"];

        _path = $@"{contentRootPath}\wwwroot\{fileName}";
    }

    public Task WriteFileAsync(string message)
    {
        using (StreamWriter writer = new(_path, append: true))
        {
            lock (writer)
            {
                writer.WriteLine(message);
            }
        }

        return Task.CompletedTask;
    }
}

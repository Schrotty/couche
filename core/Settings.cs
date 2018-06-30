using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

public class Settings {

    public IConfiguration Configuration
    {
        get;
    }

    public string Username
    {
        get => Configuration["username"];
    }

    public string Password
    {
        get => Configuration["password"];
    }

    public string Uri
    {
        get => Configuration["uri"];
    }

    internal Settings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("settings.json");

        Configuration = builder.Build();
    }
}
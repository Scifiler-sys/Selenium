using Microsoft.Extensions.Configuration;

public class Configuration
{
    private readonly IConfigurationRoot _config;

    public Configuration()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Data/credentials.json")
            .Build();
    }

    public string GetUser()
    {
        return _config["username"];
    }

    public string GetPass()
    {
        return _config["password"];
    }

    public string GetTrainingUrl()
    {
        return _config["reportLink"];
    }
}
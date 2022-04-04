using Microsoft.Extensions.Configuration;

public class Configuration
{
    private readonly IConfigurationRoot _config;
    public Configuration()
    {
        _config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("./Data/appsettings.json")
                    .Build();
    }

    public string OrganizationName 
    { 
        get
        {
            return _config["OrgName"];
        } 
    }

    public string ContactEmail 
    { 
        get
        {
            return _config["contactEmail"];
        } 
    }
}
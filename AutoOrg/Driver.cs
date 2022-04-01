using OpenQA.Selenium.Chrome;

public class Driver
{
    private readonly ChromeDriver _driver;

    public Driver()
    {
        ChromeOptions options = new ChromeOptions();
        options.DebuggerAddress = "localhost:9222";

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    
}
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class Driver
{
    private readonly ChromeDriver _driver;

    public Driver()
    {
        var options = new ChromeOptions();
        options.DebuggerAddress = "localhost:9222";

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public void GetEmails()
    {
        _driver.Navigate().GoToUrl("https://rev2.lightning.force.com/lightning/r/Report/00O0P0000045vl3UAA/view?reportFilters=%5B%7B%22column%22%3A%22CUST_ID%22%2C%22value%22%3A%22a193g0000027uX5AAI%22%2C%22operator%22%3A%22equals%22%2C%22isContextFilter%22%3Atrue%7D%5D");
        //Setting up DOM elements
        var tableRows = _driver.FindElements(By.XPath(@"*[contains(@class,'data-grid-table-row')]"));

        Console.WriteLine(tableRows.Count);
        ////*[@id="full-data-grid-8-row0-col0"]/div/div/a
        // foreach (var item in tableRows)
        // {
        //     var 
        // }
    }
}
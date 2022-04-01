using System.Net.Mail;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class Driver
{
    private readonly ChromeDriver _driver;
    private List<string> _validEmails = new List<string>();

    public Driver()
    {
        var options = new ChromeOptions();
        options.DebuggerAddress = "localhost:9222";

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public void Login()
    {
        
    }

    public void GetEmails()
    {
        _driver.Navigate().GoToUrl("https://rev2.lightning.force.com/lightning/r/Report/00O0P0000045vl3UAA/view?reportFilters=%5B%7B%22column%22%3A%22CUST_ID%22%2C%22value%22%3A%22a193g0000027uX5AAI%22%2C%22operator%22%3A%22equals%22%2C%22isContextFilter%22%3Atrue%7D%5D");
        //Setting up DOM elements
        var iframe = _driver.FindElement(By.XPath(@"//*[@id='brandBand_2']/div/div/div/iframe"));

        //Switching to the iframe since Saleforce uses it
        _driver.SwitchTo().Frame(iframe);
        var title = _driver.FindElement(By.XPath(@"//*[@id='report-00O0P0000045vl3UAA']/div/div[1]/div[1]/div[1]/div[1]/div/div[2]/div/div/h1/span[2]"));
        var tableRows = _driver.FindElements(By.XPath(@"//*[@id='report-00O0P0000045vl3UAA']/div/div[1]/div[2]/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div/div/div[4]/div/div/div/div/div/div/div[1]/table/tbody/tr"));

        //Stores all valid emails into _validEmails List
        for (int row = 0; row < tableRows.Count-2; row++)
        {
            var item = tableRows[row].FindElement(By.XPath("//*[@id='full-data-grid-8-row"+ row +"-col2']"));

            if (CheckValidEmail(item.Text))
            {
                _validEmails.Add(item.Text);
            }
        }

        foreach (var item in _validEmails)
        {
            Console.WriteLine(item);
        }
    }

    /// <summary>
    /// Checks if the email is valid
    /// </summary>
    /// <param name="p_email">the email that is being verified</param>
    /// <returns>true if valid, false if not</returns>
    private bool CheckValidEmail(string p_email)
    {
            try
            {
                MailAddress email = new MailAddress(p_email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch(ArgumentException)
            {
                return false;
            }
    }
}
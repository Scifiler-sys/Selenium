using System.Net.Mail;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class Driver
{
    private readonly ChromeDriver _driver;
    private readonly Configuration _config;
    private List<string> _validEmails = new List<string>();

    public Driver()
    {
        _config = new Configuration();

        var options = new ChromeOptions();
        options.DebuggerAddress = "localhost:9222";

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
    }

    public void Login()
    {
        _driver.Navigate().GoToUrl("https://rev2.my.salesforce.com/");

        //Setting up DOM elements
        var loginInput = _driver.FindElement(By.XPath(@"//*[@id='username']"));
        var passInput = _driver.FindElement(By.XPath(@"//*[@id='password']"));
        var loginButton = _driver.FindElement(By.XPath(@"//*[@id='Login']"));

        loginInput.SendKeys(_config.GetUser());
        passInput.SendKeys(_config.GetPass());
        loginButton.Click();
    }

    public void GetEmails()
    {
        _driver.Navigate().GoToUrl(_config.GetTrainingUrl());
            
        //Setting up DOM elements
        var iframe = _driver.FindElement(By.XPath(@"//*[@id='brandBand_2']/div/div/div/iframe"));

        //Switching to the iframe since Saleforce uses it
        _driver.SwitchTo().Frame(iframe);
        var title = _driver.FindElement(By.XPath(@"//*[@id='report-00O0P0000045vl3UAA']/div/div[1]/div[1]/div[1]/div[1]/div/div[2]/div/div/h1/span[2]"));
        var tableRows = _driver.FindElements(By.XPath(@"//*[@id='report-00O0P0000045vl3UAA']/div/div[1]/div[2]/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div/div/div[4]/div/div/div/div/div/div/div[1]/table/tbody/tr"));

        Console.WriteLine(tableRows.Count);
        //Stores all valid emails into _validEmails List
        for (int row = 0; row < tableRows.Count-2; row++)
        {
            //item stores each row in this table
            IWebElement item;

            try
            {
                item = tableRows[row].FindElement(By.XPath("//*[@id='full-data-grid-8-row"+ row +"-col2']"));
            }
            //Saleforce reloads the elements again so the old reference of tableRows becomes deleted
            //Catch block will refind the tableRows element again in HTML followed by finding the email
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                tableRows = _driver.FindElements(By.XPath(@"//*[@id='report-00O0P0000045vl3UAA']/div/div[1]/div[2]/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div/div/div[4]/div/div/div/div/div/div/div[1]/table/tbody/tr"));
                item = tableRows[row].FindElement(By.XPath("//*[@id='full-data-grid-8-row"+ row +"-col2']"));
            }

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
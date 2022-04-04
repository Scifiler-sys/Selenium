using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

public class Driver
{
    private readonly ChromeDriver _driver;
    private readonly Configuration _config;

    public Driver()
    {
        _config = new Configuration();
        ChromeOptions options = new ChromeOptions();
        options.DebuggerAddress = "localhost:9222";

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public void CreateOrg()
    {
        _driver.Navigate().GoToUrl("https://github.com/account/organizations/new?coupon=&plan=team_free");

        //Setting DOM elements
        var orgInput = _driver.FindElement(By.XPath("//*[@id='organization_profile_name']"));
        var contactInput = _driver.FindElement(By.XPath("//*[@id='organization_billing_email']"));
        var businessRadioButton = _driver.FindElement(By.XPath("//*[@id='terms_of_service_type_corporate']"));
        var businessInput = _driver.FindElement(By.XPath("//*[@id='organization_company_name']"));
        var agreeRadioButton = _driver.FindElement(By.XPath("//*[@id='agreed_to_terms']"));
        var submitButton = _driver.FindElement(By.XPath("//*[@id='org-new-form']/button"));

        //Actions
        orgInput.SendKeys(_config.OrganizationName);
        contactInput.SendKeys(_config.ContactEmail);
        businessRadioButton.Click();
        businessInput.SendKeys("Revature");
        agreeRadioButton.Click();

        Console.WriteLine("Verify the robot confirmation\nPress Enter to Continue");
        Console.ReadLine();

        submitButton.Click();
    }

    public void InvitePeople()
    {
        //Setup
        Excel  excels = new Excel();
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        //Setting DOM elements
        var userInput = _driver.FindElement(By.XPath("//*[@id='search-member']"));
        var completeButton = _driver.FindElement(By.XPath(@"//*[@id='js-pjax-container']/div/div[2]/div/div/div[2]/div/button"));
        var firstChoice = "//*[@id='new-org-members-complete-results-option-0']";
        IWebElement userOption;

        foreach (var item in excels.GetUsers)
        {
            userInput.SendKeys(item);
            userOption = _driver.FindElement(By.XPath(firstChoice));
            
            try
            {
                while (userOption.Text.Trim() == "")
                {
                    userOption = _driver.FindElement(By.XPath(firstChoice));
                    Thread.Sleep(500);
                }
                
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                userOption = _driver.FindElement(By.XPath(firstChoice));
            }

            if (userOption.Text.Contains("isn’t a GitHub member"))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write($"{item} was not found");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("");

                for (int i = 0; i < item.Length; i++)
                {
                    userInput.SendKeys(Keys.Backspace);
                }
                Thread.Sleep(500);
            }
            else
            {
                userOption.Click();
            }
        }

        completeButton.Click();
    }
}
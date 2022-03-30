using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class Driver
{
    private readonly ChromeDriver _driver;
    private readonly IConfigurationRoot _config;

    public Driver()
    {
        _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        var options = new ChromeOptions();
        options.DebuggerAddress = "localhost:9222";

        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); 
    }

    public void FirstPage()
    {
        //Setting DOM Elements
        string radioButton = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[2]/div/div[2]/div/div[2]/div/label/input";
        string dropDownWeek = @"//*[@id='SelectId_0_placeholder']";
        string dropDownSelect =@"//*[@id='SelectId_0']/div[2]/div[" + (WeekCalculation()+2) + "]";
        string nextButton = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[3]/div[1]/button/div";

        _driver.Navigate().GoToUrl(@"https://forms.office.com/Pages/ResponsePage.aspx?id=iuJja_motUeqQJfiMSFRZM7dmlOzrS1IsTudUsyGqRJUMzVIMEk1SUtYWjVCVDJWMUgxQllBVFk3QyQlQCN0PWcu");

        _driver.FindElement(By.XPath(radioButton)).Click();
        _driver.FindElement(By.XPath(dropDownWeek)).Click();
        _driver.FindElement(By.XPath(dropDownSelect)).Click();
        _driver.FindElement(By.XPath(nextButton)).Click();
    }

    public void SecondPage()
    {
        //Setting DOM Elements
        string howManyAssocInput = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[2]/div/div[2]/div/div/input";
        string saleForceMatch = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[3]/div/div[2]/div/div/input";
        string howManyWarnInput = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[4]/div/div[2]/div/div/input";
        string howManyPromoteInput = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[5]/div/div[2]/div/div/input";
        string yesRadioButton = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[6]/div/div[2]/div/div[1]/div/label/input";
        string nextButton = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[3]/div[1]/button[2]/div";

        _driver.FindElement(By.XPath(howManyAssocInput))
            .SendKeys(_config["AssociateNumber"]);

        _driver.FindElement(By.XPath(saleForceMatch))
            .SendKeys(bool.Parse(_config["SaleForceMatch"]) ? "N/A" : CommaGenerator(_config.GetSection("SaleForceReasons").Get<string[]>()));
        
        _driver.FindElement(By.XPath(howManyWarnInput))
            .SendKeys(_config["Warning"]);

        _driver.FindElement(By.XPath(howManyPromoteInput))
            .SendKeys(_config["AssociateNumber"]);

        _driver.FindElement(By.XPath(yesRadioButton))
            .Click();

        _driver.FindElement(By.XPath(nextButton))
            .Click();
    }

    public void ThirdPage()
    {
        //Setting DOM Elements
        string generalNotesInput = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[2]/div/div[2]/div/div/textarea";
        string listOfInitiativeInput = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[3]/div/div[2]/div/div/textarea";
        string workLoadRadioButton = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[2]/div[4]/div/div[2]/div/div[3]/div/label/input";
        string nextButton = @"//*[@id='form-container']/div/div/div[1]/div/div[1]/div[2]/div[4]/div[1]/button[2]/div";

        _driver.FindElement(By.XPath(generalNotesInput))
            .SendKeys(_config["GeneralNote"]);

        //Logic to add commas when there is more
        string initiatives = CommaGenerator(_config.GetSection("Initiatives").Get<string[]>());
        
        _driver.FindElement(By.XPath(listOfInitiativeInput))
            .SendKeys(initiatives);
        
        _driver.FindElement(By.XPath(workLoadRadioButton))
            .Click();

        // _driver.FindElement(By.XPath(nextButton))
        //     .Click();
    }

    //Will format arrays into data1, data2, data3,..., dataN
    public string CommaGenerator(string[] p_array)
    {
        string result = "";
        int cnt = 1;
        foreach (var item in p_array)
        {
            result += item + (p_array.Length > cnt++ ? ", " :"");
        }

        return result;
    }
    public int WeekCalculation()
    {
        int[] nums = _config["DateStartedBatch"].Split("/").Select(Int32.Parse).ToArray();
        DateTime date = new DateTime(nums[0],nums[1],nums[2]);
        TimeSpan diffDate = DateTime.Now - date;
        return (int)Math.Ceiling(diffDate.Days/7.0);
    }
}
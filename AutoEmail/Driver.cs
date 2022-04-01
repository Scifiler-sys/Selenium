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
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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

        var tableRows = _driver.FindElements(By.XPath(@"//*[@id='report-00O0P0000045vl3UAA']/div/div[1]/div[2]/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div/div/div[4]/div/div/div/div/div/div/div[2]/table/tbody/tr"));

        while (tableRows.Count < 5)
        {
            tableRows = _driver.FindElements(By.XPath(@"//*[@id='report-00O0P0000045vl3UAA']/div/div[1]/div[2]/div/div/div/div/div/div/div[1]/div/div/div[1]/div/div/div/div[4]/div/div/div/div/div/div/div[1]/table/tbody/tr"));
        }
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

        _driver.SwitchTo().DefaultContent();

        foreach (var item in _validEmails)
        {
            Console.WriteLine(item);
        }
    }

    public void SendEmails()
    {
        //Navigate to message
        _driver.Navigate().GoToUrl("https://outlook.office365.com/mail/");

        _driver.FindElement(By.XPath("//*[contains(text(), 'New message')]"))
            .Click();

        //Setting Dom Elements
        var toInput = _driver.FindElement(By.XPath(@"//*[@id='sentinel']"));
        var subjectInput = _driver.FindElement(By.XPath(@"//*[@aria-label='Add a subject']"));
        var bodyInput = _driver.FindElement(By.XPath("//*[@aria-label='Message body']"));

        //Typing all the emails
        foreach (var item in _validEmails)
        {
            Console.WriteLine(item);
            toInput.SendKeys(item+";");
        }
        
        //Typing the rest of the email
        subjectInput.SendKeys("Welcome To Revature!");

        

        bodyInput.SendKeys(@"
Good Day! 


Welcome to Revature! I am excited to welcome you to the upcoming batch.  


My name is Stephen Pagdilao, a Revature trainer based in Reston office. As the batch will be starting soon, I’ve issued this email to help you prepare.  
 

First, your machine should be able to handle the multiple software you’ll be working with during the training. Please check that your machine meets the following specifications: 

");

boldWord(bodyInput,"CPU");

bodyInput.SendKeys(@": intel core i3 and above 

");

boldWord(bodyInput,"RAM");  

bodyInput.SendKeys(@": 8Gb or more 

"); 

boldWord(bodyInput,"Memory");

bodyInput.SendKeys(@": 32Gb free minimum 

");

boldWord(bodyInput,"Operating system");

bodyInput.SendKeys(@": Windows 7 or higher  

");
boldWord(bodyInput,"Other requirements");

bodyInput.SendKeys(@": Your machine should have a web camera as it is company policy to keep it on during work hours.  

");

boldWord(bodyInput,"Optional");

bodyInput.SendKeys(@": Extra screen, as the batch will be conducted virtually, it would be helpful for you to have a monitor to attend the training and an extra one to follow along with the demos.  

You might also find it helpful to clear up some memory in preparation for the projects you’ll be working on during training.  

Second, your machine should be equipped with the software you’ll be using during training. Please have the following software installed in the machine you’ll be using for training (the links on where to download them have been provided as well): 

Git - https://git-scm.com/downloads

VS Code - https://code.visualstudio.com/Download

.NET 6 SDK (Choose the .NET 6 SDK) - https://dotnet.microsoft.com/en-us/download


When choosing which version to download, pick the latest, 64 bit, and Windows version.  


Third, you should also have a GitHub account prior to the beginning of the batch. GitHub is an online repository that we’ll be using throughout training. It’s where you’ll be uploading your projects as well as where you’ll find the demos I’ll be presenting in class.  


Finally, please answer the following survey to help me understand your level of familiarity with the material we’ll be discussing and to get to know you. Please answer the questions as honestly as you can as this will help me pace the training. Make sure to finish the survey before the 19th of January.  
survey link - https://forms.office.com/Pages/ResponsePage.aspx?id=iuJja_motUeqQJfiMSFRZAGM2VH0wiNBlu-QQ2OgLGdUMEswUDlVOVVTQUcxRUVOOFI2MlJaRlNYWi4u

Feel free to email me back if you have any questions. 


I look forward to guiding you on this journey over the coming weeks as you grow in your skills and knowledge as a software engineer. I’m excited to meet you!


From,
Stephen Pagdilao
Revature LLC | Sr. Trainer
E stephen.pagdilao@revature.com");
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

    private void boldWord(IWebElement p_input,string p_string)
    {
        p_input.SendKeys(Keys.LeftControl + "b");

        p_input.SendKeys(p_string);

        p_input.SendKeys(Keys.LeftControl + "b");
    }
}
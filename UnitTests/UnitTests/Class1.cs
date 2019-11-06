using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unitTest
{
    [TestFixture]
    public class Class1
    {
        private IWebDriver driver;
        private string baseurl;

        [SetUp]
        public void SetupTest()
        {
            var op = new FirefoxOptions
            {
                AcceptInsecureCertificates = true
            };

            driver = new FirefoxDriver(op);

            baseurl = "https://localhost:5001";



        }

        [TearDown]
        public void TearDownTest()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }

        //Member Loging Success
        [Test]
        public void LogInTest()
        {
            driver.Navigate().GoToUrl(baseurl + "/Access/Login");

            driver.FindElement(By.Id("Email")).SendKeys("test1@test.com");
            driver.FindElement(By.Id("Password")).SendKeys("w");
            driver.FindElement(By.Id("submit")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains("Home"));
        }
        //Member Loging Fail (Bad Password)
        [Test]
        public void LogInTestFail()
        {
            driver.Navigate().GoToUrl(baseurl + "/Access/Login");

            driver.FindElement(By.Id("Email")).SendKeys("test1@test.com");
            driver.FindElement(By.Id("Password")).SendKeys("butt");
            driver.FindElement(By.Id("submit")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains("Log"));
        }

        //Member Join Success
        [Test]
        public void JoinTest()
        {
            driver.Navigate().GoToUrl(baseurl + "/Users/Create");

            driver.FindElement(By.Id("Email")).SendKeys("random@test.com");
            driver.FindElement(By.Id("Password")).SendKeys("Qwerty123");
            driver.FindElement(By.Id("DisplayName")).SendKeys("random");
            driver.FindElement(By.Id("utAdmin")).Click();
            driver.FindElement(By.Id("genderM")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("First");
            driver.FindElement(By.Id("LastName")).SendKeys("Last");
            driver.FindElement(By.Id("submit")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains("Home"));
        }
        //Member Join Fail (No Email)
        [Test]
        public void JoinTestFail()
        {
            driver.Navigate().GoToUrl(baseurl + "/Users/Create");

            driver.FindElement(By.Id("Email")).SendKeys("");
            driver.FindElement(By.Id("Password")).SendKeys("Qwerty123");
            driver.FindElement(By.Id("DisplayName")).SendKeys("random");
            driver.FindElement(By.Id("utAdmin")).Click();
            driver.FindElement(By.Id("genderM")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("First");
            driver.FindElement(By.Id("LastName")).SendKeys("Last");
            driver.FindElement(By.Id("submit")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains("Add"));
        }

        //Member Account Edit Success
        [Test]
        public void AccountEdit()
        {
            driver.Navigate().GoToUrl(baseurl + "/Users/Edit/10");

            //driver.FindElement(By.Id("Email")).SendKeys("random@test.com");
            driver.FindElement(By.Id("DisplayName")).SendKeys("switched");
            /*driver.FindElement(By.Id("utAdmin")).Click();
            driver.FindElement(By.Id("genderM")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("First");
            driver.FindElement(By.Id("LastName")).SendKeys("LastName");*/
            driver.FindElement(By.Id("submit")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains("User"));
        }
        //Member Account Edit Fail (No Display Name)
        [Test]
        public void AccountEditFail()
        {
            driver.Navigate().GoToUrl(baseurl + "/Users/Edit/10");

            driver.FindElement(By.Id("Email")).SendKeys("random@test.com");
            driver.FindElement(By.Id("DisplayName")).SendKeys("");
            driver.FindElement(By.Id("utAdmin")).Click();
            driver.FindElement(By.Id("genderM")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("First");
            driver.FindElement(By.Id("LastName")).SendKeys("Last");
            driver.FindElement(By.Id("submit")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.Contains("Modify"));
        }

        //Member Profile Delete Success
        [Test]
        public void DeleteProfile()
        {
            driver.Navigate().GoToUrl(baseurl + "/Users/Delete/10");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.FindElement(By.Id("submit")).Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();



            wait.Until(d => d.Title.Contains("List"));
        }

        //Member Profile Delete Fail (Profile doesn't exist)
        [Test]
        public void DeleteProfileFail()
        {
            driver.Navigate().GoToUrl(baseurl + "/Users");

            try
            {
                driver.FindElement(By.XPath("/html/body/div/table/tbody/tr[80]/td[23]/a[3]")).Click();
            }
            catch (Exception e)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.Contains("List"));
            }
        }


    }
}

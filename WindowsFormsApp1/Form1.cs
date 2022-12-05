using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace test1
{
    public partial class Form1 : Form
    {
        ChromeDriver driver;
        bool Changed = false;
        List<string> New_Paticle = new List<string>();
        List<string> Changed_data = new List<string>();
        string Last_Paticle = string.Empty;
        Timer timer = new Timer();

        ChromeDriver driver2;
        bool Changed2 = false;
        List<string> New_Paticle2 = new List<string>();
        List<string> Changed_data2 = new List<string>();
        string Last_Paticle2 = string.Empty;
        Timer timer2 = new Timer();

        string url = "https://www.dongguk.edu/article/HAKSANOTICE/list#none";
        string url2 = "https://dgucoop.dongguk.edu/store/store.php?w=4&l=2";
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Crawler_Load(object sender, EventArgs e)
        {
            Getdriver();
            var headline = GetNewsData();
            addtitle(headline);
            Crawler_refreshing();
        }

        private void Crawler_refreshing()
        {
            timer.Interval = 7000; //주기 설정
            timer.Tick += new EventHandler(titmer_tick); //주기마다 실행되는 이벤트 등록
            timer.Start();
        }

        private void titmer_tick(object sender, EventArgs e)
        {
            var headline = refresh_News();
            if (Changed == true) // 변경된 데이터가 있다면
            {
                addtitle(headline); // 데이터 추가
                Changed = false;
            }
            else
            {
                return;
            }
        }

        private void Getdriver()
        {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            options.AddArgument("--headless");

            driver = new ChromeDriver(driverService, options);
            driver.Url = url;
        }

        private List<string> GetNewsData()
        {
            string Title = string.Empty;

            var headline = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[1]/a/div[2]/p"));
            var headline2 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[2]/a/div[2]/p"));
            var headline3 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[3]/a/div[2]/p"));
            var headline4 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[4]/a/div[2]/p"));
            var headline5 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[5]/a/div[2]/p"));
            var headline6 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[6]/a/div[2]/p"));
            //*[@id="content_focus"]/div/div[3]/div[2]/ul/li[1]/a/div[2]/p/text()
            //*[@id="content_focus"]/div/div[3]/div[2]/ul/li[1]/a/div[2]/p
            Title = headline.Text;
            New_Paticle.Add(Title);
            Title = headline2.Text;
            New_Paticle.Add(Title);
            Title = headline3.Text;
            New_Paticle.Add(Title);
            Title = headline4.Text;
            New_Paticle.Add(Title);
            Title = headline5.Text;
            New_Paticle.Add(Title);
            Title = headline6.Text;
            New_Paticle.Add(Title);

            Last_Paticle = New_Paticle[0];
            New_Paticle.Reverse();

            return New_Paticle;
        }

        private List<string> refresh_News()
        {
            string Title = string.Empty;
            driver.Navigate().Refresh();
            New_Paticle.Clear();

            var headline = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[1]/a/div[2]/p"));
            var headline2 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[2]/a/div[2]/p"));
            var headline3 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[3]/a/div[2]/p"));
            var headline4 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[4]/a/div[2]/p"));
            var headline5 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[5]/a/div[2]/p"));
            var headline6 = driver.FindElement(By.XPath("//*[@id='content_focus']/div/div[3]/div[2]/ul/li[6]/a/div[2]/p"));
            Title = headline.Text;
            New_Paticle.Add(Title);
            Title = headline2.Text;
            New_Paticle.Add(Title);
            Title = headline3.Text;
            New_Paticle.Add(Title);
            Title = headline4.Text;
            New_Paticle.Add(Title);
            Title = headline5.Text;
            New_Paticle.Add(Title);
            Title = headline6.Text;
            New_Paticle.Add(Title);

            if (New_Paticle.IndexOf(Last_Paticle) != 0 && New_Paticle.IndexOf(Last_Paticle) != -1) // 데이터가 존재하고 변동이 있다면
            {
                Changed = true;
                Changed_data.Clear();
                int Last_Paticle_li_Number = New_Paticle.IndexOf(Last_Paticle);

                for (int i = Last_Paticle_li_Number - 1; i >= 0; i--)
                {
                    Changed_data.Add(New_Paticle[i]);
                }
                Last_Paticle = Changed_data[Changed_data.Count - 1]; // 최신 기사로 바꾼다.

                return Changed_data;
            }

            else if (New_Paticle.IndexOf(Last_Paticle) == 0) // 데이터가 변동이 없다면
            {
                Changed = false;
            }

            return New_Paticle;
        }

        public void addtitle(List<string> Paticle)
        {
            StreamWriter sw = File.CreateText("../../../Crawling1.txt");
            for (int i = Paticle.Count - 1; i >= 0; i--)
            {
                //Write a line of text
                sw.WriteLine(Paticle[i]);
            }
            sw.Close();
        }

        private void Crawler_Load2(object sender, EventArgs e)
        {
            Getdriver2();
            var headline = GetNewsData2();
            addtitle2(headline);
            Crawler_refreshing2();
        }

        private void Crawler_refreshing2()
        {
            timer2.Interval = 7000; //주기 설정
            timer2.Tick += new EventHandler(titmer_tick2); //주기마다 실행되는 이벤트 등록
            timer2.Start();
        }

        private void titmer_tick2(object sender, EventArgs e)
        {
            var headline = refresh_News2();
            if (Changed2 == true) // 변경된 데이터가 있다면
            {
                addtitle2(headline); // 데이터 추가
                Changed2 = false;
            }
            else
            {
                return;
            }
        }

        private void Getdriver2()
        {
            ChromeOptions options = new ChromeOptions();
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            options.AddArgument("--headless");

            driver2 = new ChromeDriver(driverService, options);
            driver2.Url = url2;
        }

        private List<string> GetNewsData2()
        {
            string Title = string.Empty;

            var headline = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[4]/span[1]"));
            var headline2 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[5]/span[1]"));
            var headline3 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[6]/span[1]"));
            var headline4 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[7]/span[1]"));
            var headline5 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[8]/span[1]"));
            //*[@id="content_focus"]/div/div[3]/div[2]/ul/li[1]/a/div[2]/p/text()
            //*[@id="content_focus"]/div/div[3]/div[2]/ul/li[1]/a/div[2]/p
            Title = headline.Text;
            New_Paticle2.Add(Title);
            Title = headline2.Text;
            New_Paticle2.Add(Title);
            Title = headline3.Text;
            New_Paticle2.Add(Title);
            Title = headline4.Text;
            New_Paticle2.Add(Title);
            Title = headline5.Text;
            New_Paticle2.Add(Title);

            Last_Paticle2 = New_Paticle2[0];
            New_Paticle2.Reverse();

            return New_Paticle2;
        }

        private List<string> refresh_News2()
        {
            string Title = string.Empty;
            driver2.Navigate().Refresh();
            New_Paticle2.Clear();

            var headline = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[4]/span[1]"));
            var headline2 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[5]/span[1]"));
            var headline3 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[6]/span[1]"));
            var headline4 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[7]/span[1]"));
            var headline5 = driver2.FindElement(By.XPath("//*[@id='sdetail']/table[2]/tbody/tr[3]/td[8]/span[1]"));
            Title = headline.Text;
            New_Paticle2.Add(Title);
            Title = headline2.Text;
            New_Paticle2.Add(Title);
            Title = headline3.Text;
            New_Paticle2.Add(Title);
            Title = headline4.Text;
            New_Paticle2.Add(Title);
            Title = headline5.Text;
            New_Paticle2.Add(Title);

            if (New_Paticle2.IndexOf(Last_Paticle2) != 0 && New_Paticle2.IndexOf(Last_Paticle2) != -1) // 데이터가 존재하고 변동이 있다면
            {
                Changed2 = true;
                Changed_data2.Clear();
                int Last_Paticle_li_Number = New_Paticle2.IndexOf(Last_Paticle2);

                for (int i = Last_Paticle_li_Number - 1; i >= 0; i--)
                {
                    Changed_data2.Add(New_Paticle2[i]);
                }
                Last_Paticle2 = Changed_data2[Changed_data2.Count - 1]; // 최신 기사로 바꾼다.

                return Changed_data2;
            }

            else if (New_Paticle2.IndexOf(Last_Paticle2) == 0) // 데이터가 변동이 없다면
            {
                Changed2 = false;
            }

            return New_Paticle2;
        }

        public void addtitle2(List<string> Paticle)
        {
            StreamWriter sw = File.CreateText("../../../Crawling2.txt");
            for (int i = Paticle.Count-1; i >= 0; i--)
            {
                //Write a line of text
                sw.WriteLine(Paticle[i]);
                sw.WriteLine("");
            }
            sw.Close();
        }
    }
}
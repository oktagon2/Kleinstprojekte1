using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NasdaqScanner1
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument document = new XmlDocument();
            document.Load("https://www.yourmoney.ch/ym/details/985336%2C1135%2C333#Tab2");
            string allText = document.InnerText;

            //Create the WebBrowser control
            WebBrowser wb = new WebBrowser();
            //Add a new event to process document when download is completed   
            wb.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(DisplayText);
            //Download the webpage
            wb.Url = urlPath;
        }
    }
}

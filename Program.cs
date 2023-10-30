using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Sharp_Examination
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dict test = new Dict();
            test.menu();
            //string path = "testEngToRus.xml";
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(path);
            //XmlElement root = doc.DocumentElement;
            //foreach (XmlElement el in root.GetElementsByTagName("Word"))
            //{
            //    Console.WriteLine(el.InnerText);
            //}
        }
    }
}

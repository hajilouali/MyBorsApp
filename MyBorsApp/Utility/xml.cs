using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;



namespace MyBorsApp.Tools
{
    public static class xml
    {
        public static string loadline(string Route)
        {
            XmlDocument xmlDocument=new XmlDocument();
            xmlDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("/App_Data/PageContent/Content.xml"));
            string result = xmlDocument.SelectSingleNode("/Root/"+Route).InnerText;
            return result;
        }

        public static bool SavetoXml(string Route, string text)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath("/App_Data/PageContent/Content.xml"));
                xmlDocument.SelectSingleNode("/Root/" + Route).InnerText = text;
                xmlDocument.Save(System.Web.Hosting.HostingEnvironment.MapPath("/App_Data/PageContent/Content.xml"));
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
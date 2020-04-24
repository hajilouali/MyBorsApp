using System.Web.Mvc;

namespace MyBorsApp.Areas.MainPanel
{
    public class MainPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MainPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MainPanel_default",
                "MainPanel/{controller}/{action}/{id}",
                new {Controller="Home", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}
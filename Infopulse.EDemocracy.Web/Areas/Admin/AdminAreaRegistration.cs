using System.Data;
using System.Web.Mvc;

namespace Infopulse.EDemocracy.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
				constraints: new { controller = "Home|Log"},
				namespaces: new[] { "Infopulse.EDemocracy.Web.Areas.Admin.Controllers" }
            );
        }
    }
}
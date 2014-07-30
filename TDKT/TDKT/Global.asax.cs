using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TDKT.Models;
using TDKT.Controllers;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace TDKT
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                Session["date"] = String.Format("{0:dd/MM/yyyy}", DateTime.Today.Date);

                TDKTEntities td = new TDKTEntities();

                if (Session["year"] == null) HttpContext.Current.Session.Add("year", td.getYear().FirstOrDefault().ToString());

                if (User.IsInRole("Theo dõi đơn vị"))
                    Session["donvi"] = td.getMaDonvi(User.Identity.GetUserId().ToString()).FirstOrDefault().ToString();
                else Session["donvi"] = null;

            }

        }
    }
}

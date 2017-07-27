using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoppingcart_tiu_project_2017.Areas.Admin.Controllers
{
    public class dashboardController : Controller
    {
        // GET: Admin/dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}
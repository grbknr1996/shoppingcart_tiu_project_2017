

using shoppingcart_tiu_project_2017.Models.Data;
using shoppingcart_tiu_project_2017.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoppingcart_tiu_project_2017.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {   //DECLARE A LIST OF PAGEVM
            List<PageVM> pagesList;

            using (Db db = new Db())
            {
                //init the list
                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }
            //RETURN VIEW WITH LIST
            return View(pagesList);
        }
    }
}
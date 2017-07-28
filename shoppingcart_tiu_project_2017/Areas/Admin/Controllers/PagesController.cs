

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
        //GET:Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()

        {
            return View();
        }
        //POST:Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
           
            //check model state
            if(! ModelState.IsValid)
            {
                return View(model);
            }
            using (Db db = new Db())
            {


                //declare the slug
                string slug;
                //init the pageDTO
                PageDTO dto = new PageDTO();
                //pageDTO title
                dto.Title = model.Title;
                //check for and set slug if need be
                if(string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }
                //make sure the title and slug are unique
                if(db.Pages.Any( x => x.Title == model.Title)||db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("","title or slug already exists");
                    return View(model);
                }
                // DTO the rest 
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100;
                //save DTO
                db.Pages.Add(dto);
                db.SaveChanges();

            }
            //set TempData message
            TempData["SM"] = "You have added a new page here!";
            //redirect
            return RedirectToAction("AddPage");
            
        }
        //GET:Admin/Pages/EditPage/id
        [HttpGet]

        public ActionResult EditPage(int id)
        {
            //declare the pagevm
            PageVM model;
            using (Db db = new Db())
            {
                //get the page
                PageDTO dto = db.Pages.Find(id);
                //confirm page exists
                if(dto==null)
                {
                    return Content("The Page doesnot exist");
                }
                //init the pagevm
                model = new PageVM(dto);
            }
                //return view with model
                return View(model);
        }
        //POST:Admin/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            //check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (Db db = new Db())
            {
                //get page id
                int id = model.Id;
                //init slug
                string slug="home";
                //get the page
                PageDTO dto = db.Pages.Find(id);
                //DTO title
                dto.Title = model.Title;
                //check for the slug and set it if need be
                if(model.Slug !="home")
                {
                    if(string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ","-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }

                //make sure title and slug are unique
                if (db.Pages.Where(x => x.Id != id).Any(x => x.Title == model.Title)||
                    db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "title or slug already exists");
                    return View(model);
                }
                //Dto the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;

                //save the dto
                db.SaveChanges();
            }
            //set tempdata message
            TempData["SM"] = "you have edited the page!";
                //redirect

                return RedirectToAction("EditPage");
        }
        //GET:Admin/Pages/PageDetails/id
        public ActionResult PageDetails(int id)
        {
            //declare the pageVM
            PageVM model;
            using (Db db = new Db())
            {
                //get the page
                PageDTO dto= db.Pages.Find(id);
                //confirm the page exists
                if(dto==null)
                {
                    return Content("The page doesnt exist!");
                }


                //init the pageVM

                model = new PageVM(dto);
            }
                //return view with model
                return View(model);
        }
        //GET:Admin/Pages/DeletePage/id
        public ActionResult DeletePage(int id)
        {
            
            //get the page
            using (Db db = new Db())
            {
                PageDTO dto = db.Pages.Find(id);
                //remove the page
               
                db.Pages.Remove(dto);
                //save
                db.SaveChanges();
            }
            //redirect
            return RedirectToAction("Index");
        }
    }
}
using shoppingcart_tiu_project_2017.Models.Data;
using shoppingcart_tiu_project_2017.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoppingcart_tiu_project_2017.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop/Categories
        public ActionResult Categories()
        {
            //declare a list of models
            List<CategoryVM> categoryVMList;
            using (Db db = new Db())
            {
                //init the models
                categoryVMList = db.Categories.ToArray().OrderBy(x => x.Sorting).Select(x => new CategoryVM(x)).ToList();
            }

                //return view with models

                return View(categoryVMList);
        }
        // POST: Admin/Shop/AddNewCategories
        [HttpPost]
        public string AddNewCategory(string catName)
        {
            //declare id
            string id;
            using (Db db = new Db())
            {
                //check that the category name is unique
                if (db.Categories.Any(x => x.Name == catName)) return "title taken";
                //init dto
                CategoryDTO dto = new CategoryDTO();
                //add to DTO
                dto.Name = catName;
                dto.Slug = catName.Replace(" ", "-").ToLower();
                dto.Sorting = 100;
                //save the dto
                db.Categories.Add(dto);
                db.SaveChanges();

                //get the id
                id = dto.Id.ToString();
            }
            //return the id
            return id;
                
        }
    }
}
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using Kendo.Mvc.Extensions;
using WebApp.Entities;

namespace WebApp.Controllers
{
    public class CategoriesController : BaseController
    {
        // GET: /Categories/
        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
            return View();
        }

        public ActionResult GetCategories([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<CategoryViewModel> categories = uow.Categories.All().Select(category =>
                new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name
                });

            return Json(categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveCategory([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            if (category != null)
            {
                var categoryToDelete = uow.Categories.GetById(category.Id);
                foreach (var ticketToDelete in categoryToDelete.Tickets)
                {
                    foreach (var commentToDelete in ticketToDelete.Comments)
                    {
                        uow.Comments.Delete(commentToDelete);
                        uow.SaveChanges();
                    }

                    uow.Tickets.Delete(ticketToDelete);
                    uow.SaveChanges();
                }

                uow.Categories.Delete(categoryToDelete);
                uow.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCategory([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var categoryToAdd = new Category()
                {
                    Name = category.Name
                };

                uow.Categories.Add(categoryToAdd);
                uow.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditCategory([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            if (category != null && ModelState.IsValid)
            {
                var target = uow.Categories.GetById(category.Id);
                if (target != null)
                {
                    target.Name = category.Name;
                    uow.Categories.Update(target);
                    uow.SaveChanges();
                }
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
	}
}
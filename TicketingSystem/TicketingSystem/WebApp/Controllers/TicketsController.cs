using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Entities;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNet.Identity;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace WebApp.Controllers
{
    public class TicketsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Tickets/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public JsonResult GetTickets([DataSourceRequest] DataSourceRequest request)
        {
            var tickets = uow.Tickets.All().Select(t => new TicketFullViewModel() 
            {
                Id = t.Id,
                Title = t.Title,
                CategoryId = t.CategoryId,
                CategoryName = t.Category.Name,
                AuthorId = t.AuthorId,
                AuthorName = t.Author.UserName,
                Priority = t.Priority
            }).ToList();

            return Json(tickets.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult TicketDetails(int id)
        {
            var ticket = uow.Tickets.All().Select(t => new TicketFullViewModel()
            {
                Id = t.Id,
                Title = t.Title,
                ScreenshotUrl = t.ScreenshotUrl,
                Priority = t.Priority,
                NumberOfComments = t.Comments.Count,
                Description = t.Description,
                AuthorId = t.AuthorId,
                AuthorName = t.Author.UserName,
                CategoryId = t.CategoryId,
                CategoryName = t.Category.Name,
                Comments = t.Comments.Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Content = c.Content,
                    AuthorId = c.AuthorId,
                    AuthorName = c.Author.UserName
                }).ToList()
            }).FirstOrDefault(t => t.Id == id);

            return View(ticket);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                uow.Comments.Add(new Comment()
                {
                    AuthorId = userId,
                    Content = commentModel.Comment,
                    TicketId = commentModel.TicketId
                });

                uow.SaveChanges();

                var viewModel = new CommentViewModel
                {
                    AuthorName = username,
                    Content = commentModel.Comment,
                    AuthorId = userId,
                    TicketId = commentModel.TicketId
                };

                return PartialView("_Comment", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public JsonResult GetCategories()
        {
            var result = uow.Categories
                .All()
                .Select(x => new
                {
                    CategoryId = x.Id,
                    CategoryName = x.Name
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(SubmitSearchModel submitModel)
        {
            var result = uow.Tickets.All();

            if (!string.IsNullOrEmpty(submitModel.CategorySearch))
            {
                result = result.Where(x => x.Category.Name.ToLower().Contains(submitModel.CategorySearch.ToLower()));
            }

            var endResult = result.Select(x => new TicketViewModel
            {
                Id = x.Id,
                Title = x.Title,
                CategoryName = x.Category.Name,
                AuthorName = x.Author.UserName
            });

            return View(endResult);
        }

        // GET: /Tickets/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket ticket = db.Tickets.Find(id);
        //    if (ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticket);
        //}

        // GET: /Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(uow.Users.All(), "Id", "UserName");
            ViewBag.CategoryId = new SelectList(uow.Categories.All(), "Id", "Name");
            return View();
        }

        // POST: /Tickets/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketFullViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                var ticketToAdd = new Ticket()
                {
                    Title = ticket.Title,
                    ScreenshotUrl = ticket.ScreenshotUrl,
                    Priority = ticket.Priority,
                    Description = ticket.Description,
                    CategoryId = ticket.CategoryId,
                    AuthorId = User.Identity.GetUserId()
                };
                uow.Tickets.Add(ticketToAdd);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", ticket.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", ticket.CategoryId);
            return View(ticket);
        }

        // GET: /Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", ticket.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", ticket.CategoryId);
            return View(ticket);
        }

        // POST: /Tickets/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "UserName", ticket.AuthorId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", ticket.CategoryId);
            return View(ticket);
        }

        // GET: /Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: /Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

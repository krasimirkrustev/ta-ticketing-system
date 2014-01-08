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

namespace WebApp.Controllers
{
    public class CommentsController : BaseController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Comments/
        public ActionResult Index()
        {
            var comments = uow.Comments.All().Include(c => c.Author).Include(c => c.Ticket);
            return View(comments.ToList());
        }

        // GET: /Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = uow.Comments.All().FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: /Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = uow.Comments.All().FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(uow.Users.All(), "Id", "UserName", comment.AuthorId);
            ViewBag.TicketId = new SelectList(uow.Tickets.All(), "Id", "Title", comment.TicketId);
            return View(comment);
        }

        // POST: /Comments/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                uow.Comments.Update(comment);
                //db.Entry(comment).State = EntityState.Modified;
                //db.SaveChanges();
                uow.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(uow.Users.All(), "Id", "UserName", comment.AuthorId);
            ViewBag.TicketId = new SelectList(uow.Tickets.All(), "Id", "Title", comment.TicketId);
            return View(comment);
        }

        // GET: /Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = uow.Comments.All().FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: /Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = uow.Comments.All().FirstOrDefault(c => c.Id == id);
            //db.Comments.Remove(comment);
            //db.SaveChanges();

            uow.Comments.Delete(comment);
            uow.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

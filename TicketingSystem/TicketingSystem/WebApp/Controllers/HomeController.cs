using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Entities;
using WebApp.Models;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private const int NumberOfTicketsToTake = 6;
        private const string CachingKey = "MostCommentedTickets";
        private const int CachingTime = 1;

        public ActionResult Index()
        {
            if (this.HttpContext.Cache[CachingKey] == null)
            {
                var tickets = GetMostCommentedTickets(NumberOfTicketsToTake);
                this.HttpContext.Cache.Add(
                    CachingKey, 
                    tickets, 
                    null, 
                    DateTime.Now.AddHours(CachingTime), 
                    TimeSpan.Zero, 
                    System.Web.Caching.CacheItemPriority.Default, 
                    null);
            }

            return View(this.HttpContext.Cache[CachingKey]);
        }

        public IEnumerable<TicketViewModel> GetMostCommentedTickets(int count)
        {
            var tickets = uow.Tickets.All()
                .OrderByDescending(ticket => ticket.Comments.Count)
                .Take(count)
                .Select(ticket => new TicketViewModel()
                {
                    Id = ticket.Id,
                    Title = ticket.Title,
                    CategoryId = ticket.CategoryId,
                    CategoryName = ticket.Category.Name,
                    AuthorId = ticket.AuthorId,
                    AuthorName = ticket.Author.UserName,
                    NumberOfComments = ticket.Comments.Count
                }).ToList();

            return tickets;
        }
    }
}
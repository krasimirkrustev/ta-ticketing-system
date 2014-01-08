namespace WebApp.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Entities;

    public class Configuration : DbMigrationsConfiguration<WebApp.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WebApp.Data.ApplicationDbContext context)
        {
            //if (context.Tickets.Count() > 0)
            //{
            //    return;
            //}

            //Random rand = new Random();

            //Category category = new Category { Name = "Web site bugs" };
            //ApplicationUser ticketingUser = new ApplicationUser() { UserName = "TicketingUser", Points = 10 };
            //ApplicationUser answeringUser = new ApplicationUser() { UserName = "AnsweringUser", Points = 10 };

            //for (int i = 0; i < 10; i++)
            //{
            //    Ticket ticket = new Ticket();
            //    ticket.Title = "Home page does not work";
            //    ticket.Category = category;
            //    ticket.CategoryId = category.Id;
            //    ticket.ScreenshotUrl = "http://laptop.bg/system/images/26207/thumb/toshiba_satellite_l8501v8.jpg";
            //    ticket.Author = ticketingUser;
            //    ticket.AuthorId = ticketingUser.Id;
            //    ticket.Priority = Priority.High;
            //    ticket.Description = "The Home page really does not working!!!";

            //    var commentsCount = rand.Next(10, 30);
            //    for (int j = 0; j < commentsCount; j++)
            //    {
            //        ticket.Comments.Add(new Comment { Content = "Needs to be fixed...", Author = answeringUser, AuthorId = answeringUser.Id });
            //    }

            //    context.Tickets.Add(ticket);
            //}

            //context.SaveChanges();

            //base.Seed(context);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.App_Start;

namespace WebApp.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [ShouldNotContains("bug", ErrorMessage="The word 'bug' should not be used in the title!")]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        [AllowHtml]
        public string CategoryName { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public int NumberOfComments { get; set; }
    }
}
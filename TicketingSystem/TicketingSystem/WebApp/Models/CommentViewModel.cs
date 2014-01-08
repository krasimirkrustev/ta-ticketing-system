using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public int TicketId { get; set; }

        public string TicketTitle { get; set; }
    }
}
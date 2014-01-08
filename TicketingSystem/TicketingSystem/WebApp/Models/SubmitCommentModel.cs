using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class SubmitCommentModel
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public int TicketId { get; set; }
    }
}
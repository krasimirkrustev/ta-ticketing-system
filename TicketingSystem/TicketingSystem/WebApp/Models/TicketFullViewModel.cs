using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Entities;

namespace WebApp.Models
{
    public class TicketFullViewModel : TicketViewModel
    {
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [AllowHtml]
        public string ScreenshotUrl { get; set; }

        public Priority Priority { get; set; }

        public string PriorityName
        {
            get { return Enum.GetName(typeof(Priority), this.Priority); }
        }

        public List<CommentViewModel> Comments { get; set; }
    }
}
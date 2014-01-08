using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class ApplicationUser : User
    {
        private ICollection<Ticket> tickets;
        private ICollection<Comment> comments;

        public ApplicationUser()
        {
            this.tickets = new HashSet<Ticket>();
            this.comments = new HashSet<Comment>();
        }

        [Required]
        public int Points { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}

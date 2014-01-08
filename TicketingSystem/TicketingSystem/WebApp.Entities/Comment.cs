using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int? TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}

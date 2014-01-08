using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Ticket
    {
        private ICollection<Comment> comments;

        public Ticket()
        {
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string AuthorId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; }
        }
    }
}

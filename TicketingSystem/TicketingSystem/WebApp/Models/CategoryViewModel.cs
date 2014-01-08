using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class CategoryViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal_blog.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(50), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(50, ErrorMessage = "Maximum of 50 characters can be entered!"), MinLength(5, ErrorMessage = "At least 5 characters must be entered!")]
        public string Header { get; set; }
        [Column(TypeName = "nvarchar"), Required(ErrorMessage = "This field cannot be left empty!"), MinLength(5, ErrorMessage = "At least 5 characters must be entered!")]
        [AllowHtml]
        public string Details { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
        public int ClickCount { get; set; }
        public bool Status { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
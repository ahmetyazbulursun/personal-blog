using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace personal_blog.Models
{
    public class Author
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(50), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(50, ErrorMessage = "Maximum of 50 characters can be entered!"), MinLength(5, ErrorMessage = "At least 5 characters must be entered!")]
        public string Fullname { get; set; }
        public string Image { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(150), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(150, ErrorMessage = "Maximum of 150 characters can be entered!"), MinLength(20, ErrorMessage = "At least 20 characters must be entered!")]
        public string Details { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
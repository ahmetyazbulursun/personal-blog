using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace personal_blog.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(20), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(20, ErrorMessage = "Maximum of 20 characters can be entered!"), MinLength(2, ErrorMessage = "At least 2 characters must be entered!")]
        public string Name { get; set; }
        public bool Status { get; set; }

        public ICollection<Post> Post { get; set; }
    }
}
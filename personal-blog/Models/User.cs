using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace personal_blog.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(15), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(15, ErrorMessage = "Maximum of 15 characters can be entered!"), MinLength(2, ErrorMessage = "At least 2 characters must be entered!")]
        public string Username { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(15), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(15, ErrorMessage = "Maximum of 15 characters can be entered!"), MinLength(2, ErrorMessage = "At least 2 characters must be entered!")]
        public string Password { get; set; }
    }
}
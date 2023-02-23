using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace personal_blog.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(50), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(50, ErrorMessage = "Maximum of 50 characters can be entered!"), MinLength(5, ErrorMessage = "At least 5 characters must be entered!")]
        public string Fullname { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(50), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(50, ErrorMessage = "Maximum of 50 characters can be entered!"), MinLength(5, ErrorMessage = "At least 5 characters must be entered!")]
        public string EMail { get; set; }
        [Column(TypeName = "nvarchar"), StringLength(500), Required(ErrorMessage = "This field cannot be left empty!"), MaxLength(500, ErrorMessage = "Maximum of 500 characters can be entered!"), MinLength(5, ErrorMessage = "At least 5 characters must be entered!")]
        public string Details { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
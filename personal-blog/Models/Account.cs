using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace personal_blog.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        public string Link { get; set; }
        public bool Status { get; set; }

        public int IconID { get; set; }
        public virtual Icon Icon { get; set; }
    }
}
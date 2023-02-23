using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace personal_blog.Models
{
    public class Icon
    {
        [Key]
        public int ID { get; set; }
        public string IconText { get; set; }
        public string IconName { get; set; }
        public bool Status { get; set; }

        public ICollection<Account> Account { get; set; }
    }
}
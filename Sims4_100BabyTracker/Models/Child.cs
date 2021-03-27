using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sims4_100BabyTracker.Models
{
    public class Child
    {        
        [Key]
        public int ChildId { get; set; }

        public string Name { get; set; }

        public string Father { get; set; }

        [ForeignKey("Mother")]
        [Display(Name = "Mother")]
        public int MotherId { get; set; }
        public virtual Matriarch Mother { get; set; }
    }
}
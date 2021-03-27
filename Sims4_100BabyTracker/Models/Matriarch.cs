using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sims4_100BabyTracker.Models
{
    public class Matriarch
    {
        public Matriarch()
        {
            BladderFailures = 0;
            EnergyFailures = 0;
            Children = new List<Child>();
        }
        
        [Key]
        public int MatriarchId { get; set; }

        public string Name { get; set; }

        public int BladderFailures { get; set; }

        public int EnergyFailures { get; set; }
        
        public virtual List<Child> Children { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models {
    public class Crime {
        [Key] public int Id { get; set;}
        [Required] public DateTime Date { get; set;}
        public string Name { get; set; }
        public string Punishment { get; set; }
        public ICollection<CrimeCriminal> Members { get; set; } = new List<CrimeCriminal>();
    }
}
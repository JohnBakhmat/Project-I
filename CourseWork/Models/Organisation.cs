using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models {
    public class Organisation {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Boss { get; set; }
        public ICollection<CriminalOrganisation> Members { get; set; } = new List<CriminalOrganisation>();
    }
}
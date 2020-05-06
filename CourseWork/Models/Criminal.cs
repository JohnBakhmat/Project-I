using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseWork.Models {
    public class Criminal {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public byte Age { get; set; }
        public double Height { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Sex { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string ExtraSigns { get; set; }
        public string LastAccomodation { get; set; }
        public ICollection<CriminalOrganisation> Organisations { get; set; } = new List<CriminalOrganisation>();
        public ICollection<CrimeCriminal> Crimes { get; set; } = new List<CrimeCriminal>();
        public string CrimesAsString { get; set;}
        public string OrganisationsAsString { get; set; }
        public bool IsArchived { get; set; }
    }
}
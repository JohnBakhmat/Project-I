
using System.Collections.Generic;

namespace CourseWork.Models
{
    public class AddCrimeViewModel
    {
        public IEnumerable<CrimeViewModel> Crimes { get; set; }

        public int CriminalId { get; set; }
    }
}

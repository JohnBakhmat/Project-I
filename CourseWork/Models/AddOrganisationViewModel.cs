
using System.Collections.Generic;

namespace CourseWork.Models
{
    public class AddOrganisationViewModel
    {
        public IEnumerable<OrganisationViewModel> Organisations { get; set; }

        public int CriminalId { get; set; }
    }
}

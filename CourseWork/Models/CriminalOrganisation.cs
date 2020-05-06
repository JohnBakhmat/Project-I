using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Models
{
    public class CriminalOrganisation
    {
        [Key] public int ID{get;set;}
        public int CriminalId{get;set;}
        public Criminal Criminal{get;set;}
        public int OrganisationId{get;set;}
        public Organisation Organisation {get;set;}
    }
}

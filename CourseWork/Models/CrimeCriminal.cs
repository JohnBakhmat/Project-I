using System.ComponentModel.DataAnnotations;
using CourseWork.Models;

namespace CourseWork.Models{
    public class CrimeCriminal
    {
        [Key] public int ID{get;set;}
        public int CriminalId{get;set;}
        public Criminal Criminal{get;set;}
        public int CrimeId{get;set;}
        public Crime Crime{get;set;}
    }
}
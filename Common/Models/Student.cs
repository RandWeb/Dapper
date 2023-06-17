using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public string FatherName { get; set; }
        public List<Score> Scores { get; set; }
        public StudentAdditionalInfo StudentAdditionalInfo { get; set; }
        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {NationalCode} {BirthDate}";
        }
    }
}

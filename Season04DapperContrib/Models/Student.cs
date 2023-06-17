using Dapper.Contrib.Extensions;

namespace Season04DapperContrib.Models
{
    [Table("Students")]

    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[Computed]
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        //[Write(false)]
        public string FatherName { get; set; }
        [Write(false)]
        public List<Score> Scores { get; set; }
        [Write(false)]
        public StudentAdditionalInfo StudentAdditionalInfo { get; set; }
        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {NationalCode} {BirthDate}";
        }
    }
}

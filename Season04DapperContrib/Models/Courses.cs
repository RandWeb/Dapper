using Dapper.Contrib.Extensions;

namespace Season04DapperContrib.Models;

[Table("Courses")]
public class Courses
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string TeacherFullName { get; set; }
    public int Capacity { get; set; }
    public override string ToString()
    {
        return $"{Id} {Title} by {TeacherFullName}";
    }
}
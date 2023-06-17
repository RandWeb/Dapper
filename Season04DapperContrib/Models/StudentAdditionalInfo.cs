using Dapper.Contrib.Extensions;

namespace Season04DapperContrib.Models;
[Table("StudentAdditionalInfo")]
public class StudentAdditionalInfo
{
    [ExplicitKey]
    public int Id { get; set; }
    public string About { get; set; }

}
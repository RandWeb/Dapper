using Dapper.Contrib.Extensions;

namespace Season04DapperContrib.Models;

[Table("Scores")]
public class Score
{
    [Key]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public double? ScoreEarned { get; set; }
    public ScoreType ScoreType { get; set; }

    public override string ToString()
    {
        return $"{StudentId},{CourseId},{ScoreType}=> {ScoreEarned}";
    }
}
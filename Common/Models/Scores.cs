namespace Common.Models;

public class Score
{
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
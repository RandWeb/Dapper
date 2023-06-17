using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomModels
{
    public class FinalScoreDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double? ScoreEarned { get; set; }
        public override string ToString()
        {
            return $"{StudentId},{CourseId}=> {ScoreEarned}";
        }
    }

    public class MidTermScoreDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double? ScoreEarned { get; set; }
        public override string ToString()
        {
            return $"{StudentId},{CourseId}=> {ScoreEarned}";
        }
    }
}

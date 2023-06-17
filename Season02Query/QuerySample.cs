using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.CustomModels;
using Common.Models;
using Dapper;

namespace Season02Query
{
    internal class QuerySample
    {
        private static string query01 = "select * from Students where FirstName in @FirstNames";
        private static string query02 = "select * from Students";
        private static string query03 = "select FirstName Name, LastName Family from Students";
        private static string query04 = "select * from Students as s inner join StudentAdditionalInfo as a  on s.id=a.id";
        private static string query05 = "select * from Students as s inner join Scores as a  on s.id=a.studentId";
        private static string query06 = "select * from Scores";
        public static void QueryAnonymous()
        {
            using (var cnn=DapperHelper.ProfilerDbConnection())
            {
                var res = cnn.Query(query01, new { FirstNames = new[]{ "milad" ,"ali"} }).ToList();
                
                foreach (var item in res)
                {
                    Console.WriteLine($"{item.FirstName} {item.LastName}");
                }
                Console.WriteLine(DapperHelper.GetExecutedCommands());
            }
        }

        public static void QueryStronglyType()
        {
            using (var cnn = DapperHelper.ProfilerDbConnection())
            {
                var res = cnn.Query<Student>(query02,buffered:false);

                foreach (var item in res)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(DapperHelper.GetExecutedCommands());
            }
        }

        public static void QueryCustomModel()
        {
            using (var cnn = DapperHelper.ProfilerDbConnection())
            {
                var res = cnn.Query<PersonInfo>(query03).ToList();

                foreach (var item in res)
                {
                    Console.WriteLine($"{item.Name} {item.Family}");
                }
                Console.WriteLine(DapperHelper.GetExecutedCommands());
            }
        }

        public static void QueryOneToOne()
        {
            using (var cnn=DapperHelper.ProfilerDbConnection())
            {

                var res = cnn.Query<Student, StudentAdditionalInfo, Student>(query04,
                    (student, info) =>
                    {
                        student.StudentAdditionalInfo = info;
                        return student;
                    }, splitOn: "Id");
                foreach (var student in res)
                {
                    Console.WriteLine($"{student} {student.StudentAdditionalInfo.About}");
                }

                Console.WriteLine(DapperHelper.GetExecutedCommands());
            }
        }

        public static void QueryOneToMany()
        {
            using (var cnn=DapperHelper.ProfilerDbConnection())
            {
                var studentsDictionary = new Dictionary<int, Student>();
                var res = cnn.Query<Student, Score, Student>(query05,
                    (student, score) =>
                    {
                        Student tempStudent;

                        if (!studentsDictionary.TryGetValue(student.Id,out tempStudent))
                        {
                            tempStudent = student;
                            tempStudent.Scores = new List<Score>();
                            studentsDictionary.Add(tempStudent.Id, tempStudent);
                        }
                        tempStudent.Scores.Add(score);
                        return tempStudent;
                    },splitOn:"Id").Distinct().ToList();

                foreach (var student in res)
                {
                    Console.WriteLine(student);
                    if (student.Scores.Any())
                    {
                        foreach (var score in student.Scores)
                        {
                            Console.WriteLine(score);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Scores");
                    }
                }
                Console.WriteLine(DapperHelper.GetExecutedCommands());

            }
        }

        public static void QueryMultipleType()
        {
            var finalScores = new List<FinalScoreDto>();
            var midTermScores = new List<MidTermScoreDto>();
            using (var cnn=DapperHelper.DbConnection())
            {
                using (var reader=cnn.ExecuteReader(query06))
                {
                    var midTermScoreParser = reader.GetRowParser<MidTermScoreDto>();
                    var finalScoreParser = reader.GetRowParser<FinalScoreDto>();

                    while (reader.Read())
                    {
                        switch ((ScoreType) reader.GetInt32(reader.GetOrdinal("ScoreType")))
                        {
                            case ScoreType.MidTerm:
                                midTermScores.Add(midTermScoreParser(reader));
                                break;
                            case ScoreType.Final:
                                finalScores.Add(finalScoreParser(reader));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            Console.WriteLine("midterm:");
            foreach (var score in midTermScores)
            {
                Console.WriteLine(score.ToString());
            }
            Console.WriteLine("final:");
            foreach (var score in finalScores)
            {
                Console.WriteLine(score.ToString());
            }
        }
    }
}

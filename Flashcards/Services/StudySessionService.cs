using Dapper;
using Flashcards.DTOs;
using Flashcards.Models;
using Flashcards.Services.Contracts;
using Flashcards.Utils;
using Microsoft.Data.SqlClient;

namespace Flashcards.Services
{
    public class StudySessionService : IStudySessionService
    {
        public void AddStudySession(DateTime date, int score, int stackId)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            var addCommand = @"INSERT INTO StudySession(SessionDate, Score, StackId) VALUES(@Date, @Score, @StackId)";
            connection.Execute(addCommand, new { Date = date, Score = score, StackId = stackId });
        }

        public List<StudySession> GetAllStudySessions()
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            var getAllQuery = @"SELECT 
                              ROW_NUMBER() OVER (ORDER BY Id) AS SessionId,
                              SessionDate,
                              Score FROM StudySession";
            return connection.Query<StudySession>(getAllQuery).ToList();
        }

        public List<MonthlySessionReport> GetMonthlySessionsCountReportByYear(int year)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            var reportQuery = @"SELECT 
	                                StackName,
	                                ISNULL([1], 0) AS January,
	                                ISNULL([2], 0) AS February,
	                                ISNULL([3], 0) AS March,
	                                ISNULL([4], 0) AS April,
	                                ISNULL([5], 0) AS May,
	                                ISNULL([6], 0) AS June,
	                                ISNULL([7], 0) AS July,
	                                ISNULL([8], 0) AS August,
	                                ISNULL([9], 0) AS September,
	                                ISNULL([10], 0) AS October,
	                                ISNULL([11], 0) AS November,
	                                ISNULL([12], 0) AS December
                                FROM (
	                                SELECT	
		                                s.Name AS StackName,
		                                MONTH(ss.SessionDate) AS StudyMonth
	                                FROM Stack s
	                                LEFT JOIN StudySession ss
	                                     ON s.Id = ss.StackId
		                                 AND YEAR(ss.SessionDate) = @Year
                                ) AS SourceData
                                PIVOT (
		                                COUNT(StudyMonth)
		                                FOR StudyMonth IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
                                ) AS SessionsByMonth
                                ORDER BY StackName;";
            return connection.Query<MonthlySessionReport>(reportQuery, new { Year = year }).ToList();
        }
        public List<MonthlySessionReport> GetMonthlyAverageScoreByYear(int year)
        {
            using var connection = new SqlConnection(DBHelper.ConnectionString);
            connection.Open();
            var reportQuery = @"
                               SELECT 
                                StackName,
                                ISNULL(ROUND([1], 0), 0) AS January,
                                ISNULL(ROUND([2], 0), 0) AS February,
                                ISNULL(ROUND([3], 0), 0) AS March,
                                ISNULL(ROUND([4], 0), 0) AS April,
                                ISNULL(ROUND([5], 0), 0) AS May,
                                ISNULL(ROUND([6], 0), 0) AS June,
                                ISNULL(ROUND([7], 0), 0) AS July,
                                ISNULL(ROUND([8], 0), 0) AS August,
                                ISNULL(ROUND([9], 0), 0) AS September,
                                ISNULL(ROUND([10], 0), 0) AS October,
                                ISNULL(ROUND([11], 0), 0) AS November,
                                ISNULL(ROUND([12], 0), 0) AS December
                            FROM (
                                SELECT 
                                    s.Name AS StackName,
                                    MONTH(ss.SessionDate) AS StudyMonth,
                                    ss.Score
                                FROM Stack s
                                LEFT JOIN StudySession ss 
                                    ON s.Id = ss.StackId
                                    AND YEAR(ss.SessionDate) = @Year
                            ) AS SourceData
                            PIVOT (
                                AVG(Score)
                                FOR StudyMonth IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
                            ) AS PivotTable
                            ORDER BY StackName;";

            return connection.Query<MonthlySessionReport>(reportQuery, new { Year = year }).ToList();
        }
    }
}

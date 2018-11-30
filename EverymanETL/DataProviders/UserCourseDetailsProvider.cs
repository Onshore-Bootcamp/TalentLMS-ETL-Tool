namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class UserCourseDetailsProvider : CommandExecutor, IDataProvider<UserCourseDetails>
    {
        public UserCourseDetailsProvider(string connectionString, string logPath)
            : base(connectionString, logPath)
        {

        }

        public bool Create(UserCourseDetails source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_USER_COURSE_DETAILS",
                new List<SqlParameter>()
                {
                    new SqlParameter("@UserId", source.UserId),
                    new SqlParameter("@CourseId", source.CourseId),
                    new SqlParameter("@EnrolledOn", source.Enrolled_On),
                    new SqlParameter("@EnrolledOnTimestamp", source.Enrolled_On_Timestamp),
                    new SqlParameter("@CompletionStatus", source.Completion_Status),
                    new SqlParameter("@CompletionPercentage", source.Completion_Percentage),
                    new SqlParameter("@CompletedOn", source.Completed_On),
                    new SqlParameter("@CompletedOnTimestamp", source.Completed_On_Timestamp),
                    new SqlParameter("@ExpiredOn", source.Expired_On),
                    new SqlParameter("@ExpiredOnTimestamp", source.Expired_On_Timestamp),
                    new SqlParameter("@TotalTime", source.Total_Time),
                });
        }

        public bool Delete(UserCourseDetails source)
        {
            return ExecuteProcedureNonQuery("DELETE_USER_COURSE_DETAILS",
                new List<SqlParameter>()
                {
                    new SqlParameter("@UserId", source.UserId),
                    new SqlParameter("@CourseId", source.CourseId)
                });
        }

        public bool Update(UserCourseDetails source)
        {
            return ExecuteProcedureNonQuery("UPDATE_USER_COURSE_DETAILS",
                new List<SqlParameter>()
                {
                    new SqlParameter("@UserId", source.UserId),
                    new SqlParameter("@CourseId", source.CourseId),
                    new SqlParameter("@EnrolledOn", source.Enrolled_On),
                    new SqlParameter("@EnrolledOnTimestamp", source.Enrolled_On_Timestamp),
                    new SqlParameter("@CompletionStatus", source.Completion_Status),
                    new SqlParameter("@CompletionPercentage", source.Completion_Percentage),
                    new SqlParameter("@CompletedOn", source.Completed_On),
                    new SqlParameter("@CompletedOnTimestamp", source.Completed_On_Timestamp),
                    new SqlParameter("@ExpiredOn", source.Expired_On),
                    new SqlParameter("@ExpiredOnTimestamp", source.Expired_On_Timestamp),
                    new SqlParameter("@TotalTime", source.Total_Time),
                });
        }

        public HashSet<UserCourseDetails> ViewAll()
        {
            HashSet<UserCourseDetails> allBranchCourses = new HashSet<UserCourseDetails>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_USER_COURSE_DETAILS");

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    allBranchCourses.Add(LMSMapper.ToUserCourseDetails(row));
                }
            }
            return allBranchCourses;
        }

        public UserCourseDetails ViewById(int source)
        {
            throw new NotImplementedException();
        }
    }
}

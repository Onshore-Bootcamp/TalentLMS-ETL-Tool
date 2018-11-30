namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class GroupCourseProvider : CommandExecutor, IDataProvider<GroupCourse>
    {
        public GroupCourseProvider(string connectionString, string logPath)
            : base(connectionString, logPath)
        {

        }

        public bool Create(GroupCourse source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_GROUP_COURSE",
                new List<SqlParameter>()
                {
                    new SqlParameter("@GroupId", source.GroupId),
                    new SqlParameter("@CourseId", source.CourseId)
                });
        }

        public bool Delete(GroupCourse source)
        {
            return ExecuteProcedureNonQuery("DELETE_GROUP_COURSE",
                new List<SqlParameter>()
                {
                    new SqlParameter("@GroupId", source.GroupId),
                    new SqlParameter("@CourseId", source.CourseId)
                });
        }

        public bool Update(GroupCourse source)
        {
            return ExecuteProcedureNonQuery("UPDATE_GROUP_COURSE",
                new List<SqlParameter>()
                {
                    new SqlParameter("@GroupId", source.GroupId),
                    new SqlParameter("@CourseId", source.CourseId)
                });
        }

        public HashSet<GroupCourse> ViewAll()
        {
            HashSet<GroupCourse> allBranchCourses = new HashSet<GroupCourse>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_GROUP_COURSES");

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    allBranchCourses.Add(LMSMapper.ToGroupCourse(row));
                }
            }
            return allBranchCourses;
        }

        public GroupCourse ViewById(int source)
        {
            throw new NotImplementedException();
        }
    }
}

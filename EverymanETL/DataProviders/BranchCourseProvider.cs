namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class BranchCourseProvider : CommandExecutor, IDataProvider<BranchCourse>
    {
        public BranchCourseProvider(string connectionString, string logPath) 
            : base (connectionString, logPath)
        {

        }

        public bool Create(BranchCourse source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_BRANCH_COURSE",
                new List<SqlParameter>()
                {
                    new SqlParameter("@BranchId", source.BranchId),
                    new SqlParameter("@CourseId", source.CourseId)
                });
        }

        public bool Delete(BranchCourse source)
        {
            return ExecuteProcedureNonQuery("DELETE_BRANCH_COURSE",
                new List<SqlParameter>()
                {
                    new SqlParameter("@BranchId", source.BranchId),
                    new SqlParameter("@CourseId", source.CourseId)
                });
        }

        public bool Update(BranchCourse source)
        {
            return ExecuteProcedureNonQuery("UPDATE_BRANCH_COURSE",
                new List<SqlParameter>()
                {
                    new SqlParameter("@BranchId", source.BranchId),
                    new SqlParameter("@CourseId", source.CourseId)
                });
        }

        public HashSet<BranchCourse> ViewAll()
        {
            HashSet<BranchCourse> allBranchCourses = new HashSet<BranchCourse>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_BRANCH_COURSES");

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    allBranchCourses.Add(LMSMapper.ToBranchCourse(row));
                }
            }
            return allBranchCourses;
        }

        public BranchCourse ViewById(int source)
        {
            throw new NotImplementedException();
        }
    }
}

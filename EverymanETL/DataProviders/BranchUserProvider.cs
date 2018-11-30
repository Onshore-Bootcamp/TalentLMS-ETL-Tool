namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class BranchUserProvider : CommandExecutor, IDataProvider<BranchUser>
    {
        public BranchUserProvider(string connectionString, string logPath)
            : base (connectionString, logPath)
        {

        }
        public bool Create(BranchUser source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_BRANCH_USER",
                new List<SqlParameter>()
                {
                    new SqlParameter("@BranchId", source.BranchId),
                    new SqlParameter("@UserId", source.UserId)
                });
        }

        public bool Delete(BranchUser source)
        {
            return ExecuteProcedureNonQuery("DELETE_BRANCH_USER",
                new List<SqlParameter>()
                {
                    new SqlParameter("@BranchId", source.BranchId),
                    new SqlParameter("@UserId", source.UserId)
                });
        }

        public bool Update(BranchUser source)
        {
            return ExecuteProcedureNonQuery("UPDATE_BRANCH_USER",
                new List<SqlParameter>()
                {
                    new SqlParameter("@BranchId", source.BranchId),
                    new SqlParameter("@UserId", source.UserId)
                });
        }

        public HashSet<BranchUser> ViewAll()
        {
            HashSet<BranchUser> allBranchCourses = new HashSet<BranchUser>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_BRANCH_USERS");

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    allBranchCourses.Add(LMSMapper.ToBranchUser(row));
                }
            }
            return allBranchCourses;
        }

        public BranchUser ViewById(int source)
        {
            throw new NotImplementedException();
        }
    }
}

namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class GroupUserProvider : CommandExecutor, IDataProvider<GroupUser>
    {
        public GroupUserProvider(string connectionString, string logPath)
            : base(connectionString, logPath)
        {

        }

        public bool Create(GroupUser source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_GROUP_USER",
                new List<SqlParameter>()
                {
                    new SqlParameter("@GroupId", source.GroupId),
                    new SqlParameter("@UserId", source.UserId)
                });
        }

        public bool Delete(GroupUser source)
        {
            return ExecuteProcedureNonQuery("DELETE_GROUP_USER",
                new List<SqlParameter>()
                {
                    new SqlParameter("@GroupId", source.GroupId),
                    new SqlParameter("@UserId", source.UserId)
                });
        }

        public bool Update(GroupUser source)
        {
            return ExecuteProcedureNonQuery("UPDATE_GROUP_USER",
                new List<SqlParameter>()
                {
                    new SqlParameter("@GroupId", source.GroupId),
                    new SqlParameter("@UserId", source.UserId)
                });
        }

        public HashSet<GroupUser> ViewAll()
        {
            HashSet<GroupUser> allBranchCourses = new HashSet<GroupUser>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_GROUP_USERS");

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    allBranchCourses.Add(LMSMapper.ToGroupUser(row));
                }
            }
            return allBranchCourses;
        }

        public GroupUser ViewById(int source)
        {
            throw new NotImplementedException();
        }
    }
}

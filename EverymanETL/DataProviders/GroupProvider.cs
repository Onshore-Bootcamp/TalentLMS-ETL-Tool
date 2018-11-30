namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class GroupProvider : CommandExecutor, IDataProvider<Group>
    {
        public GroupProvider(string connectionString, string logPath)
            : base(connectionString, logPath)
        {

        }

        public bool Create(Group source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_GROUP",
                new List<SqlParameter> {
                    new SqlParameter("@GroupId", source.Id),
                    new SqlParameter("@Name", source.name),
                    new SqlParameter("@Description", source.description),
                    new SqlParameter("@Key", source.key),
                    new SqlParameter("@Price", source.price),
                    new SqlParameter("@OwnerId", source.owner_id),
                    new SqlParameter("@BelongsToBranch", source.belongs_to_branch),
                    new SqlParameter("@MaxRedemptions", source.max_redemptions)
                });
        }

        public bool Delete(Group source)
        {
            return ExecuteProcedureNonQuery("DELETE_GROUP",
                new SqlParameter("@GroupId", source.Id));
        }

        public Group ViewById(int source)
        {
            Group response = new Group();
            DataTable result = ExecuteProcedureQuery("VIEW_GROUP_BY_ID",
                new SqlParameter("@GroupId", source));

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response = LMSMapper.ToGroup(row);
                }
            }
            return response;
        }

        public bool Update(Group source)
        {
            return ExecuteProcedureNonQuery("UPDATE_GROUP",
                  new List<SqlParameter> {
                    new SqlParameter("@GroupId", source.Id),
                    new SqlParameter("@Name", source.name),
                    new SqlParameter("@Description", source.description),
                    new SqlParameter("@Key", source.key),
                    new SqlParameter("@Price", source.price),
                    new SqlParameter("@OwnerId", source.owner_id),
                    new SqlParameter("@BelongsToBranch", source.belongs_to_branch),
                    new SqlParameter("@MaxRedemptions", source.max_redemptions)
                  });
        }

        public HashSet<Group> ViewAll()
        {
            HashSet<Group> response = new HashSet<Group>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_GROUPS");
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response.Add(LMSMapper.ToGroup(row));
                }
            }
            return response;
        }
    }
}

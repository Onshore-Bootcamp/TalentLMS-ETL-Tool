namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class UserProvider : CommandExecutor, IDataProvider<User>
    {
        public UserProvider(string connectionString, string logPath)
            : base (connectionString, logPath)
        {

        }

        public bool Create(User source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_USER",
                new List<SqlParameter> {
                    new SqlParameter("@UserId", source.Id),
                    new SqlParameter("@Login", source.login),
                    new SqlParameter("@FirstName", source.first_name),
                    new SqlParameter("@LastName", source.last_name),
                    new SqlParameter("@Email", source.email),
                    new SqlParameter("@UserType", source.user_type),
                    new SqlParameter("@TimeZone", source.timezone),
                    new SqlParameter("@Language", source.language),
                    new SqlParameter("@Status", source.status),
                    new SqlParameter("@DeactivationDate", source.deactivation_date),
                    new SqlParameter("@Level", source.level),
                    new SqlParameter("@Points", source.points),
                    new SqlParameter("@CreatedOn", source.created_on),
                    new SqlParameter("@LastUpdated", source.last_updated),
                    new SqlParameter("@LastUpdatedTimestamp", source.last_updated_timestamp),
                    new SqlParameter("@Avatar", source.avatar),
                    new SqlParameter("@LoginKey", source.login_key)
                });
        }

        public bool Delete(User source)
        {
            return ExecuteProcedureNonQuery("DELETE_USER",
                new SqlParameter("@UserId", source.Id));
        }

        public User ViewById(int source)
        {
            User response = new User();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_USERS");
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response = LMSMapper.ToUser(row);
                    break;
                }
            }
            return response;
        }

        public bool Update(User source)
        {
            return ExecuteProcedureNonQuery("UPDATE_USER",
                new List<SqlParameter> {
                    new SqlParameter("@UserId", source.Id),
                    new SqlParameter("@Login", source.login),
                    new SqlParameter("@FirstName", source.first_name),
                    new SqlParameter("@LastName", source.last_name),
                    new SqlParameter("@Email", source.email),
                    new SqlParameter("@UserType", source.user_type),
                    new SqlParameter("@TimeZone", source.timezone),
                    new SqlParameter("@Language", source.language),
                    new SqlParameter("@Status", source.status),
                    new SqlParameter("@DeactivationDate", source.deactivation_date),
                    new SqlParameter("@Level", source.level),
                    new SqlParameter("@Points", source.points),
                    new SqlParameter("@CreatedOn", source.created_on),
                    new SqlParameter("@LastUpdated", source.last_updated),
                    new SqlParameter("@LastUpdatedTimestamp", source.last_updated_timestamp),
                    new SqlParameter("@Avatar", source.avatar),
                    new SqlParameter("@LoginKey", source.login_key)
                });
        }

        public HashSet<User> ViewAll()
        {
            HashSet<User> response = new HashSet<User>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_USERS");
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response.Add(LMSMapper.ToUser(row));
                }
            }
            return response;
        }
    }
}

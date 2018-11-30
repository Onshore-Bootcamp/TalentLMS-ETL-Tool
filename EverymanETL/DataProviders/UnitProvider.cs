namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class UnitProvider : CommandExecutor, IDataProvider<Unit>
    {
        public UnitProvider(string connectionstring, string logPath)
            : base(connectionstring, logPath)
        {

        }

        public bool Create(Unit source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_UNIT",
                new List<SqlParameter> {
                    new SqlParameter("@UnitId", source.Id),
                    new SqlParameter("@CourseId", source.course_id),
                    new SqlParameter("@Type", source.type),
                    new SqlParameter("@Name", source.name),
                    new SqlParameter("@Url", source.url)
                });
        }

        public bool Delete(Unit source)
        {
            return ExecuteProcedureNonQuery("DELETE_UNIT",
                new SqlParameter("@UnitId", source.Id));
        }

        public Unit ViewById(int source)
        {
            Unit response = new Unit();
            DataTable result = ExecuteProcedureQuery("VIEW_UNIT_BY_ID",
                new SqlParameter("@UnitId", source));

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response = LMSMapper.ToUnit(row);
                }
            }
            return response;
        }

        public bool Update(Unit source)
        {
            return ExecuteProcedureNonQuery("UPDATE_UNIT",
                new List<SqlParameter> {
                    new SqlParameter("@UnitId", source.Id),
                    new SqlParameter("@CourseId", source.course_id),
                    new SqlParameter("@Type", source.type),
                    new SqlParameter("@Name", source.name),
                    new SqlParameter("@Url", source.url)
                });
        }

        public HashSet<Unit> ViewAll()
        {
            HashSet<Unit> response = new HashSet<Unit>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_UNITS");
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response.Add(LMSMapper.ToUnit(row));
                }
            }
            return response;
        }
    }
}

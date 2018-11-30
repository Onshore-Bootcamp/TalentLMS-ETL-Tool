namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class UnitCompletionProvider : CommandExecutor, IDataProvider<UnitCompletion>
    {
        public UnitCompletionProvider(string connectionString, string logPath)
            : base (connectionString, logPath)
        {

        }
        public bool Create(UnitCompletion source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_UNIT_COMPLETION",
                new List<SqlParameter>()
                {
                    new SqlParameter("@UnitId", source.Id),
                    new SqlParameter("@UserId", source.UserId),
                    new SqlParameter("@CourseId", source.CourseId),
                    new SqlParameter("@CompletionStatus", source.Completion_Status),
                    new SqlParameter("@CompletedOn", source.Completed_On),
                    new SqlParameter("@CompletedOnTimestamp", source.Completed_On_Timestamp),
                    new SqlParameter("@Score", source.Score)
                });
        }

        public bool Delete(UnitCompletion source)
        {
            return ExecuteProcedureNonQuery("DELETE_UNIT_COMPLETION",
                new List<SqlParameter>()
                {
                    new SqlParameter("@UnitCompletionId", source.UnitCompletionId)
                });
        }

        public bool Update(UnitCompletion source)
        {
            return ExecuteProcedureNonQuery("UPDATE_UNIT_COMPLETION",
                new List<SqlParameter>()
                {
                    new SqlParameter("@UnitId", source.Id),
                    new SqlParameter("@UserId", source.UserId),
                    new SqlParameter("@CourseId", source.CourseId),
                    new SqlParameter("@CompletionStatus", source.Completion_Status),
                    new SqlParameter("@CompletedOn", source.Completed_On),
                    new SqlParameter("@CompletedOnTimestamp", source.Completed_On_Timestamp),
                    new SqlParameter("@Score", source.Score)
                });
        }

        public HashSet<UnitCompletion> ViewAll()
        {
            HashSet<UnitCompletion> response = new HashSet<UnitCompletion>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_UNIT_COMPLETION");
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response.Add(LMSMapper.ToUnitCompletion(row));
                }
            }
            return response;
        }

        public UnitCompletion ViewById(int source)
        {
            throw new NotImplementedException();
        }
    }
}

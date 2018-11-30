using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CommandExecutor
{
    public class CommandExecutor : IDisposable
    {
        protected readonly string _LogFile;
        protected readonly string _ConnectionString;

        public CommandExecutor(string iConnectionString, string iLogFile)
        {
            _ConnectionString = iConnectionString;
            _LogFile = iLogFile;
        }

        protected bool ExecuteProcedureNonQuery(string iStoredProcedure)
        {
            return ExecuteProcedureNonQuery(iStoredProcedure, null as List<SqlParameter>, 30);
        }

        protected bool ExecuteProcedureNonQuery(string iStoredProcedure, SqlParameter iParam)
        {
            return ExecuteProcedureNonQuery(iStoredProcedure, new List<SqlParameter>() { iParam }, 30);
        }

        protected bool ExecuteProcedureNonQuery(string iStoredProcedure, List<SqlParameter> iParams)
        {
            return ExecuteProcedureNonQuery(iStoredProcedure, iParams, 30);
        }

        protected bool ExecuteProcedureNonQuery(string iStoredProcedure, SqlParameter iParam, int iCommandTimeout)
        {
            return ExecuteProcedureNonQuery(iStoredProcedure, new List<SqlParameter>() { iParam }, iCommandTimeout);
        }

        protected bool ExecuteProcedureNonQuery(string iStoredProcedure, List<SqlParameter> iParams, int iCommandTimeout)
        {
            //Variable for the number of affected rows.
            int lRowsAffected = 0;

            //Open a connection on the supplied connection string.
            //Also setup the command and use the newly created connection.
            using (SqlConnection lConnection = new SqlConnection(_ConnectionString))
            using (SqlCommand lCommand = new SqlCommand(iStoredProcedure, lConnection))
            {
                //Set the command type to a stored procedure.
                lCommand.CommandType = CommandType.StoredProcedure;

                //Set the override command timeout, defaulted to 30 over overloads.
                lCommand.CommandTimeout = iCommandTimeout;

                //Assign parameters if any given.
                if (iParams != null) { lCommand.Parameters.AddRange(iParams.ToArray()); }

                try
                {
                    lConnection.Open();
                    //Run the command and capture the number of rows affected.
                    lRowsAffected = lCommand.ExecuteNonQuery();
                }
                catch (SqlException lEx)
                {
                    //Log any errors to a file.
                    using (StreamWriter writer = new StreamWriter(_LogFile, true))
                    {
                        writer.WriteLine("{0}-{1}-{2}", DateTime.Now.ToString("yyyy-MM-dd"), "DB Exception", lEx.Message);
                    }
                }
                finally
                {
                    lConnection.Close();
                    //Close the connection.
                }
            }
            //Return value is based on the number of rows affected by the query.
            return lRowsAffected > 0;
        }

        protected DataTable ExecuteProcedureQuery(string iCommand)
        {
            return ExecuteProcedureQuery(iCommand, null as List<SqlParameter>, 30);
        }

        protected DataTable ExecuteProcedureQuery(string iCommand, SqlParameter iParam)
        {
            return ExecuteProcedureQuery(iCommand, new List<SqlParameter>() { iParam }, 30);
        }

        protected DataTable ExecuteProcedureQuery(string iCommand, SqlParameter iParam, int iCommandTimeout)
        {
            return ExecuteProcedureQuery(iCommand, new List<SqlParameter>() { iParam }, iCommandTimeout);
        }

        protected DataTable ExecuteProcedureQuery(string iCommand, List<SqlParameter> iParams)
        {
            return ExecuteProcedureQuery(iCommand, iParams, 30);
        }

        protected DataTable ExecuteProcedureQuery(string iStoredProcedure, List<SqlParameter> iParams, int iCommandTimeout)
        {
            if (iCommandTimeout <= 0)
                iCommandTimeout = 30;

            DataTable oQueryResult = new DataTable();

            using (SqlConnection lConnection = new SqlConnection(_ConnectionString))
            using (SqlCommand lCommand = new SqlCommand(iStoredProcedure, lConnection))
            {
                lCommand.CommandType = CommandType.StoredProcedure;
                lCommand.CommandTimeout = iCommandTimeout;

                if (iParams != null)
                {
                    lCommand.Parameters.AddRange(iParams.ToArray());
                }

                try
                {
                    lConnection.Open();
                    using (SqlDataAdapter lAdapter = new SqlDataAdapter(lCommand))
                    {
                        lAdapter.Fill(oQueryResult);
                    }
                }
                catch (SqlException lEx)
                {
                    using (StreamWriter writer = new StreamWriter(_LogFile, true))
                    {
                        writer.WriteLine("{0}-{1}-{2}", DateTime.Now.ToString("yyyy-MM-dd"), "DB Exception", lEx.Message);
                    }
                }
                finally
                {
                    lConnection.Close();
                }
            }
            return oQueryResult;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

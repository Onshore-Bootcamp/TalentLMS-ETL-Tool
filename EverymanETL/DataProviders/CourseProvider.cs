namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class CourseProvider : CommandExecutor, IDataProvider<Course>
    {
        public CourseProvider(string connectionString, string logPath)
            : base(connectionString, logPath)
        {

        }

        public bool Create(Course source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_COURSE",
                new List<SqlParameter> {
                    new SqlParameter("@CourseId", source.Id),
                    new SqlParameter("@Name", source.Name),
                    new SqlParameter("@Code", source.Code),
                    new SqlParameter("@CategoryId", source.Category_Id),
                    new SqlParameter("@Description", source.Description),
                    new SqlParameter("@Price", source.Price),
                    new SqlParameter("@Status", source.Status),
                    new SqlParameter("@CreationDate", source.Creation_Date),
                    new SqlParameter("@LastUpdatedOn", source.Last_Update_On),
                    new SqlParameter("@CreatorId", source.Creator_Id),
                    new SqlParameter("@HideFromCatalog", source.Hide_From_Catalog),
                    new SqlParameter("@TimeLimit", source.Time_Limit),
                    new SqlParameter("@Level", source.Level),
                    new SqlParameter("@Shared", source.Shared),
                    new SqlParameter("@SharedUrl", source.Shared_Url),
                    new SqlParameter("@Avatar", source.Avatar),
                    new SqlParameter("@BigAvatar", source.Big_Avatar),
                    new SqlParameter("@Certification", source.Certification),
                    new SqlParameter("@CertificationDuration", source.Certification_Duration)
                });
        }

        public bool Delete(Course source)
        {
            return ExecuteProcedureNonQuery("DELETE_COURSE",
                new SqlParameter("@CourseId", source.Id));
        }

        public Course ViewById(int source)
        { 
            Course response = new Course();
            DataTable result = ExecuteProcedureQuery("VIEW_COURSE_BY_ID",
                new SqlParameter("@CourseId", source));

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response = LMSMapper.ToCourse(row);
                    break;
                }
            }
            return response;
        }

        public bool Update(Course source)
        {
            return ExecuteProcedureNonQuery("UPDATE_COURSE",
                new List<SqlParameter> {
                    new SqlParameter("@CourseId", source.Id),
                    new SqlParameter("@Name", source.Name),
                    new SqlParameter("@Code", source.Code),
                    new SqlParameter("@CategoryId", source.Category_Id),
                    new SqlParameter("@Description", source.Description),
                    new SqlParameter("@Price", source.Price),
                    new SqlParameter("@Status", source.Status),
                    new SqlParameter("@CreationDate", source.Creation_Date),
                    new SqlParameter("@LastUpdatedOn", source.Last_Update_On),
                    new SqlParameter("@CreatorId", source.Creator_Id),
                    new SqlParameter("@HideFromCatalog", source.Hide_From_Catalog),
                    new SqlParameter("@TimeLimit", source.Time_Limit),
                    new SqlParameter("@Level", source.Level),
                    new SqlParameter("@Shared", source.Shared),
                    new SqlParameter("@SharedUrl", source.Shared_Url),
                    new SqlParameter("@Avatar", source.Avatar),
                    new SqlParameter("@BigAvatar", source.Big_Avatar),
                    new SqlParameter("@Certification", source.Certification),
                    new SqlParameter("@CertificationDuration", source.Certification_Duration)
                });
        }

        public HashSet<Course> ViewAll()
        {
            HashSet<Course> response = new HashSet<Course>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_COURSES");
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response.Add(LMSMapper.ToCourse(row));
                }
            }
            return response;
        }
    }
}

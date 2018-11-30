namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class CategoryProvider : CommandExecutor, IDataProvider<Category>
    {
        public CategoryProvider(string connectionString, string logPath)
            : base(connectionString, logPath)
        {

        }

        public bool Create(Category source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_CATEGORY",
                new List<SqlParameter> {
                    new SqlParameter("@CategoryId", source.Id),
                    new SqlParameter("@Name", source.Name),
                    new SqlParameter("@Price", source.Price),
                    new SqlParameter("@ParentCategory", source.parent_category_id)
                });
        }

        public bool Delete(Category source)
        {
            return ExecuteProcedureNonQuery("DELETE_CATEGORY",
                new SqlParameter("@CategoryId", source.Id));
        }

        public Category ViewById(int source)
        {
            Category category = new Category();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_CATEGORIES",
                new SqlParameter("@CategoryId", source));

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    category = LMSMapper.ToCategory(row);
                    break;
                }
            }
            return category;
        }

        public bool Update(Category source)
        {
            return ExecuteProcedureNonQuery("UPDATE_CATEGORY",
                new List<SqlParameter>()
                {
                    new SqlParameter("@CategoryId", source.Id),
                    new SqlParameter("@Name", source.Name),
                    new SqlParameter("@Price", source.Price),
                    new SqlParameter("@ParentCategory", source.parent_category_id)
                });
        }

        public HashSet<Category> ViewAll()
        {
            HashSet<Category> allCategories = new HashSet<Category>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_CATEGORIES");
            if(result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    allCategories.Add(LMSMapper.ToCategory(row));
                }
            }
            return allCategories;
        }
    }
}

namespace EverymanETL.DataProviders
{
    using CommandExecutor;
    using EverymanETL.Mapping;
    using EverymanETL.Models.API;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class BranchProvider : CommandExecutor, IDataProvider<Branch>
    {
        public BranchProvider(string connectionString, string logPath) 
            : base(connectionString, logPath)
        {

        }

        public bool Create(Branch source)
        {
            return ExecuteProcedureNonQuery("CREATE_NEW_BRANCH",
                new List<SqlParameter> {
                    new SqlParameter("@BranchId", source.Id),
                    new SqlParameter("@Name", source.Name),
                    new SqlParameter("@Description", source.Description),
                    new SqlParameter("@Avatar", source.Avatar),
                    new SqlParameter("@Theme", source.Theme),
                    new SqlParameter("@Timezone", source.TimeZone),
                    new SqlParameter("@SignupMethod", source.SignUp_Method),
                    new SqlParameter("@InternalAnnouncement", source.Internal_Announcement),
                    new SqlParameter("@ExternalAnnouncement", source.External_Announcement),
                    new SqlParameter("@Language", source.Language),
                    new SqlParameter("@UserTypeId", source.User_Type_Id),
                    new SqlParameter("@UserType", source.User_Type),
                    new SqlParameter("@GroupId", source.Group_Id),
                    new SqlParameter("@RegistrationEmailRestriction", source.Registration_Email_Restriction),
                    new SqlParameter("@UserLimit", source.Users_Limit),
                    new SqlParameter("@DisallowGlobalLogin", source.Disallow_Global_Login),
                    new SqlParameter("@PaymentProcessor", source.Payment_Processor),
                    new SqlParameter("@Currency", source.Currency),
                    new SqlParameter("@PaypalEmail", source.PayPal_Email),
                    new SqlParameter("@eCommerceSubscription", source.eCommerce_Subscription),
                    new SqlParameter("@eCommerceSubscriptionPrice", source.eCommerce_Subscription_Price),
                    new SqlParameter("@eCommerceSubscriptionInterval", source.eCommerce_Subscription_Interval),
                    new SqlParameter("@eCommerceSubscriptionTrialPeriod", source.eCommerce_Subscription_Trial_Period),
                    new SqlParameter("@eCommerceCredits", source.eCommerce_Credits)
                });
        }

        public bool Delete(Branch source)
        {
            return ExecuteProcedureNonQuery("DELETE_BRANCH",
                new SqlParameter("@BranchId", source.Id));
        }

        public Branch ViewById(int branchId)
        {
            Branch response = new Branch();
            DataTable result = ExecuteProcedureQuery("VIEW_BRANCH_BY_ID",
                new SqlParameter("@BranchId", branchId));

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response = LMSMapper.ToBranch(row);
                    break;
                }
            }
            return response;
        }

        public bool Update(Branch source)
        {
            return ExecuteProcedureNonQuery("UPDATE_BRANCH",
                   new List<SqlParameter> {
                    new SqlParameter("@BranchId", source.Id),
                    new SqlParameter("@Name", source.Name),
                    new SqlParameter("@Description", source.Description),
                    new SqlParameter("@Avatar", source.Avatar),
                    new SqlParameter("@Theme", source.Theme),
                    new SqlParameter("@Timezone", source.TimeZone),
                    new SqlParameter("@SignupMethod", source.SignUp_Method),
                    new SqlParameter("@InternalAnnouncement", source.Internal_Announcement),
                    new SqlParameter("@ExternalAnnouncement", source.External_Announcement),
                    new SqlParameter("@Language", source.Language),
                    new SqlParameter("@UserTypeId", source.User_Type_Id),
                    new SqlParameter("@UserType", source.User_Type),
                    new SqlParameter("@GroupId", source.Group_Id),
                    new SqlParameter("@RegistrationEmailRestriction", source.Registration_Email_Restriction),
                    new SqlParameter("@UserLimit", source.Users_Limit),
                    new SqlParameter("@DisallowGlobalLogin", source.Disallow_Global_Login),
                    new SqlParameter("@PaymentProcessor", source.Payment_Processor),
                    new SqlParameter("@Currency", source.Currency),
                    new SqlParameter("@PaypalEmail", source.PayPal_Email),
                    new SqlParameter("@eCommerceSubscription", source.eCommerce_Subscription),
                    new SqlParameter("@eCommerceSubscriptionPrice", source.eCommerce_Subscription_Price),
                    new SqlParameter("@eCommerceSubscriptionInterval", source.eCommerce_Subscription_Interval),
                    new SqlParameter("@eCommerceSubscriptionTrialPeriod", source.eCommerce_Subscription_Trial_Period),
                    new SqlParameter("@eCommerceCredits", source.eCommerce_Credits)
                   });
        }

        public HashSet<Branch> ViewAll()
        {
            HashSet<Branch> response = new HashSet<Branch>();
            DataTable result = ExecuteProcedureQuery("VIEW_ALL_BRANCHES");
            if(result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    response.Add(LMSMapper.ToBranch(row));
                }
            }
            return response;
        }
    }
}

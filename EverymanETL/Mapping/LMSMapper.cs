namespace EverymanETL.Mapping
{
    using EverymanETL.Models.API;
    using System;
    using System.Data;

    public static class LMSMapper
    {
        public static Branch ToBranch(DataRow row)
        {
            Branch response = new Branch();
            response.Id = Get<object, long>(row["BranchId"]);
            response.Name = Get<object, string>(row["Name"]); //(string)row["Name"];
            response.Description = Get<object, string>(row["Description"]);
            response.Avatar = Get<object, string>(row["Avatar"]);
            response.Theme = Get<object, string>(row["Theme"]);
            response.TimeZone = Get<object, string>(row["Timezone"]);
            response.SignUp_Method = Get<object, string>(row["SignUpMethod"]);
            response.Internal_Announcement = Get<object, string>(row["InternalAnnouncement"]);
            response.External_Announcement = Get<object, string>(row["ExternalAnnouncement"]);
            response.Language = Get<object, string>(row["Language"]);
            response.User_Type_Id = Get<object, string>(row["UserTypeId"]);
            response.User_Type = Get<object, string>(row["UserType"]);
            response.Group_Id = Get<object, long>(row["GroupId"]);
            response.Registration_Email_Restriction = Get<object, string>(row["RegistrationEmailRestriction"]);
            response.Users_Limit = Get<object, string>(row["UserLimit"]);
            response.Disallow_Global_Login = Get<object, string>(row["DisallowGlobalLogin"]);
            response.Payment_Processor = Get<object, string>(row["PaymentProcessor"]);
            response.Currency = Get<object, string>(row["Currency"]);
            response.PayPal_Email = Get<object, string>(row["PaypalEmail"]);
            response.eCommerce_Subscription = Get<object, string>(row["eCommerceSubscription"]);
            response.eCommerce_Subscription_Price = Get<object, string>(row["eCommerceSubscriptionPrice"]);
            response.eCommerce_Subscription_Interval = Get<object, string>(row["eCommerceSubscriptionInterval"]);
            response.eCommerce_Subscription_Trial_Period = Get<object, string>(row["eCommerceSubscriptionTrialPeriod"]);
            response.eCommerce_Credits = Get<object, string>(row["eCommerceCredits"]);
            return response;
        }

        //F
        public static BranchCourse ToBranchCourse(DataRow row)
        {
            long branchCourseId = Get<object, long>(row["BranchCourseId"]);
            long branchId = Get<object, long>(row["BranchId"]);
            long courseId = Get<object, long>(row["CourseId"]);

            return new BranchCourse(branchCourseId, branchId, courseId);
        }
        //F
        public static BranchUser ToBranchUser(DataRow row)
        {
            long branchUserId = Get<object, long>(row["BranchUserId"]);
            long branchId = Get<object, long>(row["BranchId"]);
            long userId = Get<object, long>(row["UserId"]);

            return new BranchUser(branchUserId, branchId, userId);
        }
        //F
        public static Course ToCourse(DataRow row)
        {
            Course response = new Course();
            response.Id = Get<object, int>(row["CourseId"]);
            response.Name = Get<object, string>(row["Name"]);
            response.Code = Get<object, string>(row["Code"]);
            response.Category_Id = Get<object, string>(row["CategoryId"]);
            response.Description = Get<object, string>(row["Description"]);
            response.Price = Get<object, string>(row["Price"]);
            response.Status = Get<object, string>(row["Status"]);
            response.Creation_Date = Get<object, string>(row["CreationDate"]);
            response.Last_Update_On = Get<object, string>(row["LastUpdatedOn"]);
            response.Creator_Id = Get<object, string>(row["CreatorId"]);
            response.Hide_From_Catalog = Get<object, string>(row["HideFromCatalog"]);
            response.Time_Limit = Get<object, string>(row["TimeLimit"]);
            response.Level = Get<object, string>(row["Level"]);
            response.Shared = Get<object, string>(row["Shared"]);
            response.Shared_Url = Get<object, string>(row["SharedUrl"]);
            response.Avatar = Get<object, string>(row["Avatar"]);
            response.Big_Avatar = Get<object, string>(row["BigAvatar"]);
            response.Certification = Get<object, string>(row["Certification"]);
            response.Certification_Duration = Get<object, string>(row["CertificationDuration"]);

            return response;
        }
        //F
        public static User ToUser(DataRow row)
        {
            User response = new User();

            response.Id = Get<object, long>(row["UserId"]);
            response.login = Get<object, string>(row["Login"]);
            response.first_name = Get<object, string>(row["FirstName"]);
            response.last_name = Get<object, string>(row["LastName"]);
            response.email = Get<object, string>(row["Email"]);
            response.user_type = Get<object, string>(row["UserType"]);
            response.timezone = Get<object, string>(row["TimeZone"]);
            response.language = Get<object, string>(row["Language"]);
            response.status = Get<object, string>(row["Status"]);
            response.deactivation_date = Get<object, string>(row["DeactivationDate"]);
            response.level = Get<object, string>(row["Level"]);
            response.points = Get<object, string>(row["Points"]);
            response.created_on = Get<object, string>(row["CreatedOn"]);
            response.last_updated = Get<object, string>(row["LastUpdated"]);
            response.last_updated_timestamp = Get<object, string>(row["LastUpdatedTimestamp"]);
            response.avatar = Get<object, string>(row["Avatar"]);
            response.login_key = Get<object, string>(row["LoginKey"]);

            return response;
        }
        //F
        public static UserCourseDetails ToUserCourseDetails(DataRow row)
        {
            UserCourseDetails response = new UserCourseDetails();

            response.UserCourseDetailsId = Get<object, long>(row["UserCourseDetailsId"]);
            response.UserId = Get<object, long>(row["UserId"]);
            response.CourseId = Get<object, long>(row["CourseId"]);
            response.Enrolled_On = Get<object, string>(row["EnrolledOn"]);
            response.Enrolled_On_Timestamp = Get<object, string>(row["EnrolledOnTimestamp"]);
            response.Completion_Status = Get<object, string>(row["CompletionStatus"]);
            response.Completion_Percentage = Get<object, string>(row["CompletionPercentage"]);
            response.Completed_On = Get<object, string>(row["CompletedOn"]);
            response.Completed_On_Timestamp = Get<object, string>(row["CompletedOnTimestamp"]);
            response.Expired_On = Get<object, string>(row["ExpiredOn"]);
            response.Expired_On_Timestamp = Get<object, string>(row["ExpiredOnTimestamp"]);
            response.Total_Time = Get<object, string>(row["TotalTime"]);

            return response;
        }

        public static Category ToCategory(DataRow row)
        {
            Category response = new Category();

            response.Id = Get<object, long>(row["CategoryId"]);
            response.Name = Get<object, string>(row["Name"]);
            response.Price = Get<object, string>(row["Price"]);
            response.parent_category_id = Get<object, string>(row["ParentCategory"]);

            return response;
        }

        public static Unit ToUnit(DataRow row)
        {
            Unit response = new Unit();

            response.Id = Get<object, long>(row["UnitId"]);
            response.course_id = Get<object, long>(row["CourseId"]);
            response.type = Get<object, string>(row["Type"]);
            response.name = Get<object, string>(row["Name"]);
            response.url = Get<object, string>(row["Url"]);

            return response;
        }

        public static Group ToGroup(DataRow row)
        {
            Group response = new Group();

            response.Id = Get<object, long>(row["GroupId"]);
            response.name = Get<object, string>(row["Name"]);
            response.description = Get<object, string>(row["Description"]);
            response.key = Get<object, string>(row["Key"]);
            response.price = Get<object, string>(row["Price"]);
            response.owner_id = Get<object, string>(row["OwnerId"]);
            response.belongs_to_branch = Get<object, string>(row["BelongsToBranch"]);
            response.max_redemptions = Get<object, string>(row["MaxRedemptions"]);

            return response;
        }

        public static GroupUser ToGroupUser(DataRow row)
        {
            long groupUserId = Get<object, long>(row["GroupUserId"]);
            long groupId = Get<object, long>(row["GroupId"]);
            long userId = Get<object, long>(row["UserId"]);

            return new GroupUser(groupUserId, groupId, userId);
        }

        public static GroupCourse ToGroupCourse(DataRow row)
        {
            long groupCourseId = Get<object, long>(row["GroupCourseId"]);
            long groupId = Get<object, long>(row["GroupId"]);
            long courseId = Get<object, long>(row["CourseId"]);

            return new GroupCourse(groupCourseId, groupId, courseId);
        }

        public static UnitCompletion ToUnitCompletion(DataRow row)
        {
            UnitCompletion response = new UnitCompletion();

            response.UnitCompletionId = Get<object, long>(row["UnitCompletionId"]);
            response.Id = Get<object, long>(row["UnitId"]);
            response.CourseId = Get<object, long>(row["CourseId"]);
            response.UserId = Get<object, long>(row["UserId"]);
            response.Completion_Status = Get<object, string>(row["CompletionStatus"]);
            response.Completed_On = Get<object, string>(row["CompletedOn"]);
            response.Completed_On_Timestamp = Get<object, string>(row["CompletedOnTimestamp"]);
            response.Score = Get<object, string>(row["Score"]);

            return response;
        }

        private static T Get<U, T>(U value)
        {
            T response = default(T);
            if (!(value is DBNull))
            {
                try
                {
                    response = (T)Convert.ChangeType(value, typeof(T));
                }
                catch (InvalidCastException castException)
                {
                    //throw ex?
                    throw castException;
                }
            }
            return response;
        }
    }
}

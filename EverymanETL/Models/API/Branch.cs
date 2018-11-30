using System.Collections.Generic;

namespace EverymanETL.Models.API
{
    public class Branch : IUnique
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Avatar { get; set; }

        public string Theme { get; set; }

        public string TimeZone { get; set; }

        public string SignUp_Method { get; set; }

        public string Internal_Announcement { get; set; }

        public string External_Announcement { get; set; }

        public string Language { get; set; }

        public string User_Type_Id { get; set; }

        public string User_Type { get; set; }

        public long? Group_Id { get; set; }

        public string Registration_Email_Restriction { get; set; }

        public string Users_Limit { get; set; }

        public string Disallow_Global_Login { get; set; }

        public string Payment_Processor { get; set; }

        public string Currency { get; set; }

        public string PayPal_Email{ get; set; }

        public string eCommerce_Subscription { get; set; }

        public string eCommerce_Subscription_Price { get; set; }

        public string eCommerce_Subscription_Interval { get; set; }

        public string eCommerce_Subscription_Trial_Period { get; set; }

        public string eCommerce_Credits { get; set; }

        public List<User> Users { get; set; }

        public List<Course> Courses { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Branch) && (obj as Branch).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

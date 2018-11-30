namespace EverymanETL.Models.API
{
    public class User : IUnique
    {
        public long Id { get; set; }

        public string login { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string email { get; set; }

        public string user_type { get; set; }

        public string timezone { get; set; }

        public string language { get; set; }

        public string status { get; set; }

        public string deactivation_date { get; set; }

        public string level { get; set; }

        public string points { get; set; }

        public string created_on { get; set; }

        public string last_updated { get; set; }

        public string last_updated_timestamp { get; set; }

        public string avatar { get; set; }

        public string login_key { get; set; }

        public dynamic courses { get; set; }
        public dynamic branches { get; set; }
        public dynamic groups { get; set; }
        public dynamic certifications { get; set; }
        public dynamic badges { get; set; }


        public override bool Equals(object obj)
        {
            return (obj is User) && (obj as User).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

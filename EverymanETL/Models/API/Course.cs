using System.Collections.Generic;

namespace EverymanETL.Models.API
{
    public class Course : IUnique
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Category_Id { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Status { get; set; }

        public string Creation_Date { get; set; }

        public string Last_Update_On { get; set; }

        public string Creator_Id { get; set; }

        public string Hide_From_Catalog { get; set; }

        public string Time_Limit { get; set; }

        public string Level { get; set; }

        public string Shared { get; set; }

        public string Shared_Url { get; set; }

        public string Avatar { get; set; }

        public string Big_Avatar { get; set; }

        public string Certification { get; set; }

        public string Certification_Duration { get; set; }

        //users status
        public List<User> Users { get; set; }
        public List<Unit> units { get; set; }
        public dynamic rules { get; set; }
        public dynamic prerequisites { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Course) && (obj as Course).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

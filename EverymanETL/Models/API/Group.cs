using System.Collections.Generic;

namespace EverymanETL.Models.API
{
    public class Group : IUnique
    {
        public long Id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string key { get; set; }

        public string price { get; set; }

        public string owner_id { get; set; }

        public string belongs_to_branch { get; set; }

        public string max_redemptions { get; set; }

        public List<User> Users { get; set; }
        public List<Course> courses { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Group) && (obj as Group).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

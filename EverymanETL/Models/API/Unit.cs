namespace EverymanETL.Models.API
{
    public class Unit : IUnique
    {
        public long Id { get; set; }

        public string name { get; set; }

        public long course_id { get; set; }

        public string type { get; set; }

        public string url { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Unit) &&
                (obj as Unit).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

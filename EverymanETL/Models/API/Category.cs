namespace EverymanETL.Models.API
{
    public class Category : IUnique
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string parent_category_id { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Category) && (obj as Category).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

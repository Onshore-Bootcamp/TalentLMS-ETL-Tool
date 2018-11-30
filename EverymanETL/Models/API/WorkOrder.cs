namespace EverymanETL.Models.API
{
    using System.Collections.Generic;

    public class WorkOrder<T>
    {
        public WorkOrder()
        {
            CreationOrder = new HashSet<T>();
            UpdateOrder = new HashSet<T>();
            DeletionOrder = new HashSet<T>();
        }

        public HashSet<T> CreationOrder { get; set; }

        public HashSet<T> UpdateOrder { get; set; }
        
        public HashSet<T> DeletionOrder { get; set; }

    }
}

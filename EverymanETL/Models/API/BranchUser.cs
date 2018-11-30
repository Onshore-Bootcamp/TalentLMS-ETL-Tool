namespace EverymanETL.Models.API
{
    public class BranchUser
    {
        public BranchUser(long branchUserId, long branchId, long userId)
        {
            this.BranchUserId = branchUserId;
            this.BranchId = branchId;
            this.UserId = userId;
        }

        public long BranchUserId { get; set; }

        public long BranchId { get; set; }

        public long UserId { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is BranchUser) &&
                (obj as BranchUser).BranchId == this.BranchId &&
                (obj as BranchUser).UserId == this.UserId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

namespace EverymanETL.Models.API
{
    public class GroupUser
    {
        public GroupUser(long groupUserId, long groupId, long userId)
        {
            this.GroupUserId = groupUserId;
            this.GroupId = groupId;
            this.UserId = userId;
        }

        public long GroupUserId { get; set; }

        public long GroupId { get; set; }

        public long UserId { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is GroupUser) &&
                (obj as GroupUser).GroupId == this.GroupId &&
                (obj as GroupUser).UserId == this.UserId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

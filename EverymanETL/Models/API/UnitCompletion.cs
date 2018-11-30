namespace EverymanETL.Models.API
{
    public class UnitCompletion
    {
        public long UnitCompletionId { get; set; }

        public long UserCourseDetailsId { get; set; }

        public long UserId { get; set; }

        public long CourseId { get; set; }

        public long Id { get; set; }
        
        public string Completion_Status { get; set; }

        public string Completed_On { get; set; }

        public string Completed_On_Timestamp { get; set; }

        public string Score { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is UnitCompletion) &&
                (obj as UnitCompletion).UserId == this.UserId &&
                (obj as UnitCompletion).CourseId == this.CourseId &&
                (obj as UnitCompletion).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

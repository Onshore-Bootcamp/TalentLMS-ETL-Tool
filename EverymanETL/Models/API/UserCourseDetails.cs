namespace EverymanETL.Models.API
{
    using System.Collections.Generic;

    public class UserCourseDetails
    {
        public long UserCourseDetailsId { get; set; }

        public long UserId { get; set; }

        public long CourseId { get; set; }

        public string Enrolled_On { get; set; }

        public string Enrolled_On_Timestamp { get; set; }

        public string Completion_Status { get; set; }

        public string Completion_Percentage { get; set; }

        public string Completed_On { get; set; }

        public string Completed_On_Timestamp { get; set; }

        public string Expired_On { get; set; }

        public string Expired_On_Timestamp { get; set; }

        public string Total_Time { get; set; }

        public List<UnitCompletion> Units { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is UserCourseDetails) &&
                (obj as UserCourseDetails).CourseId == this.CourseId &&
                (obj as UserCourseDetails).UserId == this.UserId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

namespace EverymanETL.Models.API
{
    public class GroupCourse
    {
        public GroupCourse(long groupCourseId, long groupId, long courseId)
        {
            this.GroupCourseId = groupCourseId;
            this.GroupId = groupId;
            this.CourseId = courseId;
        }

        public long GroupCourseId { get; set; }

        public long GroupId { get; set; }

        public long CourseId { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is GroupCourse) &&
                (obj as GroupCourse).CourseId == this.CourseId &&
                (obj as GroupCourse).GroupId == this.GroupId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

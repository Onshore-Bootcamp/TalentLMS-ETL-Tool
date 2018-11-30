namespace EverymanETL.Models.API
{
    public class BranchCourse
    {
        public BranchCourse(long branchCourseId, long branchId, long courseId)
        {
            this.BranchCourseId = branchCourseId;
            this.BranchId = branchId;
            this.CourseId = courseId;
        }

        public long BranchCourseId { get; set; }

        public long BranchId { get; set; }

        public long CourseId { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is BranchCourse) && 
                (obj as BranchCourse).BranchId == this.BranchId && 
                (obj as BranchCourse).CourseId == this.CourseId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

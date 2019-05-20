using System.Threading;

namespace OffsetStructures
{
    public class Offset
    {
        public long studentId;
        public long courseId;
        public Offset Next { get; set; }

        public Offset(long student, long course, Offset next = null)
        {
            studentId = student;
            courseId = course;
            Next = next;
        }

        public override bool Equals(object obj)
        {
            var offset = (Offset)obj;

            return (studentId == offset.studentId && courseId == offset.courseId);
        }

        public override int GetHashCode()
        {
            return ((courseId + 1) * (studentId + 1)).GetHashCode();
        }


        public static bool operator ==(Offset x, Offset y)
        {
            if ((object)x == null)
            {
                return ((object)y == null);
            }

            if ((object)y == null)
            {
                return ((object)x == null);
            }
            return (x.studentId == y.studentId && x.courseId == y.courseId);
        }

        public static bool operator !=(Offset x, Offset y)
        {
            if ((object)x == null)
            {
                return ((object)y != null);
            }

            if ((object)y == null)
            {
                return ((object)x != null);
            }
            return (x.studentId == y.studentId && x.courseId == y.courseId);
        }
    }
}
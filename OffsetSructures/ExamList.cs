using System.Threading;

namespace OffsetStructures
{
    public class ExamList : IExamSystem
    {
        public int Length { get; private set; }

        OffsetNode head;
        OffsetNode tail;

        public ExamList()
        {
            tail = new OffsetNode(new Offset(-1, -1));
            head = new OffsetNode(new Offset(-1, -1), tail);
            Length = 0;
        }


        public void Add(long studentId, long courseId)
        {
            Monitor.Enter(head);
            OffsetNode pred = head;
            Monitor.Enter(head.Next);
            OffsetNode curr = head.Next;
            while (curr != tail)
            {
                Monitor.Exit(pred);
                pred = curr;
                curr = curr.Next;
                Monitor.Enter(curr);
            }
            curr = new OffsetNode(new Offset(studentId, courseId), tail);
            pred.Next = curr;
            Length++;
            Monitor.Exit(pred);
            Monitor.Exit(tail);
        }

        public void Remove(long studentId, long courseId)
        {
            Monitor.Enter(head);
            OffsetNode pred = head;
            Monitor.Enter(head.Next);
            OffsetNode curr = head.Next;
            while (curr != tail || curr.Value != new Offset(studentId,courseId))
            {
                Monitor.Exit(pred);
                pred = curr;
                curr = curr.Next;
                Monitor.Enter(curr);
            }
            if (curr == tail)
            {
                Monitor.Exit(pred);
                Monitor.Exit(tail);
            }
            else
            {
                pred.Next = curr.Next;
                Length--;
                Monitor.Exit(pred);
                Monitor.Exit(curr);
            }
        }

        public bool Contains(long studentId, long courseId)
        {
            Monitor.Enter(head);
            OffsetNode pred = head;
            Monitor.Enter(head.Next);
            OffsetNode curr = head.Next;
            while (curr != tail || curr.Value != new Offset(studentId, courseId))
            {
                Monitor.Exit(pred);
                pred = curr;
                curr = curr.Next;
                Monitor.Enter(curr);
            }
            if (curr == tail)
            {
                Monitor.Exit(pred);
                Monitor.Exit(tail);
                return false;
            }
            else
            {
                Monitor.Exit(pred);
                Monitor.Exit(curr);
                return true;
            }
        }

        public int EnumerateElems()
        {
            int result = 0;
            OffsetNode tmp = head.Next;
            while (tmp != tail)
            {
                result++;
                tmp = tmp.Next;
            }
            return result;
        }
    }
}

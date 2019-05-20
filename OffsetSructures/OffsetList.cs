namespace OffsetStructures
{
    public class OffsetList
    {
        public int Length { get; private set; }

        private Offset _first;
        
        public OffsetList()
        {
            _first = null;
            Length = 0;
        }

        public void Add(Offset value)
        {
            _first = new Offset(value.studentId, value.courseId, _first);
            Length++;
        }

        public bool Remove(Offset value)
        {
            if ((_first != null) && (_first == value))
            {
                _first = _first.Next;
                Length--;
                return true;
            }

            Offset temp = _first;
            while (temp != null)
            {
                if ((temp.Next != null) && (temp.Next == value))
                {
                    temp.Next = temp.Next.Next;
                    Length--;
                    return true;
                }

                temp = temp.Next;
            }
            return false;
        }

        public bool Contains(Offset value)
        {
            Offset temp = _first;
            while (temp != null)
            {
                if (temp == value)
                {
                    return true;
                }

                temp = temp.Next;
            }

            return false;
        }
        
    }

}
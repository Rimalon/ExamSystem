namespace OffsetStructures
{
    public class OffsetNode
    {
        public Offset Value { get; set; }
        public OffsetNode Next { get; set; }


        public OffsetNode(Offset value, OffsetNode next = null)
        {
            Value = value;
            Next = next;
        }
    }
}

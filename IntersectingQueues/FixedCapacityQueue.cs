using System.Collections.Generic;

namespace IntersectingQueues
{
    public class FixedCapacityQueue<T> where T : struct
    {
        private readonly Queue<T> _queue;
        public FixedCapacityQueue(int capacity)
        {
            _queue = new Queue<T>(capacity);
            Capacity = capacity;
        }

        public int Capacity { get; }

        public T? Push(T newElement)
        {
            T? result = _queue.Count == Capacity
                ? (T?)_queue.Dequeue()
                : null;
            _queue.Enqueue(newElement);
            return result;
        }
    }
}

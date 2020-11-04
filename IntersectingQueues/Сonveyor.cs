using System;
using System.Collections.Generic;

namespace IntersectingQueues
{
    public class Сonveyor
    {
        private const int IntersectionLength = 1;
        private readonly List<FixedCapacityQueue<int>> _compositeQueueA;
        private readonly List<FixedCapacityQueue<int>> _compositeQueueB;
        public Сonveyor(int lineLength, int intersectionsCount)
        {
            if (intersectionsCount < 1)
            {
                throw new ArgumentException("Intersections count must be greater or equal to 1.");
            }
            if (lineLength < 1)
            {
                throw new ArgumentException("Line length must be greater or equal to 1.");
            }
            if (lineLength < intersectionsCount)
            {
                throw new ArgumentException("Line length must be greater or equal to intersections count.");
            }

            var sectionLength = (lineLength - intersectionsCount * IntersectionLength) / intersectionsCount;
            var tailLength = lineLength - (sectionLength * (intersectionsCount - 1) + intersectionsCount * IntersectionLength);

            _compositeQueueA = new List<FixedCapacityQueue<int>>();
            _compositeQueueB = new List<FixedCapacityQueue<int>>();
            if (tailLength != 0)
            {
                _compositeQueueA.Add(new FixedCapacityQueue<int>(tailLength));
                _compositeQueueB.Add(new FixedCapacityQueue<int>(tailLength));
            }
            var intersectionPoint = new FixedCapacityQueue<int>(IntersectionLength);
            _compositeQueueA.Add(intersectionPoint);
            _compositeQueueB.Add(intersectionPoint);

            for (int i = tailLength + IntersectionLength; i < lineLength; i += sectionLength + IntersectionLength)
            {
                if (sectionLength != 0)
                {
                    _compositeQueueA.Add(new FixedCapacityQueue<int>(sectionLength));
                    _compositeQueueB.Add(new FixedCapacityQueue<int>(sectionLength));
                }
                var intersection = new FixedCapacityQueue<int>(IntersectionLength);
                _compositeQueueA.Add(intersection);
                _compositeQueueB.Add(intersection);
            }
        }

        public int? PushA(int newElement)
        {
            return PushElementToQueue(newElement, _compositeQueueA);
        }

        public int? PushB(int newElement)
        {
            return PushElementToQueue(newElement, _compositeQueueB);
        }

        private int? PushElementToQueue(int newElement, List<FixedCapacityQueue<int>> compositeQueue)
        {
            int? result = newElement;
            foreach (var queue in compositeQueue)
            {
                result = queue.Push((int)result);
                if (result == null)
                {
                    break;
                }
            }
            return result;
        }
    }
}

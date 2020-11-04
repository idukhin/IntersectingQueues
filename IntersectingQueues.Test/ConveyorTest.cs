using Xunit;

namespace IntersectingQueues.Test
{
    public class ConveyorTest
    {
        [Fact]
        public void SamplePushing()
        {
            int lineLength = 10;
            int intersectionsCount = 3;
            var conveyor = new Ñonveyor(lineLength, intersectionsCount);
            for (int i = 0; i < lineLength; i++)
            {
                conveyor.PushA(i);
            }
            for (int i = 0; i < lineLength - intersectionsCount; i++)
            {
                conveyor.PushB(-i);
            }
            var res = conveyor.PushB(999);
            Assert.Equal(0, res);
        }
    }
}

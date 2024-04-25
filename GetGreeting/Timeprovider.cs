namespace GetGreeting
{
    public class Timeprovider : ITimeProvider
    {
        public DateTime GetCurrentTime()
        {
            throw new NotImplementedException();
        }

    }

    public class FakeTimeProvideer : ITimeProvider
    {
        private DateTime _fakeTime;

        public FakeTimeProvideer(DateTime fakeTime)
        {
            _fakeTime = fakeTime;
        }

        public DateTime GetCurrentTime()
        {
            return _fakeTime;
        }
    }
}



namespace GetGreeting
{

    public class GreetingProvider
    {

        private ITimeProvider _timeProvider;

        public GreetingProvider(ITimeProvider provider)
        {
            _timeProvider = provider;
        }

        public string GetGreeting()
        {
            var hour = _timeProvider.GetCurrentTime().Hour; // changed from DateTime.Now.Hour to _timeProvider.GetCurrentTime()

            if (hour >= 5 && hour < 12)
            {
                return "Good morning!";
            }
            else if (hour >= 12 && hour < 18)
            {
                return "Good afternoon!";
            }
            else if (hour >= 18 && hour < 22)
            {
                return "Good evening!";
            }
            else
            {
                return "Good night!";
            }
        }
    }

}

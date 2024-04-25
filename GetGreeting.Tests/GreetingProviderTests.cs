using Moq;
using GetGreeting;

namespace GetGreeting.Tests
{
    public class GreetingProviderTests
    {

        private GreetingProvider _greetingProvider;
        private Mock<ITimeProvider> _mockedTimeProvider;

        [SetUp]
        public void Setup()
        {
            _mockedTimeProvider = new Mock<ITimeProvider>();
            _greetingProvider = new GreetingProvider(_mockedTimeProvider.Object);
        }

        [Test]
        public void GetGreeting_ShouldReturn_AGoodMorningMessage_WhenItIsMorning()
        {
            _mockedTimeProvider.Setup(x => x.GetCurrentTime()).Returns(new DateTime(2000, 2, 2, 11, 9, 9));
            var result = _greetingProvider.GetGreeting();                            //YY:MM:DD:HH:MM:SS
            Assert.That(result, Is.EqualTo("Good morning!"));
        }

        [Test]
        public void GetGreeting_ShouldReturn_AGoodAfternoonMessage_WhenItIsAfternoon()
        {
            _mockedTimeProvider.Setup(x => x.GetCurrentTime()).Returns(new DateTime(2000, 2, 2, 12, 10, 10));
            var result = _greetingProvider.GetGreeting();                            //YY:MM:DD:HH:MM:SS
            Assert.That(result, Is.EqualTo("Good afternoon!"));
        }                                                                           

        [Test]
        public void GetGreeting_ShouldReturn_AGoodEveningMessage_WhenItIsEvening()
        {
            _mockedTimeProvider.Setup(x => x.GetCurrentTime()).Returns(new DateTime(2000, 2, 2, 18, 9, 9));
            var result = _greetingProvider.GetGreeting();                            //YY:MM:DD:HH:MM:SS
            Assert.That(result, Is.EqualTo("Good evening!"));
        }

        [Test]

        public void GetGreeting_ShouldReturn_AGoodNightMessage_WhenItIsNight()
        {
            _mockedTimeProvider.Setup(x => x.GetCurrentTime()).Returns(new DateTime(2000, 2, 2, 22, 9, 9));
            var result = _greetingProvider.GetGreeting();                            //YY:MM:DD:HH:MM:SS
            Assert.That(result, Is.EqualTo("Good night!"));
        }
    }
}
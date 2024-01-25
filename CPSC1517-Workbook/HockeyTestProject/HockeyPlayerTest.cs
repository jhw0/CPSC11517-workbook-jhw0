using FluentAssertions;
using Hockey.Data;

namespace HockeyTestProject
{
    public class HockeyPlayerTest
    {
        //[Fact]
        //public void HockeyPlayer_DefaultConstructor_CreatesGoodPlayer()
        //{
        //	// arrange
        //	HockeyPlayer player;

        //	// act
        //	player = new HockeyPlayer();

        //	//assert
        //	player.Should().BeOfType<HockeyPlayer>();
        //}

        // Constants for test HockeyPlayer
        // DOB is separated because DateOnly cannot be a constant and we need to use consts
        const string FirstName = "Connor";
        const string LastName = "Brown";
        const string BirthPlace = "Toronto, ON, CAN";
        const int BirthYear = 1994;
        const int BirthMonth = 01;
        const int BirthDay = 14;
        const int HeightInInches = 72;
        const int WeightInPounds = 188;
        const int JerseyNumber = 28;
        const Position PlayerPosition = Position.Center;
        const Shot PlayerShot = Shot.Left;
        // The following relies on our being correct here - not writing a test for the test expected value
        // cant be const because age keeps changing by the day
        // .DayNumber tells u exact number of days from that date
        readonly int Age = (DateOnly.FromDateTime(DateTime.Now).DayNumber - new DateOnly(BirthYear, BirthMonth, BirthDay).DayNumber) / 365;

        // Can quickly run a test to check our method for AGE above
        //[Fact]
        //public void AGE_Is_Correct()
        //{
        //	Age.Should().Be(30);
        //}

        /// <summary>
        /// Creates a default HockeyPlayer for testing
        /// </summary>
        /// <returns>A new HockeyPlayer</returns>
        public HockeyPlayer CreateTestHockeyPlayer()
        {
            return new HockeyPlayer(FirstName, LastName, BirthPlace, new DateOnly(BirthYear, BirthMonth, BirthDay), WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot);
        }

        // Good HockeyPlayer constructor
        [Theory]
        [InlineData(FirstName, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot)]
        public void HockeyPlayer_GreedyConstructor_ReturnsHockeyPlayer(string firstName, string lastName, string birthPlace,
            int birthYear, int birthMonth, int birthDay, int weightInPounds, int heightInInches, int jerseyNumber, Position position, Shot shot)
        {
            HockeyPlayer actual;

            actual = new HockeyPlayer(firstName, lastName, birthPlace, new DateOnly(birthYear, birthMonth, birthDay), weightInPounds, heightInInches, jerseyNumber, position, shot);

            actual.Should().NotBeNull();
        }

        // Failing HockeyPlayer constructor - TODO: create InlineData lines for remaining properties
        [Theory]
        [InlineData("", LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
        [InlineData(" ", LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
        [InlineData(null, LastName, BirthPlace, BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches, JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be null or empty.")]
        public void HockeyPlayer_GreedyConstructor_ThrowsException(string firstName, string lastName, string birthPlace,
            int birthYear, int birthMonth, int birthDay, int weightInPounds, int heightInInches, int jerseyNumber, Position position, Shot shot, string errMsg)
        {

            // Arrange ---- wrap call inside a method to workaround error
            Action act = () => new HockeyPlayer(firstName, lastName, birthPlace, new DateOnly(birthYear, birthMonth, birthDay), weightInPounds, heightInInches, jerseyNumber, position, shot);
            // we want to test this method by passing the above parameters into this method

            // Act/Assert ----- call the method, it should throw an exception with a particular msg, since we passed empty, null or whitespace into one of the paramters
            act.Should().Throw<ArgumentException>().WithMessage(errMsg);
        }


        //used to quick tesst the age calculation
        //[Fact]
        //public void Age_IsCorrect()
        //{
        //	Age.Should().Be(30);
        //}

        [Fact]
        public void HockeyPlayer_Age_ReturnsCorrectAge()
        {
            // Arrange 
            HockeyPlayer player = CreateTestHockeyPlayer(); //uses the default test constructor

            // Act
            int actual = player.Age;

            // Assert
            actual.Should().Be(Age); //or actual = Age;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(98)]
        public void HockeyPlayer_JerseyNumber_GoodSetAndGet(int expected)
        {
            //arrange
            HockeyPlayer player = CreateTestHockeyPlayer();

            //act
            player.JerseyNumber = expected;
            int actual = player.JerseyNumber;

            //assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(99)]
        public void HockeyPlayer_JerseyNumber_BadSetAndThrows(int value)
        {
            HockeyPlayer player = CreateTestHockeyPlayer();

            Action act = () => player.JerseyNumber = value;

            act.Should().Throw<ArgumentException>();
        }

    }
}
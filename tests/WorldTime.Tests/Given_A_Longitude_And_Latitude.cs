using System;
using Xunit;
using Shouldly;
using WorldTime.Calculations;

namespace WorldTime.Tests
{

    public class Given_A_Longitude_And_Latitude
    {
        [Theory]
        [InlineData(51.27904, 1.07992,0)] // Canterbury
        [InlineData(48.20849, 16.37208,1)] //Vienna
        [InlineData(35.6895, 139.69171, 9)] // Tokyo
        [InlineData(53.33306, -6.24889, 0)] // Dublin
        [InlineData(38.89511, -77.03637, -5)] // Washington DC
        public void When_GetTime_Then_Return_Correct_Time(double latitude, double longitude, int expectedOffset)
        {
            DateTime asOf = new DateTime(2020,1,1, 7,24, 0);

            var calculator = new GmtOffsetApproximator();

            var offset = calculator.GetOffset(new WorldTime.Calculations.Point(){Latitude = latitude, Longitude = longitude});

            offset.ShouldBe(expectedOffset);
        }
    }
}

using System.Threading.Tasks;
using FluentAssertions;
using NEL.LibPostalClient.Models.Brokers.LibPostal;
using Xunit;

namespace NEL.LibPostalClient.Tests.Acceptance
{
    public partial class LibPostalTest
    {
        [Fact]
        public async Task ShouldExpandAddressAsync()
        {
            //Given
            string inputAddress = "10 Downing Str, Westminster, London, SW1A2AA, UK";
            string[] expectedResult = { "10 downing str westminster london sw1a2aa uk", 
                "10 downing str westminster london sw 1a2aa uk" };

            //When
            string[] actualResult = await this.libPostalClient.ExpandAddressAsync(inputAddress);

            //Then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

    }
}

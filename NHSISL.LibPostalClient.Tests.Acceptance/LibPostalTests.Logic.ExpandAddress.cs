// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Acceptance
{
    public partial class LibPostalTest
    {
        [Fact]
        public async Task ShouldExpandAddressAsync()
        {
            //Given
            string inputAddress = "10 Downing Str, Westminster, London, SW1A2AA, UK";
            string[] expectedResult =
                {
                    "10 downing street westminster london sw1a2aa uk",
                    "10 downing strata unit westminster london sw1a2aa uk",
                    "10 downing street westminster london southwest 1a2aa uk",
                    "10 downing street westminster london sw 1a2aa uk",
                    "10 downing strata unit westminster london southwest 1a2aa uk",
                    "10 downing strata unit westminster london sw 1a2aa uk"
                };

            //When
            string[] actualResult = await this.libPostalClient.ExpandAddressAsync(inputAddress);

            //Then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

    }
}

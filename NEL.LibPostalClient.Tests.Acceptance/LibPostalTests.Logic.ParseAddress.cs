// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NEL.LibPostalClient.Tests.Acceptance
{
    public partial class LibPostalTest
    {
        [Fact]
        public async Task ShouldParseAddressAsync()
        {
            //Given
            string inputAddress = "10 downing str westminster london sw1a2aa uk";
            List<KeyValuePair<string, string>> expectedResult = new List<KeyValuePair<string, string>>();

            //expectedResult


            //string[] expectedResult = { "10 downing str westminster london sw1a2aa uk", 
            //    "10 downing str westminster london sw 1a2aa uk" };

            //When
            List<KeyValuePair<string, string>> actualResult =
                await this.libPostalClient.ParseAddressAsync(inputAddress);

            //Then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

    }
}

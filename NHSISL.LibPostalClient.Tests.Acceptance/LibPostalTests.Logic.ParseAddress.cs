// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Acceptance
{
    public partial class LibPostalTest
    {
        [Fact]
        public async Task ShouldParseAddressAsync()
        {
            //Given
            string inputAddress = "10 downing str westminster london sw1a2aa uk";

            List<KeyValuePair<string, string>> expectedResult = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("house_number", "10"),
                new KeyValuePair<string, string>("road", "downing str"),
                new KeyValuePair<string, string>("city_district", "westminster"),
                new KeyValuePair<string, string>("city", "london"),
                new KeyValuePair<string, string>("postcode", "sw1a2aa"),
                new KeyValuePair<string, string>("country", "uk")
            };

            //When
            List<KeyValuePair<string, string>> actualResult =
                await this.libPostalClient.ParseAddressAsync(inputAddress);

            //Then
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

    }
}

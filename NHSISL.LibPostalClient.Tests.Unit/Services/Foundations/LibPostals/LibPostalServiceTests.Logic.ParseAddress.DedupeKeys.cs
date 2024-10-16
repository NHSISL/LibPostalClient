// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Fact]
        public void ShouldDedupeKeys()
        {
            // given
            List<KeyValuePair<string, string>> inputList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("house_number", "7"),
                new KeyValuePair<string, string>("house", "finley court"),
                new KeyValuePair<string, string>("house_number", "168"),
                new KeyValuePair<string, string>("road", "foxley lane"),
                new KeyValuePair<string, string>("city", "purley"),
                new KeyValuePair<string, string>("postcode", "cr8 3nf"),
            };

            List<KeyValuePair<string, string>> expectedList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("house_number_1", "7"),
                new KeyValuePair<string, string>("house", "finley court"),
                new KeyValuePair<string, string>("house_number_2", "168"),
                new KeyValuePair<string, string>("road", "foxley lane"),
                new KeyValuePair<string, string>("city", "purley"),
                new KeyValuePair<string, string>("postcode", "cr8 3nf"),
            };

            // when
            List<KeyValuePair<string, string>> actualList
                = this.libPostalService.DeduplicateKeys(inputList);

            // then
            actualList.Should().BeEquivalentTo(expectedList);
        }



    }
}

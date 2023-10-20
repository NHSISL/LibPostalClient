// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace NEL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Fact]
        public async Task ShouldParseAddressAsync()
        {
            // given
            string inputAddress = GetRandomString();

            List<KeyValuePair<string, string>> outputParsedAddress =
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( GetRandomString(), GetRandomString()),
                };

            List<KeyValuePair<string, string>> expectedParsedAddress =
                outputParsedAddress.DeepClone();

            this.libPostalServiceBrokerMock.Setup(broker =>
               broker.ParseAddress(inputAddress))
                   .Returns(outputParsedAddress);

            // when
            List<KeyValuePair<string, string>> actualParsedAddress
                = await this.libPostalService.ParseAddressAsync(inputAddress);

            // then
            actualParsedAddress.Should().BeEquivalentTo(expectedParsedAddress);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ParseAddress(inputAddress),
                    Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

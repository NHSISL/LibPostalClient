// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Fact]
        public async Task ShouldExpandAddressAsync()
        {
            // given
            string randomAddress = GetRandomString();
            string[] inputAddress = new string[] { randomAddress };
            string[] storageAddress = inputAddress;
            string[] expectedAddresses = storageAddress.DeepClone();

            this.libPostalServiceBrokerMock.Setup(broker =>
               broker.ExpandAddress(randomAddress))
                   .Returns(inputAddress);

            // when
            string[] actualAddress = await this.libPostalService.ExpandAddressAsync(randomAddress);

            // then
            actualAddress.Should().BeEquivalentTo(expectedAddresses);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ExpandAddress(randomAddress),
                    Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

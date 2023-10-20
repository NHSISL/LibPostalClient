// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

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
        public void ShouldExpandAddressAsync()
        {
            // given
            string randomAddress = GetRandomString();
            string[] inputAddress = new string[] { randomAddress };
            string[] storageAddress = inputAddress;
            string expectedAddress = storageAddress[0].DeepClone();

            this.libPostalServiceBrokerMock.Setup(broker =>
               broker.ExpandAddress(randomAddress))
                   .Returns(inputAddress);

            // when
            string[] actualAddress = await this.libPostalService.ExpandAddressAsync(randomAddress);

            // then
            actualAddress.Result[0].Should().BeEquivalentTo(expectedAddress);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ExpandAddress(randomAddress),
                    Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using Xunit;

namespace NEL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddressExpandIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();
            string randomAddress = GetRandomString();

            var failedAddressServiceException =
                new FailedAddressServiceException(
                    message: "Failed address service occurred, please contact support",
                    innerException: serviceException);

            var expectedAddressServiceException =
                new AddressServiceException(
                    message: "Address service error occurred, contact support.",
                    innerException: failedAddressServiceException);

            this.libPostalServiceBrokerMock.Setup(broker =>
              broker.ExpandAddress(randomAddress))
                  .Throws(serviceException);

            // when
            ValueTask<string[]> expandAddressTask =
                 this.libPostalService.ExpandAddress(randomAddress);

            AddressServiceException actualAddressServiceException =
                await Assert.ThrowsAsync<AddressServiceException>(
                    expandAddressTask.AsTask);

            // then
            actualAddressServiceException.Should()
                .BeEquivalentTo(expectedAddressServiceException);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ExpandAddress(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAddressServiceException))),
                        Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}

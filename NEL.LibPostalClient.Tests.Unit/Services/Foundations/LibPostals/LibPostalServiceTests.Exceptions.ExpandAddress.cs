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

            var failedServiceException =
                new FailedServiceException(
                    message: "Failed service occurred, please contact support",
                    innerException: serviceException);

            var expectedAddressServiceException =
                new LibPostalServiceException(
                    message: "Lib Postal service error occurred, contact support.",
                    innerException: failedServiceException);

            this.libPostalServiceBrokerMock.Setup(broker =>
              broker.ExpandAddress(randomAddress))
                  .Throws(serviceException);

            // when
            ValueTask<string[]> expandAddressTask =
                 this.libPostalService.ExpandAddress(randomAddress);

            LibPostalServiceException actualAddressServiceException =
                await Assert.ThrowsAsync<LibPostalServiceException>(
                    expandAddressTask.AsTask);

            // then
            actualAddressServiceException.Should()
                .BeEquivalentTo(expectedAddressServiceException);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ExpandAddress(It.IsAny<string>()),
                    Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

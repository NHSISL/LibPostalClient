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

            var failedLibPostalServiceException =
                new FailedLibPostalServiceException(
                    message: "Failed Lib Postal service occurred, please contact support",
                    innerException: serviceException);

            var expectedLibPostalServiceException =
                new LibPostalServiceException(
                    message: "Lib Postal service error occurred, contact support.",
                    innerException: failedLibPostalServiceException);

            this.libPostalServiceBrokerMock.Setup(broker =>
              broker.ExpandAddress(randomAddress))
                  .Throws(serviceException);

            // when
            ValueTask<string[]> expandAddressTask =
                 this.libPostalService.ExpandAddress(randomAddress);

            LibPostalServiceException actualLibPostalsServiceException =
                await Assert.ThrowsAsync<LibPostalServiceException>(
                    expandAddressTask.AsTask);

            // then
            actualLibPostalsServiceException.Should()
                .BeEquivalentTo(expectedLibPostalServiceException);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ExpandAddress(It.IsAny<string>()),
                    Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

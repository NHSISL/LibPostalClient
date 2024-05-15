// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Fact]
        public async Task ShouldThrowServiceExceptionOnParseAddressIfServiceErrorOccursAndLogItAsync()
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
              broker.ParseAddress(randomAddress))
                  .Throws(serviceException);

            // when
            ValueTask<List<KeyValuePair<string, string>>> parseAddressTask =
                 this.libPostalService.ParseAddressAsync(randomAddress);

            LibPostalServiceException actualLibPostalsServiceException =
                await Assert.ThrowsAsync<LibPostalServiceException>(
                    parseAddressTask.AsTask);

            // then
            actualLibPostalsServiceException.Should()
                .BeEquivalentTo(expectedLibPostalServiceException);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ParseAddress(It.IsAny<string>()),
                    Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

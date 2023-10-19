// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using NEL.MESH.Models.Foundations.LibPostal.Exceptions;
using Xunit;

namespace NEL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnExpandIfAddressIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            string invalidAddress = invalidText;

            var invalidAddressArgumentException =
                new InvalidAddressArgumentException(
                    message: "Invalid address argument. Please correct the errors and try again.");

            invalidAddressArgumentException.AddData(
                key: "address",
                values: "Text is required");

            var expectedAddressValidationException =
                new LibPostalValidationException(
                    message: "Address validation errors occurred, please try again.",
                    innerException: invalidAddressArgumentException);

            // when
            ValueTask<string[]> expandAddressTask = this.libPostalService.ExpandAddress(invalidAddress);

            LibPostalValidationException actualAddressValidationException =
                await Assert.ThrowsAsync<LibPostalValidationException>(() =>
                    expandAddressTask.AsTask());

            // then
            actualAddressValidationException.Should()
                .BeEquivalentTo(expectedAddressValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAddressValidationException))),
                        Times.Once);

            this.libPostalServiceBrokerMock.Verify(broker =>
               broker.ExpandAddress(It.IsAny<string>()),
                   Times.Never);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

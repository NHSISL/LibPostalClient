// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using NHSISL.MESH.Models.Foundations.LibPostal.Exceptions;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnExpandIfArgumentIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            string invalidAddress = invalidText;

            var invalidArgumentLibPostalException =
                new InvalidArgumentLibPostalException(
                    message: "Invalid Lib Postal argument. Please correct the errors and try again.");

            invalidArgumentLibPostalException.AddData(
                key: "address",
                values: "Text is required");

            var expectedLibPostalValidationException =
                new LibPostalValidationException(
                    message: "Lib Postal validation errors occurred, please try again.",
                    innerException: invalidArgumentLibPostalException);

            // when
            ValueTask<string[]> expandAddressTask = this.libPostalService.ExpandAddressAsync(invalidAddress);

            LibPostalValidationException actualLibPostalValidationException =
                await Assert.ThrowsAsync<LibPostalValidationException>(() =>
                    expandAddressTask.AsTask());

            // then
            actualLibPostalValidationException.Should()
                .BeEquivalentTo(expectedLibPostalValidationException);

            this.libPostalServiceBrokerMock.Verify(broker =>
               broker.ExpandAddress(invalidAddress),
                   Times.Never);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

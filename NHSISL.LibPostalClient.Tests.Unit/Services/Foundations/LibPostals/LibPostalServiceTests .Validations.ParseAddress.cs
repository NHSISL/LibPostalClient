// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
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
        public async Task ShouldThrowValidationExceptionOnParseAddressIfArgumentIsInvalidAndLogItAsync(
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
            ValueTask<List<KeyValuePair<string, string>>> parseAddressTask =
                this.libPostalService.ParseAddressAsync(invalidAddress);

            LibPostalValidationException actualLibPostalValidationException =
                await Assert.ThrowsAsync<LibPostalValidationException>(() =>
                    parseAddressTask.AsTask());

            // then
            actualLibPostalValidationException.Should()
                .BeEquivalentTo(expectedLibPostalValidationException);

            this.libPostalServiceBrokerMock.Verify(broker =>
               broker.ParseAddress(invalidAddress),
                   Times.Never);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}

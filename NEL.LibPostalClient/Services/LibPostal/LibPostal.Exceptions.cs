// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using NEL.MESH.Models.Foundations.LibPostal.Exceptions;
using Xeptions;

namespace NEL.LibPostalClient.Services.LibPostal
{
    public partial class LibPostalService
    {
        private delegate ValueTask<string[]> ReturningLibPostalAddressFunction();

        private async ValueTask<string[]> TryCatch(
           ReturningLibPostalAddressFunction returningLibPostalAddressFunction)
        {
            try
            {
                return await returningLibPostalAddressFunction();
            }
            catch (InvalidAddressArgumentException invalidAddressArgumentException)
            {
                throw CreateAndLogValidationException(invalidAddressArgumentException);
            }
            catch (Exception exception)
            {
                var failedAddressServiceException =
                    new FailedAddressServiceException(
                        message: "Failed address service occurred, please contact support",
                        innerException: exception);

                throw CreateAndLogServiceException(failedAddressServiceException);

            }
        }

        private LibPostalValidationException CreateAndLogValidationException(Xeption exception)
        {
            var libPostalValidationException =
                new LibPostalValidationException(
                    message: "Address validation errors occurred, please try again.",
                    innerException: exception);

            this.loggingBroker.LogError(libPostalValidationException);

            return libPostalValidationException;
        }

        private AddressServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var addressServiceException =
                new AddressServiceException(
                    message: "Address service error occurred, contact support.",
                    innerException: exception);

            this.loggingBroker.LogError(addressServiceException);

            return addressServiceException;
        }
    }
}

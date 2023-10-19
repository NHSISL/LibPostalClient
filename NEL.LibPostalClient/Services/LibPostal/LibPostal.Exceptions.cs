// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
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
            catch (Exception exception)
            {
                var failedAddressServiceException =
                    new FailedAddressServiceException(
                        message: "Failed address service occurred, please contact support",
                        innerException: exception);

                throw CreateAndLogServiceException(failedAddressServiceException);

            }
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

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using NHSISL.MESH.Models.Foundations.LibPostal.Exceptions;
using Xeptions;

namespace NHSISL.LibPostalClient.Services.LibPostal
{
    internal partial class LibPostalService
    {
        private delegate ValueTask<string[]> ReturningLibPostalAddressFunction();
        private delegate ValueTask<List<KeyValuePair<string, string>>> ReturningLibPostalParseAddressFunction();

        private async ValueTask<string[]> TryCatch(
           ReturningLibPostalAddressFunction returningLibPostalAddressFunction)
        {
            try
            {
                return await returningLibPostalAddressFunction();
            }
            catch (InvalidArgumentLibPostalException invalidArgumentLibPostalException)
            {
                throw CreateAndLogValidationException(invalidArgumentLibPostalException);
            }
            catch (Exception exception)
            {
                var failedLibPostalServiceException =
                    new FailedLibPostalServiceException(
                        message: "Failed Lib Postal service occurred, please contact support",
                        innerException: exception);

                throw CreateAndLogServiceException(failedLibPostalServiceException);
            }
        }

        private async ValueTask<List<KeyValuePair<string, string>>> TryCatch(
           ReturningLibPostalParseAddressFunction returningLibPostalParseAddressFunction)
        {
            try
            {
                return await returningLibPostalParseAddressFunction();
            }
            catch (InvalidArgumentLibPostalException invalidArgumentLibPostalException)
            {
                throw CreateAndLogValidationException(invalidArgumentLibPostalException);
            }
            catch (Exception exception)
            {
                var failedLibPostalServiceException =
                    new FailedLibPostalServiceException(
                        message: "Failed Lib Postal service occurred, please contact support",
                        innerException: exception);

                throw CreateAndLogServiceException(failedLibPostalServiceException);
            }
        }

        private LibPostalValidationException CreateAndLogValidationException(Xeption exception)
        {
            var libPostalValidationException =
                new LibPostalValidationException(
                    message: "Lib Postal validation errors occurred, please try again.",
                    innerException: exception);

            return libPostalValidationException;
        }

        private LibPostalServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var libPostalServiceException =
                new LibPostalServiceException(
                    message: "Lib Postal service error occurred, contact support.",
                    innerException: exception);

            return libPostalServiceException;
        }
    }
}

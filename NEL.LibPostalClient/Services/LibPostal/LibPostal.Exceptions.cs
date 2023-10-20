﻿// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using NEL.MESH.Models.Foundations.LibPostal.Exceptions;
using Xeptions;

namespace NEL.LibPostalClient.Services.LibPostal
{
    internal partial class LibPostalService
    {
        private delegate ValueTask<string[]> ReturningLibPostalAddressFunction();

        private async ValueTask<string[]> TryCatch(
           ReturningLibPostalAddressFunction returningLibPostalAddressFunction)
        {
            try
            {
                return await returningLibPostalAddressFunction();
            }
            catch (InvalidArgumentException invalidAddressArgumentException)
            {
                throw CreateAndLogValidationException(invalidAddressArgumentException);
            }
            catch (Exception exception)
            {
                var failedAddressServiceException =
                    new FailedServiceException(
                        message: "Failed service occurred, please contact support",
                        innerException: exception);

                throw CreateAndLogServiceException(failedAddressServiceException);

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

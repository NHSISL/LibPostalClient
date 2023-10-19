// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

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


    }
}

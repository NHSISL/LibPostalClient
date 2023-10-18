// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.LibPostalClient.Models.Clients.LibPostal.Exceptions
{
    public class LibPostalClientValidationException : Xeption
    {
        public LibPostalClientValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}

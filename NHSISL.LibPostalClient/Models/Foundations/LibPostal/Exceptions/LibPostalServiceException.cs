// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    internal class LibPostalServiceException : Xeption
    {
        public LibPostalServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}

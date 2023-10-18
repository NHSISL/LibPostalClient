// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.LibPostalClient.Models.Clients.LibPostal.Exceptions
{
    public class LibPostalClientDependencyException : Xeption
    {
        public LibPostalClientDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}

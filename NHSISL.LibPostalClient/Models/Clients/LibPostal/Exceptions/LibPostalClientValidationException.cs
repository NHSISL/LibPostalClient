// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace NHSISL.LibPostalClient.Models.Clients.LibPostal.Exceptions
{
    public class LibPostalClientValidationException : Xeption
    {
        public LibPostalClientValidationException(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}

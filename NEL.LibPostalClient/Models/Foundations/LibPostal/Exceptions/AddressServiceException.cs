// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    internal class AddressServiceException : Xeption
    {
        public AddressServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

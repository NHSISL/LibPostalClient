// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    internal class FailedAddressServiceException : Xeption
    {
        public FailedAddressServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

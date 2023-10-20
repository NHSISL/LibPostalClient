// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    public class FailedServiceException : Xeption
    {
        public FailedServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

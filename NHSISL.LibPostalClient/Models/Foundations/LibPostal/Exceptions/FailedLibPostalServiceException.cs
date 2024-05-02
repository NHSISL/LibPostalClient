// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    public class FailedLibPostalServiceException : Xeption
    {
        public FailedLibPostalServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

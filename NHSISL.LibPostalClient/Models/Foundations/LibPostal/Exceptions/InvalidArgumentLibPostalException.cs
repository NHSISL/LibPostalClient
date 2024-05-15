// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    public class InvalidArgumentLibPostalException : Xeption
    {
        public InvalidArgumentLibPostalException(string message)
            : base(message)
        { }
    }
}

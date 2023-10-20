// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    public class InvalidAddressLibPostalException : Xeption
    {
        public InvalidAddressLibPostalException(string message)
            : base(message)
        { }
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    public class InvalidAddressArgumentException : Xeption
    {
        public InvalidAddressArgumentException(string message)
            : base(message)
        { }
    }
}

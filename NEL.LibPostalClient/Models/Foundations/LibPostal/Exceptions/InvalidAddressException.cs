// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    public class InvalidAddressException : Xeption
    {
        public InvalidAddressException(string message)
            : base(message)
        { }
    }
}

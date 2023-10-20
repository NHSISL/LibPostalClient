// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    public class InvalidArgumentException : Xeption
    {
        public InvalidArgumentException(string message)
            : base(message)
        { }
    }
}

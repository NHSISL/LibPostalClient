// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.MESH.Models.Foundations.LibPostal.Exceptions
{
    internal class LibPostalValidationException : Xeption
    {
        public LibPostalValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}

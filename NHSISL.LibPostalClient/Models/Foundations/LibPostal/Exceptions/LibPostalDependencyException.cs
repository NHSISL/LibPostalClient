﻿// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions
{
    internal class LibPostalDependencyException : Xeption
    {
        public LibPostalDependencyException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}

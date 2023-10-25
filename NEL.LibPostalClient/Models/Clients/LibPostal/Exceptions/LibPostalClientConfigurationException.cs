// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using Xeptions;

namespace NEL.LibPostalClient.Models.Clients.LibPostal.Exceptions
{
    public class LibPostalClientConfigurationException : Xeption
    {
        public LibPostalClientConfigurationException(string message)
            : base(message)
        { }
    }
}

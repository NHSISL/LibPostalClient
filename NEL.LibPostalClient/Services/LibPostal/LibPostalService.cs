// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using NEL.LibPostalClient.Brokers.LibPostal;

namespace NEL.LibPostalClient.Services.LibPostal
{
    internal partial class LibPostalService : ILibPostalService
    {
        private readonly ILibPostalBroker libPostalBroker;

        public LibPostalService(ILibPostalBroker libPostalBroker)
        {
            this.libPostalBroker = libPostalBroker;
        }

        public string[] ExpandAddress(string address)
        {
            return this.libPostalBroker.ExpandAddress(address);
        }

        public List<KeyValuePair<string, string>> ParseAddress(string address) =>
            throw new NotImplementedException();
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NEL.LibPostalClient.Brokers.LibPostal;
using NEL.LibPostalClient.Brokers.Loggings;

namespace NEL.LibPostalClient.Services.LibPostal
{
    public partial class LibPostalService : ILibPostalService
    {
        private readonly ILibPostalBroker libPostalBroker;
        private readonly ILoggingBroker loggingBroker;

        public LibPostalService(
            ILibPostalBroker libPostalBroker,
            ILoggingBroker loggingBroker)
        {
            this.libPostalBroker = libPostalBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<string[]> ExpandAddress(string address) =>
            TryCatch(async () =>
            {
                ValidateAddressArgs(address);

                return await Task.FromResult(this.libPostalBroker.ExpandAddress(address));
            });

        public async ValueTask<List<KeyValuePair<string, string>>> ParseAddress(string address) =>
            throw new NotImplementedException();
    }
}

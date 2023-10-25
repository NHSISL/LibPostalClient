// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
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

        public ValueTask<string[]> ExpandAddressAsync(string address) =>
            TryCatch(async () =>
            {
                ValidateArgs(address);

                return await Task.FromResult(this.libPostalBroker.ExpandAddress(address));
            });

        public ValueTask<List<KeyValuePair<string, string>>> ParseAddressAsync(string address) =>
             TryCatch(async () =>
             {
                 ValidateArgs(address);

                 return await Task.FromResult(libPostalBroker.ParseAddress(address));
             });
    }
}

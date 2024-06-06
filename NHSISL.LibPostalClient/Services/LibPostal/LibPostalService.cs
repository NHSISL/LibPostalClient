// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using NHSISL.LibPostalClient.Brokers.LibPostal;

namespace NHSISL.LibPostalClient.Services.LibPostal
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
                 var parsedAddress = this.libPostalBroker.ParseAddress(address);
                 List<KeyValuePair<string, string>> result = DeduplicateKeys(parsedAddress);

                 return await Task.FromResult(result);
             });

        public List<KeyValuePair<string, string>> DeduplicateKeys(List<KeyValuePair<string, string>> inputList)
        {
            Dictionary<string, int> keyCounts = new Dictionary<string, int>();
            Dictionary<string, int> keyOccurrences = new Dictionary<string, int>();

            foreach (var kvp in inputList)
            {
                if (keyOccurrences.ContainsKey(kvp.Key))
                {
                    keyOccurrences[kvp.Key]++;
                }
                else
                {
                    keyOccurrences[kvp.Key] = 1;
                }
            }

            List<KeyValuePair<string, string>> outputList = new List<KeyValuePair<string, string>>();

            foreach (var kvp in inputList)
            {
                string key = kvp.Key;

                if (keyOccurrences[key] > 1)
                {
                    if (keyCounts.ContainsKey(key))
                    {
                        keyCounts[key]++;
                    }
                    else
                    {
                        keyCounts[key] = 1;
                    }
                    key = $"{key}_{keyCounts[key]}";
                }

                outputList.Add(new KeyValuePair<string, string>(key, kvp.Value));
            }

            return outputList;
        }
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace NHSISL.LibPostalClient.Services.LibPostal
{
    internal interface ILibPostalService
    {
        ValueTask<string[]> ExpandAddressAsync(string address);
        ValueTask<List<KeyValuePair<string, string>>> ParseAddressAsync(string address);
        List<KeyValuePair<string, string>> DeduplicateKeys(List<KeyValuePair<string, string>> inputList);
    }
}

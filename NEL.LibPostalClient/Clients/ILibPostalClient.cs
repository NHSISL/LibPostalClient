// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace NEL.LibPostalClient.Clients
{
    public interface ILibPostalClient
    {
        ValueTask<string[]> ExpandAddress(string address);
        ValueTask<List<KeyValuePair<string, string>>> ParseAddress(string address);
    }
}

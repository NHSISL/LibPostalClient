// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace NEL.LibPostalClient.Services.LibPostal
{
    internal interface ILibPostalService
    {
        string[] ExpandAddress(string address);
        List<KeyValuePair<string, string>> ParseAddress(string address);
    }
}

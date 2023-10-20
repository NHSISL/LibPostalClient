﻿// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;

namespace NEL.LibPostalClient.Brokers.LibPostal
{
    internal interface ILibPostalBroker
    {
        string[] ExpandAddress(string address);
        List<KeyValuePair<string, string>> ParseAddress(string address);
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using LibPostalNet;

namespace NEL.LibPostalClient.Brokers.LibPostal
{
    internal interface ILibPostalBroker
    {
        LibpostalNormalizeResponse ExpandAddress(string address, LibpostalNormalizeOptions options);
        LibpostalAddressParserResponse ParseAddress(string address, LibpostalAddressParserOptions options);
    }
}

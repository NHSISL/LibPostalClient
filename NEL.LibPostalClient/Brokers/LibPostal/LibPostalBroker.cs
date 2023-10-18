// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using LibPostalNet;
using NEL.LibPostalClient.Models.Brokers.LibPostal;

namespace NEL.LibPostalClient.Brokers.LibPostal
{
    internal class LibPostalBroker : ILibPostalBroker
    {
        private readonly LibPostalConfiguration libPostalConfiguration;

        public LibPostalBroker(LibPostalConfiguration libPostalConfiguration)
        {
            this.libPostalConfiguration = libPostalConfiguration;

            libpostal.LibpostalSetupDatadir(
                this.libPostalConfiguration.DataDirectory);

            libpostal.LibpostalSetupParserDatadir(
                this.libPostalConfiguration.ParserDataDirectory);

            libpostal.LibpostalSetupLanguageClassifierDatadir(
                this.libPostalConfiguration.LanguageClassifierDataDirectory);
        }

        public LibpostalNormalizeResponse ExpandAddress(string address, LibpostalNormalizeOptions options)
        {
            var expansion = libpostal.LibpostalExpandAddress(address, options);

            return expansion;
        }

        public LibpostalAddressParserResponse ParseAddress(string address, LibpostalAddressParserOptions options)
        {
            return libpostal.LibpostalParseAddress(address, options);
        }
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using LibPostalNet;
using NEL.LibPostalClient.Models.Brokers.LibPostal;

namespace NEL.LibPostalClient.Brokers.LibPostal
{
    public class LibPostalBroker : ILibPostalBroker
    {
        private readonly LibPostalConfiguration libPostalConfiguration;
        private readonly LibpostalAddressParserOptions libpostalAddressParserOptions;
        private readonly LibpostalNormalizeOptions libpostalNormalizeOptions;

        public LibPostalBroker(LibPostalConfiguration libPostalConfiguration)
        {
            this.libPostalConfiguration = libPostalConfiguration;

            libpostal.LibpostalSetupDatadir(
                this.libPostalConfiguration.DataDirectory);

            libpostal.LibpostalSetupParserDatadir(
                this.libPostalConfiguration.ParserDataDirectory);

            libpostal.LibpostalSetupLanguageClassifierDatadir(
                this.libPostalConfiguration.LanguageClassifierDataDirectory);

            this.libpostalAddressParserOptions = new LibpostalAddressParserOptions();
            libpostalAddressParserOptions.Country = this.libPostalConfiguration.PaserOptions.Country;
            libpostalAddressParserOptions.Language = this.libPostalConfiguration.PaserOptions.Language;

            this.libpostalNormalizeOptions = libpostal.LibpostalGetDefaultOptions();
        }

        /// <summary>
        /// This method takes an unstructured or partially structured address and attempts to break it down 
        /// into its constituent parts in a more detailed and structured format. The expansion process involves 
        /// parsing the input address string and breaking it down into its individual components. 
        /// This can include extracting information such as street numbers, street names, city names, 
        /// postal codes, and more.
        /// </summary>
        /// <param name="address">The address</param>
        /// <returns>A </returns>
        public string[] ExpandAddress(string address)
        {
            LibpostalNormalizeOptions options = libpostal.LibpostalGetDefaultOptions();
            var expansion = libpostal.LibpostalExpandAddress(address, this.libpostalNormalizeOptions);

            return expansion.Expansions;
        }

        /// <summary>
        /// The parsing algorithm analyzes the input address string to identify and extract 
        /// specific address components. It uses natural language processing and pattern recognition 
        /// to recognize the different parts of the address. The result is a structured representation of the 
        /// address in the form of key-value pairs or a data structure.
        /// </summary>
        /// <param name="address">The address</param>
        /// <returns></returns>
        public List<KeyValuePair<string, string>> ParseAddress(string address)
        {
            var parsedAddress = libpostal.LibpostalParseAddress(address, options: this.libpostalAddressParserOptions);

            return parsedAddress.Results;
        }
    }
}

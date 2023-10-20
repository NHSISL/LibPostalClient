// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using NEL.LibPostalClient.Clients;
using NEL.LibPostalClient.Models.Brokers.LibPostal;

namespace NEL.LibPostalClient.Tests.Acceptance
{
    public partial class LibPostalTest
    {
        private readonly ILibPostalClient libPostalClient;

        public LibPostalTest()
        {
            var config = new LibPostalConfiguration
            {
                DataDirectory = @"c:\temp\LipPostal",
                ParserDataDirectory = @"c:\temp\LipPostal\parser",
                LanguageClassifierDataDirectory = @"c:\temp\LipPostal\language_classifier",
                PaserOptions = new ParserOptions
                {
                    Country = "GB",
                    Language = "en",
                }
            };

            libPostalClient = new Clients.LibPostalClient(config);
        }
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.IO;
using System.Reflection;
using NHSISL.LibPostalClient.Clients;
using NHSISL.LibPostalClient.Models.Brokers.LibPostal;

namespace NHSISL.LibPostalClient.Tests.Acceptance.Brokers
{
    public class LibPostalFixtureBroker
    {
        internal ILibPostalClient libPostalClient;

        public LibPostalFixtureBroker()
        {
            string assembly = Assembly.GetExecutingAssembly().Location;
            string dataFolderPath = Path.Combine(Path.GetDirectoryName(assembly), @"Data");

            var config = new LibPostalConfiguration
            {
                DataDirectory = dataFolderPath,
                ParserDataDirectory = dataFolderPath,
                LanguageClassifierDataDirectory = dataFolderPath,
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

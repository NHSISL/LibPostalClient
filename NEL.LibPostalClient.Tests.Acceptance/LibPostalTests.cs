// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.IO;
using System.Reflection;
using NEL.LibPostalClient.Clients;
using NEL.LibPostalClient.Models.Brokers.LibPostal;

namespace NEL.LibPostalClient.Tests.Acceptance
{
    public partial class LibPostalTest
    {
        private readonly ILibPostalClient libPostalClient;

        public LibPostalTest()
        {

            string assembly = Assembly.GetExecutingAssembly().Location;
            string dataFolderPath = Path.Combine(Path.GetDirectoryName(assembly), @"App_Data\libpostal");

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

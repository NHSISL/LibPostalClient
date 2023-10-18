// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

namespace NEL.LibPostalClient.Models.Brokers.LibPostal
{
    public class LibPostalConfiguration
    {
        public string DataDirectory { get; set; } = string.Empty;
        public string ParserDataDirectory { get; set; } = string.Empty;
        public string LanguageClassifierDataDirectory { get; set; } = string.Empty;
        public ParserOptions PaserOptions { get; set; } = new ParserOptions();
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using NEL.LibPostalClient.Infrastructure.Services;

namespace NEL.LibPostalClient.Infrastructure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scriptGenerationService = new ScriptGenerationService();
            scriptGenerationService.GenerateBuildScript();
        }
    }
}
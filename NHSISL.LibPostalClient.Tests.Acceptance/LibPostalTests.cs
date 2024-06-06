// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using NHSISL.LibPostalClient.Tests.Acceptance.Brokers;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Acceptance
{
    [Collection(nameof(TestCollection))]
    public partial class LibPostalTest
    {
        private readonly LibPostalFixtureBroker libPostalFixtureBroker;

        public LibPostalTest(LibPostalFixtureBroker libPostalFixtureBroker)
        {
            this.libPostalFixtureBroker = libPostalFixtureBroker;
        }
    }
}

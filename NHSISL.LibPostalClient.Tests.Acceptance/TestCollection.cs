// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using NHSISL.LibPostalClient.Tests.Acceptance.Brokers;
using Xunit;

namespace NHSISL.LibPostalClient.Tests.Acceptance
{
    [CollectionDefinition(nameof(TestCollection))]
    public class TestCollection : ICollectionFixture<LibPostalFixtureBroker>
    { }
}

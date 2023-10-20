// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Moq;
using NEL.LibPostalClient.Brokers.LibPostal;
using NEL.LibPostalClient.Services.LibPostal;
using Tynamix.ObjectFiller;
using Xeptions;

namespace NEL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        private readonly Mock<ILibPostalBroker> libPostalServiceBrokerMock;
        private readonly ILibPostalService libPostalService;

        public LibPostalServiceTests()
        {
            libPostalServiceBrokerMock = new Mock<ILibPostalBroker>();

            libPostalService =
                new LibPostalService(libPostalBroker: libPostalServiceBrokerMock.Object);
        }

        private static string GetRandomString(int wordMinLength = 2, int wordMaxLength = 100) =>
            new MnemonicString(
                wordCount: 1,
                wordMinLength,
                wordMaxLength: wordMaxLength < wordMinLength ? wordMinLength : wordMaxLength)
                    .GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
           actualException => actualException.SameExceptionAs(expectedException);

    }
}

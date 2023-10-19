// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NEL.LibPostalClient.Extensions.Exceptions
{
    public static class ExceptionExtension
    {
        public static string GetValidationSummary(this Exception exception)
        {
            if ((exception == null || exception.Data.Count == 0)
                && (exception?.InnerException == null || exception.InnerException.Data.Count == 0))
            {
                return string.Empty;
            }

            StringBuilder validationSummary = new StringBuilder();
            AppendErrorSummary(exception, validationSummary);

            if (exception.InnerException != null)
            {
                AppendInnerErrorSummary(exception.InnerException, validationSummary);
            }

            return validationSummary.ToString();
        }

        private static void AppendErrorSummary(Exception exception, StringBuilder validationSummary)
        {
            if (exception.Data.Count > 0)
            {
                validationSummary.Append($"{exception.GetType().Name} Errors:  ");

                foreach (DictionaryEntry entry in exception.Data)
                {
                    string errorSummary = ((List<string>)entry.Value)
                        .Select((value) => value)
                        .Aggregate((current, next) => current + ", " + next);

                    string line = $"{entry.Key} => {errorSummary};  ";

                    if (!validationSummary.ToString().Contains(line))
                    {
                        validationSummary.Append(line);
                    }
                }

                validationSummary.AppendLine();
            }
        }

        private static void AppendInnerErrorSummary(Exception exception, StringBuilder validationSummary)
        {
            if (exception != null)
            {
                AppendErrorSummary(exception, validationSummary);

                if (exception.InnerException != null)
                {
                    AppendInnerErrorSummary(exception.InnerException, validationSummary);
                }
            }
        }
    }
}

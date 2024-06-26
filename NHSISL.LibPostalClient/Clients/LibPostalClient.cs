﻿// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHSISL.LibPostalClient.Brokers.LibPostal;
using NHSISL.LibPostalClient.Models.Brokers.LibPostal;
using NHSISL.LibPostalClient.Models.Clients.LibPostal.Exceptions;
using NHSISL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using NHSISL.LibPostalClient.Services.LibPostal;
using NHSISL.MESH.Models.Foundations.LibPostal.Exceptions;
using Xeptions;

namespace NHSISL.LibPostalClient.Clients
{
    public class LibPostalClient : ILibPostalClient
    {
        private readonly ILibPostalService libPostalService;

        public LibPostalClient(LibPostalConfiguration config)
        {
            IHost host = RegisterServices(config);
            this.libPostalService = host.Services.GetRequiredService<ILibPostalService>();
        }

        public async ValueTask<string[]> ExpandAddressAsync(string address)
        {
            try
            {
                return await this.libPostalService.ExpandAddressAsync(address);
            }
            catch (LibPostalValidationException libPostalValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    innerException: libPostalValidationException.InnerException as Xeption,
                    data: libPostalValidationException.InnerException.Data);
            }
            catch (LibPostalDependencyValidationException libPostalDependencyValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    libPostalDependencyValidationException.InnerException as Xeption,
                    data: libPostalDependencyValidationException.InnerException.Data);
            }
            catch (LibPostalDependencyException libPostalDependencyException)
            {
                throw new LibPostalClientDependencyException(
                    message: "LibPostal client dependency error occurred, contact support.",
                    libPostalDependencyException.InnerException as Xeption);
            }
            catch (LibPostalServiceException libPostalServiceException)
            {
                throw new LibPostalClientServiceException(
                    message: "LibPostal client service error occurred, contact support.",
                    libPostalServiceException.InnerException as Xeption);
            }
        }

        public async ValueTask<List<KeyValuePair<string, string>>> ParseAddressAsync(string address)
        {
            try
            {
                return await this.libPostalService.ParseAddressAsync(address);
            }
            catch (LibPostalValidationException libPostalValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    libPostalValidationException.InnerException as Xeption,
                    data: libPostalValidationException.InnerException.Data);
            }
            catch (LibPostalDependencyValidationException libPostalDependencyValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    libPostalDependencyValidationException.InnerException as Xeption,
                    data: libPostalDependencyValidationException.InnerException.Data);
            }
            catch (LibPostalDependencyException libPostalDependencyException)
            {
                throw new LibPostalClientDependencyException(
                    message: "LibPostal client dependency error occurred, contact support.",
                    libPostalDependencyException.InnerException as Xeption);
            }
            catch (LibPostalServiceException libPostalServiceException)
            {
                throw new LibPostalClientServiceException(
                    message: "LibPostal client service error occurred, contact support.",
                    libPostalServiceException.InnerException as Xeption);
            }
        }

        private static IHost RegisterServices(LibPostalConfiguration config)
        {
            ValidateConfiguration(config);
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices(configuration =>
            {
                configuration.AddSingleton(options => config);
                configuration.AddTransient<ILibPostalBroker, LibPostalBroker>();
                configuration.AddTransient<ILibPostalService, LibPostalService>();
            });

            IHost host = builder.Build();

            return host;
        }

        private static void ValidateConfiguration(LibPostalConfiguration config)
        {
            if (config is null)
            {
                throw new LibPostalClientConfigurationException(
                    message: "LibPostal client configuration is not set.");
            }

            Validate(
                (Rule: IsInvalid(config.DataDirectory), Parameter: nameof(LibPostalConfiguration.DataDirectory)),

                (Rule: IsInvalid(config.ParserDataDirectory),
                    Parameter: nameof(LibPostalConfiguration.ParserDataDirectory)),

                (Rule: IsInvalid(config.LanguageClassifierDataDirectory),
                    Parameter: nameof(LibPostalConfiguration.LanguageClassifierDataDirectory)));
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAddressException = new LibPostalClientConfigurationException(
                message: "Invalid LibPostal client configuration. Please correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAddressException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAddressException.ThrowIfContainsErrors();
        }
    }
}

// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NEL.LibPostalClient.Brokers.LibPostal;
using NEL.LibPostalClient.Models.Brokers.LibPostal;
using NEL.LibPostalClient.Models.Clients.LibPostal.Exceptions;
using NEL.LibPostalClient.Models.Foundations.LibPostal.Exceptions;
using NEL.LibPostalClient.Services.LibPostal;
using NEL.MESH.Models.Foundations.LibPostal.Exceptions;
using Xeptions;

namespace NEL.LibPostalClient.Clients
{
    public class LibPostalClient : ILibPostalClient
    {
        private readonly ILibPostalService libPostalService;

        public LibPostalClient(LibPostalConfiguration config)
        {
            IHost host = RegisterServices(config);
            this.libPostalService = host.Services.GetRequiredService<ILibPostalService>();
        }

        public async ValueTask<string[]> ExpandAddress(string address)
        {
            try
            {
                return await this.libPostalService.ExpandAddress(address);
            }
            catch (LibPostalValidationException libPostalValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    libPostalValidationException.InnerException as Xeption);
            }
            catch (LibPostalDependencyValidationException libPostalDependencyValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    libPostalDependencyValidationException.InnerException as Xeption);
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

        public async ValueTask<List<KeyValuePair<string, string>>> ParseAddress(string address)
        {
            try
            {
                return await this.libPostalService.ParseAddress(address);
            }
            catch (LibPostalValidationException libPostalValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    libPostalValidationException.InnerException as Xeption);
            }
            catch (LibPostalDependencyValidationException libPostalDependencyValidationException)
            {
                throw new LibPostalClientValidationException(
                    message: "LibPostal client validation error(s) occurred, fix the error(s) and try again.",
                    libPostalDependencyValidationException.InnerException as Xeption);
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
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OptionsConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, IConfiguration config)
            where TOptions : class, new()
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddSingleton<IConfigureOptions<TOptions>>(new ConfigureFromConfigurationOptions<TOptions>(config));
            services.TryAddSingleton<TOptions>(sp => sp.GetRequiredService<IOptions<TOptions>>().Value);
            return services;
        }

        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, IConfiguration config, bool trackConfigChanges)
            where TOptions : class, new()
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddSingleton<IConfigureOptions<TOptions>>(new ConfigureFromConfigurationOptions<TOptions>(config));
            services.TryAddSingleton<TOptions>(sp => sp.GetRequiredService<IOptions<TOptions>>().Value);
            if (trackConfigChanges)
            {
                services.AddSingleton<IOptionsChangeTokenSource<TOptions>>(new ConfigurationChangeTokenSource<TOptions>(config));
            }
            return services;
        }
    }
}
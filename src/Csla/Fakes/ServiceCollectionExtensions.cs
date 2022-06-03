/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DataAccess.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNotStandard.UnitTesting.Csla
{

	/// <summary>
	/// Extension methods for the IServiceCollection type
	/// </summary>
	internal static class ServiceCollectionExtensions
	{

		/// <summary>
		/// Add a default, fake IConfiguration implementation if none is already present
		/// </summary>
		/// <param name="services">The service collection that we extend</param>
		/// <returns>The service collection being extended, to support method chaining</returns>
		internal static IServiceCollection TryAddFakeConfiguration(this IServiceCollection services)
		{
			services.TryAddScoped<IConfiguration, Fakes.FakeConfiguration>();

			return services;
		}

		/// <summary>
		/// Add a default, fake ILogger<T> implementation if none is already present
		/// </summary>
		/// <param name="services">The service collection that we extend</param>
		/// <returns>The service collection being extended, to support method chaining</returns>
		internal static IServiceCollection TryAddFakeLogging(this IServiceCollection services)
		{
			services.TryAddScoped(typeof(ILogger<>), typeof(Fakes.FakeLogger<>));

			return services;
		}

		/// <summary>
		/// Add a default, fake IConnnectionManager implementation if none is already present
		/// </summary>
		/// <param name="services">The service collection that we extend</param>
		/// <returns>The service collection being extended, to support method chaining</returns>
		internal static IServiceCollection TryAddFakeConnectionManagement(this IServiceCollection services)
		{
			services.TryAddScoped<IConnectionManager, Fakes.FakeConnectionManager>();

			return services;
		}
	}
}

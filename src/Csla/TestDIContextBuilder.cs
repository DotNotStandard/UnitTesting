/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using Csla.Configuration;
using Csla.Core;
using DotNotStandard.Validation.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace DotNotStandard.UnitTesting.Csla
{
	/// <summary>
	/// Builder class for creating a configured TestDIContext for unit testing
	/// </summary>
	public class TestDIContextBuilder
	{
		private readonly IServiceCollection _services;
		private IPrincipal _principal;

		public TestDIContextBuilder()
		{
			_services = new ServiceCollection();
			_principal = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("Test User"), new string[] { "Users" }));
		}

		/// <summary>
		/// Accept a principal for use in authentication during testing
		/// </summary>
		/// <param name="principal">The principal that is to be used for testing</param>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder UsePrincipal(IPrincipal principal)
		{
			_principal = principal;

			return this;
		}

		/// <summary>
		/// Add Csla to the service collection with default config, to enable testing of Csla types
		/// </summary>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddCsla()
		{
			_services.AddCsla();
			return this;
		}

		/// <summary>
		/// Add Csla to the service collection with custom config, to enable testing of Csla types
		/// </summary>
		/// <param name="options">The delegate that applies custom options to the underlying config</param>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddCsla(Action<CslaOptions> options)
		{
			_services.AddCsla(options);
			return this;
		}

		/// <summary>
		/// Register a type for use during testing with transient lifetime
		/// </summary>
		/// <typeparam name="T">The type that is to be registered</typeparam>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddTransient<T>() where T: class
		{
			_services.AddTransient<T>();

			return this;
		}

		/// <summary>
		/// Register a type for use during testing with transient lifetime
		/// </summary>
		/// <typeparam name="TService">The type representing the service that will be requested</typeparam>
		/// <typeparam name="TImplementation">The type used to implement the service type</typeparam>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			_services.AddTransient<TService, TImplementation>();

			return this;
		}

		/// <summary>
		/// Register a type for use during testing with scoped lifetime
		/// </summary>
		/// <typeparam name="TService">The type representing the service that will be requested</typeparam>
		/// <typeparam name="TImplementation">The type used to implement the service type</typeparam>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddScoped<T>() where T : class
		{
			_services.AddScoped<T>();

			return this;
		}

		/// <summary>
		/// Register a type for use during testing with scoped lifetime
		/// </summary>
		/// <typeparam name="TService">The type representing the service that will be requested</typeparam>
		/// <typeparam name="TImplementation">The type used to implement the service type</typeparam>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			_services.AddScoped<TService, TImplementation>();

			return this;
		}

		/// <summary>
		/// Register a type for use during testing with singleton lifetime
		/// </summary>
		/// <typeparam name="TService">The type representing the service that will be requested</typeparam>
		/// <typeparam name="TImplementation">The type used to implement the service type</typeparam>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddSingleton<T>() where T : class
		{
			_services.AddSingleton<T>();

			return this;
		}

		/// <summary>
		/// Register a type for use during testing with singleton lifetime
		/// </summary>
		/// <typeparam name="TService">The type representing the service that will be requested</typeparam>
		/// <typeparam name="TImplementation">The type used to implement the service type</typeparam>
		/// <returns>The builder instance, to enable method chaining</returns>
		public TestDIContextBuilder AddSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
		{
			_services.AddSingleton<TService, TImplementation>();

			return this;
		}

		/// <summary>
		/// Apply custom configuration to the underlying service collection
		/// </summary>
		/// <param name="action">The action to be used to apply custom configuration</param>
		/// <returns></returns>
		public TestDIContextBuilder ConfigureServices(Action<IServiceCollection> action)
		{
			action.Invoke(_services);

			return this;
		}

		/// <summary>
		/// Complete configuration, using the data from previous methods to build up the 
		/// service collection into a service provider, and return that in a test DI context
		/// </summary>
		/// <returns>A TestDIContext that is configured for testing of code using CSLA objects</returns>
		public TestDIContext Build()
		{
			IServiceProvider serviceProvider;
			IContextManager contextManager;

			// Add default logging and configuration implementations
			_services.TryAddFakeLogging();
			_services.TryAddFakeConfiguration();

			// Register the default items needed for testing CSLA types
			_services.AddValidationSubsystem();
			_services.AddValidationInMemoryRepositories();
			_services.TryAddFakeConnectionManagement();

			// Build the service provider
			serviceProvider = _services.BuildServiceProvider();

			// Set up auth principal, to support testing types with authorisation rules
			contextManager = serviceProvider.GetRequiredService<IContextManager>();
			contextManager.SetUser(_principal);

			// Initialise the validation subsystem for use during testing
			ValidationSubsystem.Initialise(serviceProvider);

			return new TestDIContext(serviceProvider);
		}
	}
}

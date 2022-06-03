/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNotStandard.UnitTesting.Csla
{

	/// <summary>
	/// Test DI Context that can be used for DI during unit tests
	/// </summary>
	public class TestDIContext
	{
		private readonly IServiceProvider _services;

		public TestDIContext(IServiceProvider services)
		{
			_services = services;
		}

		/// <summary>
		/// Create an instance of a required type using the root DI container
		/// </summary>
		/// <typeparam name="T">The type that is required</typeparam>
		/// <returns>An appropriately instantiated object of the required type</returns>
		public T GetRequiredService<T>()
		{
			return _services.GetRequiredService<T>();
		}
	}
}

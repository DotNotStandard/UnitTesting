/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace DotNotStandard.UnitTesting.Csla.Fakes
{
	/// <summary>
	/// Fake implementation of IConfiguration for use during unit tests
	/// </summary>
	internal class FakeConfiguration : IConfiguration
	{
		public string this[string key] 
		{ 
			get { return string.Empty; } 
			set {} 
		}

		public IEnumerable<IConfigurationSection> GetChildren()
		{
			return Enumerable.Empty<IConfigurationSection>();
		}

		public IChangeToken GetReloadToken()
		{
			return null;
		}

		public IConfigurationSection GetSection(string key)
		{
			return null;
		}
	}
}

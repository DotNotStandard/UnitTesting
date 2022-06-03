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

namespace DotNotStandard.UnitTesting.Csla.Fakes
{
	/// <summary>
	/// Fake type that can represent a logger scope during unit tests
	/// </summary>
	internal class FakeLoggerScope : IDisposable
	{
		public void Dispose()
		{
		}
	}
}

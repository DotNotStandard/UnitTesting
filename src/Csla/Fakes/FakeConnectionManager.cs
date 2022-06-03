/*
 * Copyright © 2022 DotNotStandard. All rights reserved.
 * 
 * See the LICENSE file in the root of the repo for licensing details.
 * 
 */
using DotNotStandard.DataAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DotNotStandard.UnitTesting.Csla.Fakes
{
	/// <summary>
	/// Fake implementation of IConnectionManager for use during unit tests
	/// </summary>
	internal class FakeConnectionManager : IConnectionManager
	{
		public IAsyncDisposable StartTransaction()
		{
			return new FakeTransactionDisposer();
		}

		public IAsyncDisposable StartTransaction(TransactionScopeOption transactionScope)
		{
			return new FakeTransactionDisposer();
		}

		public Task<T> GetConnectionAsync<T>(string connectionName) where T : class
		{
			throw new NotImplementedException();
		}

		public Task CommitTransactionAsync()
		{
			return Task.CompletedTask;
		}

		public Task FinaliseTransactionAsync()
		{
			return Task.CompletedTask;
		}

		public ValueTask DisposeAsync()
		{
			return ValueTask.CompletedTask;
		}

	}
}

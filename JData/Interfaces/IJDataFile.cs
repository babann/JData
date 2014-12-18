using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JData
{
    public interface IJDataFile : IDisposable, IBindingList
	{
		IJDataHeader Header { get; }

		IEnumerable<IJDataRow> Data { get; }

        IJDataRow this[int inndex] { get; }

		int ColumnsCount { get; }

		int RowsCount { get; }

		IJDataRow CreateHeaderRow ();

		IJDataRow CreateDataRow ();
	}
}


using System;
using System.Collections.Generic;

namespace JData
{
	public interface IJDataFile
	{
		IJDataHeader Header { get; }

		IEnumerable<IJDataRow> Data { get; }

		int ColumnsCount { get; }

		int RowsCount { get; }

		IJDataRow this [int index] { get; }

		void AppendHeader (params IJDataRow[] rows);

		IJDataRow NewRow ();

		IJDataRow AddDataRow ();
	}
}


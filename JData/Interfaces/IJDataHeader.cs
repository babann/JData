using System;

namespace JData
{
	public interface IJDataHeader
	{
		IJDataFile Parent { get; }

		int ColumnsCount { get; }

		void AddRows(params IJDataRow [] rows);
	}
}


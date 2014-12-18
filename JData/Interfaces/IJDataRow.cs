using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JData
{
	public interface IJDataRow : ICustomTypeDescriptor
	{
		IJDataFile Parent { get; }

		IEnumerable<IJDataCell> Cells { get; }

		int CellsCount { get; }

		IJDataCell this [int index] { get; }

        IJDataCell this[string column] { get; }

		void SetParent(IJDataFile parent);

		IJDataCell AddCell();

		void AddValues(params string [] values);
	}
}


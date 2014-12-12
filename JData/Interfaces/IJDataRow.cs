using System;
using System.Collections.Generic;

namespace JData
{
	public interface IJDataRow //: ICloneable
	{
		IJDataFile Parent { get; }

		IEnumerable<IJDataCell> Cells { get; }

		int CellsCount { get; }

		IJDataCell this [int index] { get; }

		void SetParent(IJDataFile parent);

		IJDataCell AddCell();

		void AddValues(params string [] values);
	}
}


using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace JData
{
	public interface IJDataHeader
	{
		IJDataFile Parent { get; }

        IEnumerable<IJDataRow> Rows { get; }

        bool HasRows { get; }

		int ColumnsCount { get; }

        IJDataRow CreateRow();

        int GetColumnIndex(string name);

        PropertyDescriptorCollection GetPropertyDescriptorCollection(Attribute[] attributes);
	}
}


using System;

namespace JData
{
	public interface IJDataCell //: ICloneable
	{
		IJDataRow Parent { get; }

		string Value { get; set; }

		int ColumnIndex { get; set; }

		void SetParent(IJDataRow parent);
	}
}


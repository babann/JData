using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel;

namespace JData
{
	/// <summary>
	/// Represents the header of the JSON file.
	/// </summary>
	public class JDataHeader : IJDataHeader
	{
		#region Private Fields

        private PropertyDescriptorCollection propertyDescriptorCollectionCache;
		private List<IJDataRow> _headerRows;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the parent file.
		/// </summary>
		[JsonIgnore]
		public IJDataFile Parent {
			get;
			private set;
		}

		/// <summary>
		/// Gets the rows of the header.
		/// </summary>
		public IEnumerable<IJDataRow> Rows
		{
			get {
				return _headerRows;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this header has rows.
		/// </summary>
		/// <value><c>true</c> if this instance has rows; otherwise, <c>false</c>.</value>
		[JsonIgnore]
		public bool HasRows
		{
			get{
				return _headerRows.Any ();
			}
		}

		/// <summary>
		/// Gets the columns count.
		/// Maximum columns count of all header rows is returned.
		/// </summary>
		/// <value>The columns count.</value>
		[JsonIgnore]
		public int ColumnsCount {
			get {
				return _headerRows.Any() ? _headerRows.Max (x => x.CellsCount) : 0;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataHeader"/> class.
		/// </summary>
		/// <param name="parent">Parent.</param>
		public JDataHeader (IJDataFile parent)
		{
			Parent = parent;
			_headerRows = new List<IJDataRow> ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataHeader"/> class.
		/// Called by JSON deserializer.
		/// </summary>
		/// <param name="Rows">Deserialized Rows property.</param>
		[JsonConstructor]
		private JDataHeader(IEnumerable<JDataRow> Rows) {
			_headerRows = new List<IJDataRow> (Rows);
		}

		#endregion

		/// <summary>
		/// Adds the rows to the header.
		/// </summary>
		public IJDataRow CreateRow()
		{
            var row = new JDataRow(Parent);
            _headerRows.Add(row);
            return row;
		}

        public int GetColumnIndex(string name)
        {
            var row = _headerRows.First();
            int cnt = 0;
            foreach (var cell in row.Cells)
            {
                if (cell.Value == name)
                    return cnt;

                cnt++;
            }

            throw new KeyNotFoundException();
        }
		/// <summary>
		/// Clear the rows.
		/// </summary>
		public void Empty()
		{
			_headerRows.Clear ();
		}

        public PropertyDescriptorCollection GetPropertyDescriptorCollection(Attribute[] attributes)
        {
            if (propertyDescriptorCollectionCache == null)
            {
                int columnsCount = this.ColumnsCount;
                PropertyDescriptor[] props = new PropertyDescriptor[columnsCount];
                {
                    for (int i = 0; i < columnsCount; i++)
                    {
                        props[i] = new JDataColumnPropertyDescriptor(_headerRows.First()[i]);
                    }
                }
                propertyDescriptorCollectionCache = new PropertyDescriptorCollection(props);
            }
            return propertyDescriptorCollectionCache;
        }

	}
}


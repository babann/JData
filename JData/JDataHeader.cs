using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JData
{
	/// <summary>
	/// Represents the header of the JSON file.
	/// </summary>
	public class JDataHeader : IJDataHeader
	{
		#region Private Fields

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
		public void AddRows(params IJDataRow [] rows)
		{
			_headerRows.AddRange (rows.Select( x=> x.Parent == null ? x : new JDataRow(this.Parent, x)));
			_headerRows.ForEach(x=> x.SetParent(this.Parent));

		}

		/// <summary>
		/// Clear the rows.
		/// </summary>
		public void Empty()
		{
			_headerRows.Clear ();
		}
	}
}


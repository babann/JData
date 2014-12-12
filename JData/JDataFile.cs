using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JData
{
	/// <summary>
	/// JSON table file.
	/// </summary>
	public class JDataFile : IJDataFile
	{
		#region Private Fields

		private List<IJDataRow> _data;
		private IJDataHeader _header;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the header rows of the data.
		/// </summary>
		public IJDataHeader Header {
			get {
				if (_header == null)
					_header = new JDataHeader (this);

				return _header;
			}
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		public IEnumerable<IJDataRow> Data {
			get {
				return _data;
			}
		}

		/// <summary>
		/// Gets the columns count from header.
		/// </summary>
		[JsonIgnore]
		public int ColumnsCount {
			get {
				return Header.ColumnsCount;
			}
		}

		/// <summary>
		/// Gets the rows count from data.
		/// </summary>
		[JsonIgnore]
		public int RowsCount {
			get {
				return _data.Count;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataFile"/> class.
		/// </summary>
		public JDataFile ()
		{
			_data = new List<IJDataRow> ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataFile"/> class.
		/// Called by JSON deserializer.
		/// </summary>
		/// <param name="Header">Deserialized Header property.</param>
		/// <param name="Data">Deserialized Data property.</param>
		[JsonConstructor]
		private JDataFile(JDataHeader Header, IEnumerable<JDataRow> Data)
		{
			_header = Header;
			_data = new List<IJDataRow>( Data );
		}

		public IJDataRow this[int index] {
			get {
				if (index > _data.Count)
					throw new ArgumentOutOfRangeException ("index");

				return _data [index];
			}
		}

		#endregion

		#region Public Interface

		/// <summary>
		/// Appends rows to the header.
		/// </summary>
		/// <param name="rows">Rows to append.</param>
		public void AppendHeader(params IJDataRow [] rows) {
			if (rows == null)
				throw new ArgumentNullException ("rows");

			Header.AddRows (rows);
		}

		/// <summary>
		/// Creates the new the row for that file.
		/// </summary>
		public IJDataRow NewRow() {
			return new JDataRow (this);
		}

		/// <summary>
		/// Creates the new row and adds it to the Data.
		/// </summary>
		public IJDataRow AddDataRow() {
			var row = new JDataRow (this);
			_data.Add (row);
			return row;
		}

		/// <summary>
		/// Load the specified file from disk.
		/// </summary>
		/// <param name="fileName">File name with a path.</param>
		public static IJDataFile Load(string fileName) {
			IJDataReader reader = new JDataReader ();
			return reader.Read (fileName);
		}

		/// <summary>
		/// Save the specified file to the disk.
		/// </summary>
		/// <param name="fileName">File name with a path.</param>
		public void Save(string fileName) {
			IJDataWriter writer = new JDataWriter (this);
			writer.Write (fileName);
		}

		#endregion
	}
}


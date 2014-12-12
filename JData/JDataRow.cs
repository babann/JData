﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JData
{
	/// <summary>
	/// Represents the data or header row of the  JSON file.
	/// </summary>
	public class JDataRow : IJDataRow
	{
		#region Fields

		private List<IJDataCell> _cells;

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
		/// Gets the data cells of this row.
		/// </summary>
		public IEnumerable<IJDataCell> Cells {
			get {
				return _cells;
			}
		}

		/// <summary>
		/// Gets the cells count.
		/// </summary>
		[JsonIgnore]
		public int CellsCount {
			get {
				return _cells.Count;
			}
		}

		/// <summary>
		/// Gets the <see cref="JData.IJDataCell"/> at the specified index.
		/// </summary>
		[JsonIgnore]
		public IJDataCell this[int index] {
			get {
				if (index > CellsCount)
					throw new ArgumentOutOfRangeException ("index");

				return _cells [index];
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataRow"/> class.
		/// </summary>
		/// <param name="parentFile">Parent file.</param>
		internal JDataRow(IJDataFile parentFile) {
			_cells = new List<IJDataCell>();
			Parent = parentFile;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataRow"/> class.
		/// </summary>
		/// <param name="parentFile">Parent file.</param>
		/// <param name="sourceRow">Source row.</param>
		internal JDataRow(IJDataFile parentFile, IJDataRow sourceRow)
			: this(parentFile) {

			_cells.AddRange(sourceRow.Cells.Select(x => (x.Parent == null) ? x : new JDataCell(this, x)));
			_cells.ForEach (x => x.SetParent (this));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataRow"/> class.
		/// Called by JSON deserializer.
		/// </summary>
		/// <param name="Cells">Deserialized Cells property.</param>
		[JsonConstructor]
		private JDataRow(IEnumerable<JDataCell> Cells) {
			_cells = new List<IJDataCell> (Cells);
		}

		#endregion

		#region Public Interface

		/// <summary>
		/// Creates the new cell and add it to this row.
		/// </summary>
		public IJDataCell AddCell() {

			var cell = new JDataCell (this);
			_cells.Add (cell);
			return cell;
		}

		/// <summary>
		/// Appends the cells based on the specified values.
		/// </summary>
		public void AddValues(params string [] values) {
			//TODO: validate columns count ??

			if (values == null)
				throw new ArgumentNullException ("values");

			foreach(var value in values)
				_cells.Add(new JDataCell(this, string.IsNullOrEmpty(value) ? "" : value));
		}

		/// <summary>
		/// Sets the parent file.
		/// </summary>
		/// <param name="parent">Parent file.</param>
		public void SetParent(IJDataFile parent) {
			Parent = parent;
		}

		#endregion
	}
}


using System;
using Newtonsoft.Json;

namespace JData
{
	/// <summary>
	/// Represents the cell of the JSON file.
	/// </summary>
	public class JDataCell : IJDataCell
	{
		#region Properties

		/// <summary>
		/// Gets the parent Row.
		/// </summary>
		[JsonIgnore]
		public IJDataRow Parent { 
			get; 
			private set;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public string Value {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the index of the column.
		/// </summary>
		/// <remarks>Not used yet</remarks>
		public int ColumnIndex {
			get;
			set;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataCell"/> class.
		/// </summary>
		/// <param name="parentRow">Parent row.</param>
		public JDataCell(JDataRow parentRow) {
			Parent = parentRow;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataCell"/> class.
		/// </summary>
		/// <param name="parentRow">Parent row.</param>
		/// <param name="value">Value.</param>
		public JDataCell(JDataRow parentRow, string value) :
			this(parentRow) {

			Value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JData.JDataCell"/> class.
		/// </summary>
		/// <param name="parentRow">Parent row.</param>
		/// <param name="source">Source cell.</param>
		public JDataCell(JDataRow parentRow, IJDataCell source) 
			: this(parentRow) {

			Value = source.Value.Clone() as string;
		}

		private JDataCell ()
		{

		}

		#endregion

		#region Public Interface

		/// <summary>
		/// Sets the parent row.
		/// </summary>
		public void SetParent(IJDataRow parent) {
			Parent = parent;
		}

		#endregion
	}
}


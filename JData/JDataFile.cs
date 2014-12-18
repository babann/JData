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

        private object _syncObject = new object();

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
		public IJDataRow CreateHeaderRow() {
			return Header.CreateRow();
		}

		/// <summary>
		/// Creates the new row and adds it to the Data.
		/// </summary>
		public IJDataRow CreateDataRow() {
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

        #region IDisposable implementation

        public void Dispose()
        {
            _header = null;
            _data.Clear();
            _data = null;
        }

        #endregion

        #region IListSource implementation

        public bool ContainsListCollection
        {
            get { return true; }
        }

        public System.Collections.IList GetList()
        {
            return _data;
        }

        #endregion

        public void AddIndex(System.ComponentModel.PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public object AddNew()
        {
            return CreateDataRow();
        }

        public bool AllowEdit
        {
            get { return true; }
        }

        public bool AllowNew
        {
            get { return true; }
        }

        public bool AllowRemove
        {
            //TODO: implement later
            get { return false; }
        }

        public void ApplySort(System.ComponentModel.PropertyDescriptor property, System.ComponentModel.ListSortDirection direction)
        {
            throw new NotImplementedException();
        }

        public int Find(System.ComponentModel.PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }

        public bool IsSorted
        {
            get { return false; }
        }

        public event System.ComponentModel.ListChangedEventHandler ListChanged;

        public void RemoveIndex(System.ComponentModel.PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void RemoveSort()
        {
            throw new NotImplementedException();
        }

        public System.ComponentModel.ListSortDirection SortDirection
        {
            get { throw new NotImplementedException(); }
        }

        public System.ComponentModel.PropertyDescriptor SortProperty
        {
            get { throw new NotImplementedException(); }
        }

        public bool SupportsChangeNotification
        {
            //TODO: implement later
            get { return false; }
        }

        public bool SupportsSearching
        {
            get { return true; }
        }

        public bool SupportsSorting
        {
            get { return true; }
        }

        public int Add(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value is IJDataRow)
            {
                _data.Add(value as IJDataRow);
                return _data.Count;
            }

            throw new ArgumentException("value");
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value is IJDataRow)
                return _data.Contains(value as IJDataRow);

            throw new ArgumentException("value");
        }

        public int IndexOf(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value is IJDataRow)
                return _data.IndexOf(value as IJDataRow);

            throw new ArgumentException("value");
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Remove(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (value is IJDataRow)
                _data.Remove(value as IJDataRow);

            throw new ArgumentException("value");
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _data.Count)
                throw new ArgumentOutOfRangeException("index");

            _data.RemoveAt(index);
        }

        object System.Collections.IList.this[int index]
        {
            get
            {
                //TODO: parameters validation
                return _data[index];
            }
            set
            {
                //TODO: the same
                _data[index] = value as IJDataRow;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _data.Count; }
        }

        public bool IsSynchronized
        {
            //TODO: read MSDN
            get { return true; }
        }

        public object SyncRoot
        {
            get { return _syncObject; }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}


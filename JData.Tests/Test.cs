using NUnit.Framework;
using System;
using System.IO;

namespace JData.Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test()]
		public void EmptyFilePropertiesTest()
		{
			JDataFile file = new JDataFile ();
			Assert.AreEqual (file.ColumnsCount, 0);
			Assert.AreEqual (file.RowsCount, 0);
			Assert.AreEqual (file.ColumnsCount, file.Header.ColumnsCount);
		}

		[Test ()]
		public void AddHeaderRowsTest ()
		{
			string[] headerLine1 = { "col1", "col2", "col3" };
			string[] headerLine2 = { "col4", "col5", "col6", "col7" };

			JDataFile file = new JDataFile ();
			var headerRow1 = file.CreateHeaderRow ();
			headerRow1.AddValues (headerLine1);

			Assert.AreEqual (headerLine1.Length, file.ColumnsCount);

			var headerRow2 = file.CreateHeaderRow ();
			headerRow2.AddValues (headerLine2);

			Assert.AreEqual (headerLine2.Length, file.ColumnsCount);
		}

		[Test ()]
		public void AddDataRowsTest ()
		{
			string[] dataLine1 = { "cell11", "cell12", "cell13" };
			string[] dataLine2 = { "cell21", "cell22", "cell23", "cell24" };

			JDataFile file = new JDataFile ();
			var dataRow1 = file.CreateDataRow ();
			dataRow1.AddValues (dataLine1);

			Assert.AreEqual (dataLine1[0], file[0][0].Value);

			var dataRow2 = file.CreateDataRow ();
			dataRow2.AddValues (dataLine2);

			Assert.AreEqual (dataLine2[1],file[1][1].Value);

			dataRow2.AddValues (dataLine1);
			Assert.AreEqual (dataRow2.CellsCount, dataLine1.Length + dataLine2.Length);
		}

		[Test()]
		public void SaveLoadEmptyFileTest()
		{
			JDataFile originalFile = new JDataFile ();
			string fileName = System.IO.Path.GetTempFileName ();
			originalFile.Save (fileName);
			var newFile = JDataFile.Load (fileName);
			System.IO.File.Delete (fileName);

			Assert.NotNull (newFile);
			Assert.AreEqual (originalFile.ColumnsCount, newFile.ColumnsCount);
			Assert.AreEqual (originalFile.RowsCount, newFile.RowsCount);
			Assert.Catch (
				typeof(ArgumentOutOfRangeException)
				, () => {
				var q = originalFile [1] [1].Value;
			});
		}

		[Test()]
		public void IncorrectFileNameTest()
		{
			JDataFile file = new JDataFile ();
			Assert.Catch (
				typeof(ArgumentException),
				() => {
					file.Save(null);
				});

			Assert.Catch (
				typeof(ArgumentException),
				() => {
					file.Save("");
				});

			Assert.Catch (
				typeof(ArgumentException),
				() => {
					JDataFile.Load(null);
				});

			Assert.Catch (
				typeof(ArgumentException),
				() => {
					JDataFile.Load("");
				});

			Assert.Catch (
				typeof(FileNotFoundException),
				() => {
					JDataFile.Load("./test.jdf");
				});
		}

		[Test ()]
		public void SaveLoadTest ()
		{
			string[] headerLine1 = { "col1", "col2", "col3" };
			string[] headerLine2 = { "col4", "col5", "col6", "col7" };

			string[] dataLine1 = { "cell11", "cell12", "cell13" };
			string[] dataLine2 = { "cell21", "cell22", "cell23", "cell24" };

			JDataFile originalFile = new JDataFile ();

			var headerRow1 = originalFile.CreateHeaderRow ();
			headerRow1.AddValues (headerLine1);

			var headerRow2 = originalFile.CreateHeaderRow ();
			headerRow2.AddValues (headerLine2);

			originalFile.CreateDataRow ().AddValues (dataLine1);
			originalFile.CreateDataRow ().AddValues (dataLine2);

			string fileName = System.IO.Path.GetTempFileName ();
			originalFile.Save (fileName);

			var newFile = JDataFile.Load (fileName);

			Assert.NotNull (newFile);
			Assert.AreEqual (originalFile.ColumnsCount, newFile.ColumnsCount);
			Assert.AreEqual (originalFile.RowsCount, newFile.RowsCount);
			Assert.AreEqual (originalFile [1] [1].Value, newFile [1] [1].Value);

			System.IO.File.Delete (fileName);
		}


	}
}


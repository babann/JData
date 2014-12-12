using System;
using System.IO;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;

namespace JData
{
	public class JDataWriter : IJDataWriter
	{
		internal const string DATA_ENTRY_NAME = "data";

		private IJDataFile _file;

		public JDataWriter ()
		{
		}

		public JDataWriter(IJDataFile file) { 
			_file = file;
		}

		public void Write(string fileName) {
			if (string.IsNullOrWhiteSpace (fileName))
				throw new ArgumentException ("File name can't be null or empty", "fileName");

			var file = JsonConvert.SerializeObject (_file);
			var fileBytes = System.Text.Encoding.Default.GetBytes (file);

			if (!Directory.Exists (Path.GetDirectoryName (fileName)))
				Directory.CreateDirectory (fileName);

			using (FileStream fsOut = File.Create (fileName)) {
				using (ZipOutputStream zipStream = new ZipOutputStream (fsOut)) {
					zipStream.SetLevel (9);

					ZipEntry newEntry = new ZipEntry (DATA_ENTRY_NAME);
					zipStream.PutNextEntry (newEntry);
					zipStream.Write (fileBytes, 0, fileBytes.Length);
					zipStream.CloseEntry ();
					zipStream.IsStreamOwner = true;
					zipStream.Close ();
				}
			}

		}
	}
}


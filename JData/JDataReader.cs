using System;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace JData
{
	public class JDataReader : IJDataReader
	{
		public JDataReader ()
		{
		}

		public IJDataFile Read(string fileName) {
			if (string.IsNullOrWhiteSpace (fileName))
				throw new ArgumentException ("File name can't be null or empty", "fileName");

			using (FileStream fs = File.OpenRead (fileName)) {
				ZipFile zf = new ZipFile (fs);
				string json = "";

				foreach (ZipEntry zipEntry in zf) {
					if (!zipEntry.IsFile || zipEntry.Name != JDataWriter.DATA_ENTRY_NAME) {
						continue;
					}

					byte[] buffer = new byte[zipEntry.Size];
					using (Stream zipStream = zf.GetInputStream (zipEntry)) {

						zipStream.Read (buffer, 0, (int)zipEntry.Size);
						json = System.Text.Encoding.Default.GetString (buffer);

						zipStream.Close ();
						break;
					}
				}

				return Newtonsoft.Json.JsonConvert.DeserializeObject<JDataFile> (json);
			}
		}
	}
}


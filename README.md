JData
=====

Simple library to read/write table data in json file compressed into zip.

If there is an XLSX format I'm wondering why there is no XLSJ?

At one of my projects we decided to refuse usage of xls/xlsx formats to store data because of huge file size 
and performance problems with these huge files. But since we still need to store it somewhere we decided to
introduce our own file format. OpenXML idea is very attractive except for one thing - there is XML (which
is bigger than JSON) and there is a lot of garbage inside output files. We need just column headers and the data.
To achieve that goal I implemented this small library.

Dependencies: Newtonsoft.JSON, ICSharpCode.SharpZipLib, NUnit for Tests

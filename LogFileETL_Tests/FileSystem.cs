using System;
using Xunit;

namespace LogFileETL_Tests
{
	public class FileSystem
	{
		/// <summary>
		/// Test GetFileKey method in LogFileETL.Helpers.FileSystem
		/// </summary>
		[Fact]
		public void GetFileKey()
		{
			var inputValue = "/system/path/to/file/with/key/of/users/samplefile.txt";

			var fileKey = LogFileETL.Helpers.FileSystem.GetFileKey(inputValue);

			Assert.Equal("users", fileKey);
		}
	}
}

using System.IO;

namespace LogFileETL.Helpers
{
	public static class FileSystem
	{
		public static string GetFileKey(string fullPath)
		{
			var logFilePathOnly = Path.GetDirectoryName(fullPath);

			var pathSegments = logFilePathOnly.Split(Path.DirectorySeparatorChar);

			var logFileKey = pathSegments[pathSegments.Length - 1];

			return logFileKey;
		}
	}
}
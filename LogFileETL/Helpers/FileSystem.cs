using System.IO;

namespace LogFileETL.Helpers
{
	public static class FileSystem
	{
		/// <summary>
		/// Extract file key value from full path.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns></returns>
		public static string GetFileKey(string fullPath)
		{
			var logFilePathOnly = Path.GetDirectoryName(fullPath);

			var pathSegments = logFilePathOnly.Split(Path.DirectorySeparatorChar);

			var logFileKey = pathSegments[pathSegments.Length - 1];

			return logFileKey;
		}
	}
}
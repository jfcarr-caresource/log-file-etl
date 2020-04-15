using System.IO;
using System.Text.RegularExpressions;

namespace LogFileETL.Helpers
{
	public static class FileSystem
	{
		/// <summary>
		/// Extract file identifiers from full path.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns></returns>
		public static Models.LogFileIdent GetFileIdentifiers(string fullPath)
		{
			var logFilePathOnly = Path.GetDirectoryName(fullPath);
			var logFileNameOnly = Path.GetFileName(fullPath);

			var pathSegments = logFilePathOnly.Split(Path.DirectorySeparatorChar);

			var processName = pathSegments[pathSegments.Length - 1];

			var rx = new Regex(@".*(?=_)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			var matches = rx.Matches(logFileNameOnly);
			var dataTypeDesc = (matches.Count > 0) ? matches[0].Value : string.Empty;

			var logFileIdent = new Models.LogFileIdent { ProcessName = processName, DataTypeDescription = dataTypeDesc };

			return logFileIdent;
		}
	}
}
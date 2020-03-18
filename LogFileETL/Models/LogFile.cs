namespace LogFileETL.Models
{
	/// <summary>
	/// Information about an input log file.
	/// </summary>
	public class LogFile
	{
		/// <summary>
		/// Full path to log file.
		/// </summary>
		public string FullPath;
	}

	/// <summary>
	/// Information about the contents of a log file.
	/// </summary>
	public class LogFileContents
	{
		/// <summary>
		/// Date and time of event.
		/// </summary>
		public string EventDateTime;

		/// <summary>
		/// Event type.
		/// </summary>
		public string EventType;

		/// <summary>
		/// Single line from log file.
		/// </summary>
		public string Details;
	}
}
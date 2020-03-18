namespace LogFileETL.Models
{
	/// <summary>
	/// Log file definition.  Specifies output types for input log file.
	/// </summary>
	public class LogFileDefinition
	{
		/// <summary>
		/// Log file key
		/// </summary>
		public string LogFileKey;

		/// <summary>
		/// Output in Splunk format?
		/// </summary>
		public bool SplunkOutput;

		/// <summary>
		/// Archive to database?
		/// </summary>
		public bool DatabaseOutput;
	}
}
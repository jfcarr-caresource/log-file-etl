namespace LogFileETL.Models
{
	public class LogFile
	{
		public string FullPath;
	}

	public class LogFileContents
	{
		public string EventDateTime;
		public string EventType;
		public string Details;
	}
}
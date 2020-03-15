namespace LogFileETL.BLL
{
	public static class DefinitionManager
	{
		public static Models.LogFileDefinition GetDefinition(string fileKey)
		{
			switch (fileKey)
			{
				case "member":
					return new Models.LogFileDefinition { LogFileKey = fileKey, SplunkOutput = true, DatabaseOutput = false };

				default:
					return new Models.LogFileDefinition { LogFileKey = fileKey, SplunkOutput = false, DatabaseOutput = false };
			}
		}
	}
}
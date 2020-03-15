namespace LogFileETL.BLL
{
	public static class DefinitionManager
	{
		public static Models.LogFileDefinition GetDefinition(string fileKey)
		{
			return DataAdapters.Definitions.GetDefinition(fileKey);
		}
	}
}
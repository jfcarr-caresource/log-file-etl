namespace LogFileETL.BLL
{
	public static class DefinitionManager
	{
		/// <summary>
		/// Retrieve log file definition.
		/// </summary>
		/// <param name="fileKey">Log file key</param>
		/// <returns>LogFileDefinition model instance</returns>
		public static Models.LogFileDefinition GetDefinition(string fileKey)
		{
			return DataAdapters.Definitions.GetDefinition(fileKey);
		}
	}
}
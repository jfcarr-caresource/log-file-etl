using System.IO;
using System.Data.SQLite;

namespace LogFileETL.DataAdapters
{
	public static class Definitions
	{
		public static Models.LogFileDefinition GetDefinition(string fileKey)
		{
			var returnModel = new Models.LogFileDefinition();

			var connectionString = "URI=file:" + Path.Combine("database/logetl.db");

			using (var connection = new SQLiteConnection(connectionString))
			{
				connection.Open();

				var statement = $"SELECT * FROM definitions WHERE logfilekey = '{fileKey}'";

				using (var command = new SQLiteCommand(statement, connection))
				{
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							returnModel.LogFileKey = reader.GetString(0);
							returnModel.SplunkOutput = reader.GetBoolean(1);
							returnModel.DatabaseOutput = reader.GetBoolean(2);
						}
					}
				}
			}

			return returnModel;
		}
	}
}
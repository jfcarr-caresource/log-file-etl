using System;
using System.IO;
using System.Data.SQLite;

namespace LogFileETL.DataAdapters
{
	public static class Archive
	{
		/// <summary>
		/// Read LogFile contents and write to database archive table.
		/// </summary>
		/// <param name="logFileInfo"></param>
		/// <returns></returns>
		public static bool DatabaseOutput(Models.LogFile logFileInfo)
		{
			try
			{
				var connectionString = "URI=file:" + Path.Combine("database/logetl.db");

				using (var connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (System.IO.StreamReader inputFile = new System.IO.StreamReader(logFileInfo.FullPath))
					{
						string line;
						var dropDate = System.DateTime.Now.ToString();

						while ((line = inputFile.ReadLine()) != null)
						{
							using (var command = new SQLiteCommand(connection))
							{
								command.CommandText = $"INSERT INTO archive (sourcefile,dropdate,fileentry) VALUES ('{logFileInfo.FullPath}','{dropDate}','{line}')";
								command.ExecuteNonQuery();
							}
						}
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				BLL.Logger.LogMessage(ex);
				return false;
			}
		}
	}
}
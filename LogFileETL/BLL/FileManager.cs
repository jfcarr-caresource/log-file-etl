using System;
using System.IO;
using Newtonsoft.Json;

namespace LogFileETL.BLL
{
	public class FileManager
	{
		/// <summary>
		/// Determine which output formats need to be generated.
		/// </summary>
		/// <param name="logFileInfo"></param>
		/// <returns></returns>
		public string FileProcessor(Models.LogFile logFileInfo)
		{
			try
			{
				if (System.IO.File.Exists(logFileInfo.FullPath))
				{
					var logFileIdent = Helpers.FileSystem.GetFileIdentifiers(logFileInfo.FullPath);
					var logFileDefinition = DefinitionManager.GetDefinition(logFileIdent.ProcessName);

					if (logFileDefinition.SplunkOutput)
					{
						SplunkOutput(logFileInfo);
					}

					if (logFileDefinition.DatabaseOutput)
					{
						DatabaseOutput(logFileInfo);
					}

					return $"File processed: {logFileInfo.FullPath}";
				}
				else
				{
					return $"File not found";
				}
			}
			catch (Exception ex)
			{
				Logger.LogMessage(ex);

				return ex.Message;
			}
		}

		/// <summary>
		/// Generate Splunk-friendly output.
		/// </summary>
		/// <param name="logFileInfo"></param>
		/// <returns></returns>
		public string SplunkOutput(Models.LogFile logFileInfo)
		{
			try
			{
				var formattedLogPath = Path.Combine(Path.GetDirectoryName(logFileInfo.FullPath), "formatted");

				using (System.IO.StreamReader inputFile = new System.IO.StreamReader(logFileInfo.FullPath))
				{
					var di = Directory.CreateDirectory(formattedLogPath);

					formattedLogPath = Path.Combine(formattedLogPath, "SplunkReadyLog.json");

					using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(formattedLogPath))
					{
						string line;
						while ((line = inputFile.ReadLine()) != null)
						{
							var splitValues = line.Split("|");

							var lineItemModel = new Models.LogFileContents
							{
								EventDateTime = splitValues[0],
								EventType = splitValues[1],
								Details = splitValues[2]
							};
							var lineItemJson = JsonConvert.SerializeObject(lineItemModel);

							outputFile.WriteLine(lineItemJson);
						}
					}
				}

				return formattedLogPath;
			}
			catch (Exception ex)
			{
				Logger.LogMessage(ex);

				return string.Empty;
			}
		}

		/// <summary>
		/// Write log file contents to database.
		/// </summary>
		/// <param name="logFileInfo"></param>
		/// <returns></returns>
		public bool DatabaseOutput(Models.LogFile logFileInfo)
		{
			return DataAdapters.Archive.DatabaseOutput(logFileInfo);
		}
	}
}
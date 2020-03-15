using System;
using System.IO;
using Newtonsoft.Json;

namespace LogFileETL.BLL
{
	public class FileManager
	{
		public string FileProcessor(Models.LogFile logFileInfo)
		{
			try
			{
				if (System.IO.File.Exists(logFileInfo.FullPath))
				{
					var logFileKey = Helpers.FileSystem.GetFileKey(logFileInfo.FullPath);
					var logFileDefinition = DefinitionManager.GetDefinition(logFileKey);

					if (logFileDefinition.SplunkOutput)
					{
						SplunkOutput(logFileInfo);
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
	}
}
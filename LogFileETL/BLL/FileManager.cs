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
					string line;
					using (System.IO.StreamReader inputFile = new System.IO.StreamReader(logFileInfo.FullPath))
					{
						var formattedLogPath = Path.Combine(Path.GetDirectoryName(logFileInfo.FullPath), "formatted", "SplunkReadyLog.json");
						using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(formattedLogPath))
						{
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

					return $"File processed: {logFileInfo.FullPath}";
				}
				else
				{
					return $"File not found";
				}
			}
			catch (Exception ex)
			{
				var logger = new Logger();
				logger.LogMessage(ex);

				return ex.Message;
			}
		}
	}
}
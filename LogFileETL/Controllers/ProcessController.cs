using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace LogFileETL.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProcessController : ControllerBase
	{
		// POST process
		[HttpPost]
		public string Post([FromBody] Models.LogFile logFileInfo)
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
	}
}

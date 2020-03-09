using Microsoft.AspNetCore.Mvc;
using System.IO;

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
				System.IO.StreamReader inputFile = new System.IO.StreamReader(logFileInfo.FullPath);

				var formattedLogPath = Path.Combine(Path.GetDirectoryName(logFileInfo.FullPath), "formatted", "SplunkReadyLog.json");
				using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(formattedLogPath))
				{
					while ((line = inputFile.ReadLine()) != null)
					{
						var jsonString = string.Empty;
						var fieldCounter = 1;
						var splitValues = line.Split("|");
						jsonString += "{ ";
						foreach (var splitValue in splitValues)
						{
							jsonString += $"\"Field {fieldCounter}\": \"{splitValue}\", ";
							fieldCounter++;
						}
						jsonString += " }  ";
						outputFile.WriteLine(jsonString);
					}
				}
				inputFile.Close();

				return $"File processed: {logFileInfo.FullPath}";
			}
			else
			{
				return $"File not found";
			}
		}
	}
}

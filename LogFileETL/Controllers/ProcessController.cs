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
			var fileManager = new BLL.FileManager();

			return fileManager.FileProcessor(logFileInfo);
		}
	}
}

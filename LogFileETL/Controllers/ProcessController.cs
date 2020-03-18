using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace LogFileETL.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProcessController : ControllerBase
	{
		/// <summary>
		/// POST LogFile request
		/// </summary>
		/// <param name="logFileInfo"></param>
		/// <returns>Status message</returns>
		[HttpPost]
		public string Post([FromBody] Models.LogFile logFileInfo)
		{
			var fileManager = new BLL.FileManager();

			return fileManager.FileProcessor(logFileInfo);
		}
	}
}

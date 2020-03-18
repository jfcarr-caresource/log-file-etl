using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace LogFileWatcher
{
	class Program
	{
		static void Main(string[] args)
		{
			var monitorTarget = GetSetting("MonitorPaths", "Drop");

			// instantiate the object
			var fileSystemWatcher = new FileSystemWatcher();

			fileSystemWatcher.Created += FileSystemWatcher_Created;
			fileSystemWatcher.Changed += FileSystemWatcher_Changed;
			fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
			fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

			fileSystemWatcher.IncludeSubdirectories = true;
			fileSystemWatcher.Path = monitorTarget;

			// Allow events to fire
			fileSystemWatcher.EnableRaisingEvents = true;

			Console.WriteLine("Listening...\n");
			Console.WriteLine("(Press [ENTER] to exit.)");
			Console.ReadLine();
		}

		/// <summary>
		/// Event triggered when a new log file is created/dropped.  It calls the LogFileETL service with the file details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A new file has been created - {e.FullPath}");
			Console.WriteLine($"Full path to the file is {Path.GetFullPath(e.FullPath)}");

			if (!e.FullPath.Contains("formatted"))
			{
				var requestBody = new Models.LogFile { FullPath = Path.GetFullPath(e.FullPath) };

				var client = new RestClient(GetSetting("ETLServiceConfig", "FullRestURL"));
				client.RemoteCertificateValidationCallback = (sender1, certificate, chain, sslPolicyErrors) => true;
				var request = new RestRequest(Method.POST);
				request.AddHeader("content-type", "application/json");
				request.AddJsonBody(requestBody);
				IRestResponse response = client.Execute(request);

				Console.WriteLine(response.Content);
			}
		}

		/// <summary>
		/// Event triggered when an existing log file is modified.
		/// </summary>
		/// <remarks>
		/// Nothing implemented here except a status message to the console.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A new file has been changed - {e.Name}");
		}

		/// <summary>
		/// Event triggered when an existing log file is deleted.
		/// </summary>
		/// <remarks>
		/// Nothing implemented here except a status message to the console.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A file has been deleted - {e.Name}");
		}

		/// <summary>
		/// Event triggered when an existing log file is renamed.
		/// </summary>
		/// <remarks>
		/// Nothing implemented here except a status message to the console.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A file has been renamed - {e.Name}");
		}

		/// <summary>
		/// Retrieve a setting value from appsettings.
		/// </summary>
		/// <param name="sectionName"></param>
		/// <param name="entryName"></param>
		/// <returns></returns>
		private static string GetSetting(string sectionName, string entryName)
		{
			var builder = new ConfigurationBuilder()
			   .SetBasePath(Directory.GetCurrentDirectory())
			   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			IConfigurationRoot configuration = builder.Build();

			return configuration.GetSection(sectionName).GetSection(entryName).Value;
		}
	}
}

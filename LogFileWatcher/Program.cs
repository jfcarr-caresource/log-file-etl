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

			fileSystemWatcher.Path = monitorTarget;

			// Allow events to fire
			fileSystemWatcher.EnableRaisingEvents = true;

			Console.WriteLine("Listening...\n");
			Console.WriteLine("(Press [ENTER] to exit.)");
			Console.ReadLine();
		}

		private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A new file has been created - {e.FullPath}");
			Console.WriteLine($"Full path to the file is {Path.GetFullPath(e.FullPath)}");

			var requestBody = new Models.LogFile { FullPath = Path.GetFullPath(e.FullPath) };

			var client = new RestClient(GetSetting("ETLServiceConfig", "FullRestURL"));
			client.RemoteCertificateValidationCallback = (sender1, certificate, chain, sslPolicyErrors) => true;
			var request = new RestRequest(Method.POST);
			request.AddHeader("content-type", "application/json");
			request.AddJsonBody(requestBody);
			IRestResponse response = client.Execute(request);

			Console.WriteLine(response.Content);
		}

		private static void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A new file has been changed - {e.Name}");
		}

		private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A file has been deleted - {e.Name}");
		}

		private static void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
		{
			Console.WriteLine($"A file has been renamed - {e.Name}");
		}

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

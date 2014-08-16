using System;
using System.Diagnostics;
using System.IO;

namespace Dota_Toolbox
{
	public static class Utils
	{
		public static void ExplorePath(string path)
		{
			if (Directory.Exists(path))
				Process.Start(path);
			else
				Console.WriteLine(path);
		}
	}
}
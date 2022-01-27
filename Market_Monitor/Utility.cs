using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Market_Monitor
{
	public class Utility
	{
		public static string CreateFile(string p_nameFolder, string p_nameFile, bool p_returnString = true)
		{
			string p_currentdDirectory = Directory.GetCurrentDirectory();
			if (!Directory.Exists(p_currentdDirectory + p_nameFolder))
			{
				Directory.CreateDirectory(p_currentdDirectory + p_nameFolder);
			}
			string p_pathFile = p_currentdDirectory + p_nameFolder + p_nameFile;
			return p_pathFile;
		}
		public static void PrintLogo()
		{
			string p_taskMonitor = @" __  __               _           _     __  __                _  _" + "\n"
								+ @"|  \/  |             | |         | |   |  \/  |              (_)| |" + "\n"
								+ @"| \  / |  __ _  _ __ | | __  ___ | |_  | \  / |  ___   _ __   _ | |_   ___   _ __" + "\n"
								+ @"| |\/| | / _` || '__|| |/ / / _ \| __| | |\/| | / _ \ | '_ \ | || __| / _ \ | '__|" + "\n"
								+ @"| |  | || (_| || |   |   < |  __/| |_  | |  | || (_) || | | || || |_ | (_) || |" + "\n"
								+ @"|_|  |_| \__,_||_|   |_|\_\ \___| \__| |_|  |_| \___/ |_| |_||_| \__| \___/ |_|" + "\n" + "\n";
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(p_taskMonitor);
			Console.ForegroundColor = ConsoleColor.White;
		}
		public static void PrintMenu(string p_menu)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(p_menu);
			Console.ForegroundColor = ConsoleColor.White;
		}
		public static void PrintList(string p_ListMenu)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(p_ListMenu);
			Console.ForegroundColor = ConsoleColor.White;

		}
		public static string ChooseOption()
		{
			
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("\nEscolha uma das Opções: ");
			string option = Console.ReadLine();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			return option;
		}

	}
}

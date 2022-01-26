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

	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Market_Monitor
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }

		public static User UserAuthentication(string p_fileUserConfigs)
		{
			Console.Write("Nome de Usuário: ");
			string p_userName = Console.ReadLine();
			string p_userPassword = string.Empty;
			Console.Write("Senha: ");
			while (true)
			{
				var key = System.Console.ReadKey(true);
				if (key.Key == ConsoleKey.Enter)
					break;
				p_userPassword += key.KeyChar;
			}

			User p_user = new User();
			p_user = ReadUserXML(p_fileUserConfigs);


			if (p_user.Name == p_userName && p_user.Password == p_userPassword)
			{
				return p_user;
			}
			return new User();
		}
		public static User ReadUserXML(string p_fileUserConfigs) 
		{
			User p_user = new User();
			try
			{
				XElement p_xmlReader = XElement.Load(p_fileUserConfigs);
				
				p_user.Id = int.Parse(p_xmlReader.Element("id").ToString());
				p_user.Name = p_xmlReader.Element("name").ToString();
				p_user.Password = p_xmlReader.Element("password").ToString();
				
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			return p_user; 
		}
		public static User CreateUser()
		{
			User p_user = new User();
			try
			{
				string p_currentdDirectory = Directory.GetCurrentDirectory();
				string p_foldercConfig = "/config";
				if (!Directory.Exists(p_currentdDirectory + p_foldercConfig))
				{
					Directory.CreateDirectory(p_currentdDirectory + p_foldercConfig);
				}
				p_foldercConfig = p_currentdDirectory + p_foldercConfig;

				string p_fileUserConfigs = p_foldercConfig + "/user.xml";

				Console.WriteLine("Criando Novo Usuário\n");
				Console.Write("Nome de Usuario: ");
				string p_userName = Console.ReadLine().Trim();
				string p_userPassword = string.Empty;
				string p_userRepeatPassword = string.Empty;
				while (true)
				{
					while (true)
					{

						Console.Write("Senha: ");
						while (true)
						{
							var key = System.Console.ReadKey(true);
							if (key.Key == ConsoleKey.Enter)
								break;
							p_userPassword += key.KeyChar;
						}
						Console.WriteLine("");
						Console.Write("Repetir senha: ");
						while (true)
						{
							var key = System.Console.ReadKey(true);
							if (key.Key == ConsoleKey.Enter)
								break;
							p_userRepeatPassword += key.KeyChar;
						}
						if (p_userRepeatPassword.Equals(p_userPassword))
							break;
						else
						{
							p_userPassword = string.Empty;
							p_userRepeatPassword = string.Empty;
						}
					}
					break;

				}

				
				p_user.Id = 1;
				p_user.Name = p_userName;
				p_user.Password = p_userPassword;
				using (XmlWriter p_xmlWriter = XmlWriter.Create(p_fileUserConfigs))
				{
					p_xmlWriter.WriteStartElement("user");
					p_xmlWriter.WriteElementString("id", "1");
					p_xmlWriter.WriteElementString("name", p_userName);
					p_xmlWriter.WriteElementString("password", p_userPassword);
					p_xmlWriter.WriteEndElement();
					p_xmlWriter.Flush();
				}
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			return p_user;
		}

	}
}

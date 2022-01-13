using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Market_Monitor
{
	class Program
	{
		
		static void Main(string[] args)
		{
			Console.WriteLine(@" __  __               _           _     __  __                _  _"+"\n"
							+ @"|  \/  |             | |         | |   |  \/  |              (_)| |"+"\n"
							+ @"| \  / |  __ _  _ __ | | __  ___ | |_  | \  / |  ___   _ __   _ | |_   ___   _ __"+"\n"
							+ @"| |\/| | / _` || '__|| |/ / / _ \| __| | |\/| | / _ \ | '_ \ | || __| / _ \ | '__|" + "\n"
							+ @"| |  | || (_| || |   |   < |  __/| |_  | |  | || (_) || | | || || |_ | (_) || |" + "\n"
							+ @"|_|  |_| \__,_||_|   |_|\_\ \___| \__| |_|  |_| \___/ |_| |_||_| \__| \___/ |_|");
			//Console.WriteLine("Market Monitor");
			while (true)
			{
				Console.WriteLine("\n***** _MENU_ *****\n1 - Finanças\n2 - FII´s\n Taxas e Indices BR");
				string option = Console.ReadLine();
				switch (option)
				{
					case "1":
						FinanceMenu();
						break;
					case "2":
						FiiMenu();
						break;
					case "3":
						//FiiMenu();
						break;
					default:
						break;
				}
			}
			
		}
		public static void CreateUser() 
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
			string p_userPassword = string.Empty ;
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

			using (XmlWriter p_xmlWriter = XmlWriter.Create(p_fileUserConfigs))
			{
				p_xmlWriter.WriteStartElement("user");
				p_xmlWriter.WriteElementString("name", p_userName);
				p_xmlWriter.WriteElementString("password", p_userPassword);
				p_xmlWriter.WriteEndElement();
				p_xmlWriter.Flush();
			}

			
		}
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
			using (XmlReader p_xmlReader = XmlReader.Create(p_fileUserConfigs)) 
			{
				p_user.Id = 1;
				p_user.Name = p_xmlReader.ReadToFollowing("name").ToString();
				p_user.Password = p_xmlReader.ReadToFollowing("password").ToString();
				if (p_userName.Trim().Equals(p_user.Name)
					&& p_userPassword.Equals(p_user.Password))
					return p_user;
				else
					return p_user;
			}
		}
		public static void FinanceMenu()
		{
			User p_user = new User();
			string p_currentdDirectory = Directory.GetCurrentDirectory();
			string p_foldercConfig = @"\config";
			string p_fileUserConfigs = p_currentdDirectory + p_foldercConfig + @"\user.xml";
			while (true)
			{
				while (true) 
				{
					if (!File.Exists(p_fileUserConfigs))
					{
						CreateUser();
						break;
					}
					else
					{
						break;
					}
				}
				p_user = UserAuthentication(p_fileUserConfigs);
				if (null != p_user)
				{
					Console.Clear();
					Console.WriteLine("Seja bem Vindo !"+ p_user.Name);
					break;
				}
				else
				{
					Console.WriteLine("Usuário ou Senha Inválidos");
					Console.Clear();
				}

			}
			
			
			Finance finance = new Finance();
			while (true)
			{
				Console.WriteLine("\n***** _FINANCE_MENU_ *****\n1 -  Calculo Reserva de Emergência");
				string option = Console.ReadLine();
				switch (option)
				{
					case "1":
						Console.WriteLine("Informe os seguintes valores\n\nSalário Mensal(1000.00) | Custo Mensal(1000.00) | Poupaça Mensal(10 %) | Meses Protegidos(12)");
						Console.WriteLine("Salário Mensal: ");
						double p_monthlySalary = double.Parse(Console.ReadLine());
						Console.WriteLine("Custo Mensal: ");
						double p_monthlyCost = double.Parse(Console.ReadLine());
						Console.WriteLine("Poupaça Mensal: ");
						double p_monthly_Savings = double.Parse(Console.ReadLine());
						Console.WriteLine("Meses Protegidos (Opcional, padrão 12): ");
						string p_numberMonths = Console.ReadLine();
						int p_monthsProtected = 12;
						if (!string.IsNullOrEmpty(p_numberMonths))
							p_monthsProtected = int.Parse(p_numberMonths);


						finance.CalculateEmergencyReserve(p_monthlySalary, p_monthlyCost, p_monthly_Savings, p_monthsProtected);
						break;
					default:
						break;
				}

			}
		}
		public static void FiiMenu()
		{
			try
			{
				while (true)
				{
					Console.WriteLine("1 - Listar FII´s");
					Console.WriteLine("2 - Listar FII´s por Vacância");
					Console.WriteLine("3 - Calcular Número Mágico Por Nome");
					Console.WriteLine("4 - Selecionar Fii");
					Console.WriteLine("5 - Filtar por Segmento");

					Fii fii = new Fii();
					string option = Console.ReadLine();
					List<Fii> l_fii = fii.GetFiis();
					if (option == "1")
					{
						Console.WriteLine("{0,10}\t{1,10}\t{2,20}\t{3,10}", "Name", "Average Vacancy", "Segment", "Price");
						foreach (var item in l_fii)
						{
							Console.WriteLine("{0,10}\t{1,10}\t{2,20}\t{3,10}", item.Name, item.AverageVacancy, item.Segment, item.Price);
						}
					}
					if (option == "2")
					{
						Console.WriteLine("Não disponivel");
					}
					if (option == "3")
					{
						Console.WriteLine("Nome do FII: ");
						string p_label = Console.ReadLine();
						double p_magicNumber = fii.CalculateMagicNumberByName(p_label, l_fii);
						Console.WriteLine($"NÚmero Mágico {p_label} - {p_magicNumber}");
					}
					if (option == "4")
					{
						Console.WriteLine("Nome do FII: ");
						string p_label = Console.ReadLine();

						if (!string.IsNullOrEmpty(p_label))
							fii.GetFiiByName(p_label, l_fii);
					}
					if (option == "5")
					{
						
					}

					Console.ReadKey();
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			
		}
	}
}

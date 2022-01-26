using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Market_Monitor
{
	public class Program
	{
		
		static void Main(string[] args)
		{
			Fii p_fii = new Fii();
			List<Fii> l_fii = p_fii.GetFiis();
			string p_taskMonitor = @" __  __               _           _     __  __                _  _" + "\n"
								+ @"|  \/  |             | |         | |   |  \/  |              (_)| |" + "\n"
								+ @"| \  / |  __ _  _ __ | | __  ___ | |_  | \  / |  ___   _ __   _ | |_   ___   _ __" + "\n"
								+ @"| |\/| | / _` || '__|| |/ / / _ \| __| | |\/| | / _ \ | '_ \ | || __| / _ \ | '__|" + "\n"
								+ @"| |  | || (_| || |   |   < |  __/| |_  | |  | || (_) || | | || || |_ | (_) || |" + "\n"
								+ @"|_|  |_| \__,_||_|   |_|\_\ \___| \__| |_|  |_| \___/ |_| |_||_| \__| \___/ |_|" + "\n" + "\n";
			
			//Console.WriteLine("Market Monitor");
			while (true)
			{
				Console.WriteLine(p_taskMonitor);
				Console.WriteLine("\n***** _MENU_ *****\n1 - Finanças\n2 - FII´s");
				//Console.WriteLine("\n***** _MENU_ *****\n1 - Finanças\n2 - FII´s\n Taxas e Indices BR");
				string option = Console.ReadLine();
				switch (option)
				{
					case "1":
						Console.Clear();
						Console.WriteLine(p_taskMonitor);
						FinanceMenu();
						break;
					case "2":
						Console.Clear();
						Console.WriteLine(p_taskMonitor);
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
		
		public static void FinanceMenu()
		{
			Fii p_fii = new Fii();
			List<Fii> l_fii = p_fii.GetFiis();

			User p_user = new User();
			string p_currentdDirectory = Directory.GetCurrentDirectory();
			string p_foldercConfig = @"\config";
			string p_folderFinance = @"\finance";
			string p_fileUserConfigs = p_currentdDirectory + p_foldercConfig + @"\user.xml";
			while (true)
			{
				while (true) 
				{
					if (!File.Exists(p_fileUserConfigs))
					{
						p_user = User.CreateUser();
						break;
					}
					else
						break;
					
				}
				//p_user = User.ReadUserXML(p_fileUserConfigs);
				Console.WriteLine("*** _Login_ ***");
				p_user = User.UserAuthentication(p_fileUserConfigs);
				if (null != p_user.Name)
				{
					Console.Clear();
					Console.WriteLine("Seja bem Vindo !"+ p_user.Name);
					break;
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Usuário ou Senha Inválidos");
					
				}

			}
			
			
			Finance finance = new Finance();
			while (true)
			{
				Console.WriteLine("\n***** _FINANCE_MENU_ *****\n1 -  Calculo Reserva de Emergência\n2 -  Minha Carteira");
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
					case "2":
						string p_fileUserWllet = p_currentdDirectory + p_folderFinance + @"\wallet.json";
						if (!File.Exists(p_fileUserWllet))
						{
							Console.WriteLine("Deseja Criar Suar Carteira de investimentos ? S/n");
							string p_option = Console.ReadLine();
							if (!string.IsNullOrWhiteSpace(p_option))
								if (p_option.ToLower().Equals("s"))
								{
									Console.WriteLine("Adicionar FII´s: ");
									Console.Write("Escolha ex (BCFF11): ");
									string p_nameFii = Console.ReadLine();

									p_fii = p_fii.GetFiiByName(p_nameFii, l_fii);
								}
								else
									break;
						}
						break;
					default:
						break;
				}

			}
		}
		public static void FiiMenu()
		{
			Fii p_fii = new Fii();
			List<Fii> l_fii = p_fii.GetFiis();
			try
			{
				while (true)
				{
					Console.WriteLine("1 - Listar FII´s");
					Console.WriteLine("2 - Listar FII´s por Vacância");
					Console.WriteLine("3 - Calcular Número Mágico Por Nome");
					Console.WriteLine("4 - Selecionar Fii");
					Console.WriteLine("5 - Filtar por Segmento");

					string option = Console.ReadLine();
					
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
						Console.Write("\nNome do FII ex ( BCFF11 ): ");
						string p_label = Console.ReadLine();
						double p_magicNumber = p_fii.CalculateMagicNumberByName(p_label, l_fii);
						Console.WriteLine($"\nNúmero Mágico {p_label} - {p_magicNumber}");
					}
					if (option == "4")
					{
						Console.Write("\nNome do FII ex( BCFF11 ): ");
						string p_label = Console.ReadLine();

						if (!string.IsNullOrEmpty(p_label))
							p_fii.GetFiiByName(p_label, l_fii);
					}
					if (option == "5")
					{
						
					}
					Console.WriteLine("\n\nPressione E para sair ou qualquer tecla para voltar ao Menu");
					string p_exit = Console.ReadKey().Key.ToString();
					if (p_exit.ToLower().Equals("e"))
					{
						Console.Clear();
						break;
					}
					else
					{
						Console.Clear();
					}
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			
		}
	}
}

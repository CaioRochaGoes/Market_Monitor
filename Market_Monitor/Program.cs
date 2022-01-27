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
			Console.Title = "Market_Monitor";
			Fii p_fii = new Fii();
			List<Fii> l_fii = Fii.GetFiis();
			
			
			//Console.WriteLine("Market Monitor");
			while (true)
			{
				Utility.PrintLogo();
				Utility.PrintMenu("\n***** _MENU_ *****");
				Utility.PrintList("\n1 - Finanças\n2 - FII´s");
				//Console.WriteLine("\n***** _MENU_ *****\n1 - Finanças\n2 - FII´s\n Taxas e Indices BR");
				switch (Utility.ChooseOption())
				{
					case "1":
						Utility.PrintLogo();
						FinanceMenu();
						break;
					case "2":
						Utility.PrintLogo();
						FiiMenu();
						break;
					default:
						Console.Clear();
						Utility.PrintMenu("\n***** _EM CONSTRUÇÃO_ *****");
						Console.ReadKey();
						break;
				}
			}
			
		}

		public static void FinanceMenu()
		{
			Fii p_fii = new Fii();
			//Mudar para GetFiisFromFile()
			//List<Fii> l_fii = p_fii.GetFiis();
			List<Fii> l_fii = new List<Fii>();

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
				Utility.PrintMenu("*** _Login_ ***");
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
				Utility.PrintLogo();
				Utility.PrintMenu("\n***** _FINANCE_MENU_ *****");
				Utility.PrintList("\n1 - Calculo Reserva de Emergência\n2 - Minha Carteira");
				switch (Utility.ChooseOption())
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
			//Mudar para GetFiisFromFile()
			//List<Fii> l_fii = p_fii.GetFiis();
			List<Fii> l_fii = new List<Fii>();
			try
			{
				while (true)
				{
					Utility.PrintLogo();
					Utility.PrintMenu("\n***** _FII´s_MENU_ *****");
					Utility.PrintList("\n1 - Listar FII´s\n2 - Listar FII´s por Vacância\n3 - Calcular Número Mágico Por Nome\n4 - Selecionar FII\n5 - Filtar por Segmento");
					string p_label = string.Empty;
					bool p_break = false;
					switch (Utility.ChooseOption())
					{
						case "1":
							Fii.PrintFIIs();
							break;
						case "2":
							Console.WriteLine("Indisponivel Temporariamente");
							Console.ReadKey();
							break;
						case "3":
							Console.Write("\nNome do FII ex ( BCFF11 ): ");
							p_label = Console.ReadLine();
							double p_magicNumber = p_fii.CalculateMagicNumberByName(p_label, l_fii);
							Console.WriteLine($"\nNúmero Mágico {p_label} - {p_magicNumber}");
							Console.ReadKey();
							break;
						case "4":
							Console.Write("\nNome do FII ex( BCFF11 ): ");
							p_label = Console.ReadLine();
							if (!string.IsNullOrEmpty(p_label))
								p_fii.GetFiiByName(p_label, l_fii);
							Console.ReadKey();
							break;
						case "5":
							Console.WriteLine("Indisponivel Temporariamente");
							Console.ReadKey();
							break;
						case "e":
							Console.Clear();
							p_break = true;
							break;
						default:
							Console.Clear();
							break;
					}
					if (p_break)
						break;

					#region old
					//string option = Console.ReadLine();

					//if (option == "1")
					//{

					//}
					//if (option == "2")
					//{
					//	Console.WriteLine("Não disponivel");
					//}
					//if (option == "3")
					//{
					//	Console.Write("\nNome do FII ex ( BCFF11 ): ");
					//	string p_label = Console.ReadLine();
					//	double p_magicNumber = p_fii.CalculateMagicNumberByName(p_label, l_fii);
					//	Console.WriteLine($"\nNúmero Mágico {p_label} - {p_magicNumber}");
					//}
					//if (option == "4")
					//{
					//	Console.Write("\nNome do FII ex( BCFF11 ): ");
					//	string p_label = Console.ReadLine();

					//	if (!string.IsNullOrEmpty(p_label))
					//		p_fii.GetFiiByName(p_label, l_fii);
					//}
					//if (option == "5")
					//{

					//}
					//Console.WriteLine("\n\nPressione E para sair ou qualquer tecla para voltar ao Menu");
					//string p_exit = Console.ReadKey().Key.ToString();
					//if (p_exit.ToLower().Equals("e"))
					//{
					//	Console.Clear();
					//	break;
					//}
					//else
					//{
					//	Console.Clear();
					//}
					#endregion
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
			
		}
	}
}

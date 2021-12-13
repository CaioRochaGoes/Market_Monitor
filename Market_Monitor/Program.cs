using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Market_Monitor
{
	class Program
	{
		
		static void Main(string[] args)
		{
			Console.WriteLine("Market Monitor");
			while (true)
			{
				Console.WriteLine("\n***** _MENU_ *****\n1 - Finanças\n2 - FII´s");
				string option = Console.ReadLine();
				switch (option)
				{
					case "1":
						FinanceMenu();
						break;
					case "2":
						FiiMenu();
						break;
					default:
						break;
				}
			}
			
		}
		public static void FinanceMenu()
		{
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
						int p_monthsProtected = int.Parse(Console.ReadLine());

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
					Console.WriteLine("9 - CalculateEmergencyReserve");
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

using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Market_Monitor
{
	public class Fii
	{
		#region property
		public string Name { get; set; }
		public string Segment { get; set; }
		public double Price { get; set; }
		public double FfoYield { get; set; }
		public double DividendYield { get; set; }
		public double P_VP { get; set; }
		public double MarketValue { get; set; }
		public double Liquidity { get; set; }
		public int RealEstateQuantity { get; set; }
		public double PricePerM2 { get; set; }
		public double RentPerM2 { get; set; }
		public double CapRate { get; set; }
		public double AverageVacancy { get; set; }
		public int Points { get; set; }

		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern IntPtr GetConsoleWindow();

		private static IntPtr ThisConsole = GetConsoleWindow();
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		private const int HIDE = 0;
		private const int MAXIMIZE = 3;
		private const int MINIMIZE = 6;
		private const int RESTORE = 9;

		#endregion

		#region methods
		public double RemoveCharacters(string text) {
			try
			{
				double p_number = 0;
				string p_stringNumber = string.Empty;
				char[] l_words = text.ToCharArray();
				foreach (var p_word in l_words)
				{
					string p_stringWord = p_word.ToString();
					if (Regex.IsMatch(p_stringWord.ToString(), "%"))
						p_stringWord = p_stringWord.Replace(Regex.Match(p_stringWord, "%").Value, "");
					if (Regex.IsMatch(p_stringWord.ToString(), @"\."))
						p_stringWord = p_stringWord.Replace(Regex.Match(p_stringWord, @"\.").Value, "");
					if (Regex.IsMatch(p_stringWord.ToString(), ","))
						p_stringWord = p_stringWord.Replace(Regex.Match(p_stringWord, ",").Value, ".");
					p_stringNumber += p_stringWord;
					
				}
				p_number = double.Parse(p_stringNumber, CultureInfo.InvariantCulture);
				return p_number;
				
			}
			catch (Exception ex)
			{
				//Console.WriteLine("Erro - RemoveCharacters - "+ ex.Message);
				return double.NaN;
			}
			
		}
		public static void PrintFIIs()
		{
			try
			{
				string p_folderFinance = @"\finance";
				string p_fileFii = @"\fii.json";
				FileInfo p_fileInfo = new FileInfo(Utility.CreateFile(p_folderFinance, p_fileFii));
				DateTime p_lastAccess = p_fileInfo.LastAccessTime;
				DateTime p_now = DateTime.Now;
				DayOfWeek p_dayOfToday = p_lastAccess.DayOfWeek;
				DayOfWeek p_lastAccesDay = p_lastAccess.DayOfWeek;
				TimeSpan p_timeDifference = p_now.Subtract(p_lastAccess);


				List<Fii> l_fiis = (
						(DayOfWeek.Saturday != p_dayOfToday && DayOfWeek.Sunday != p_dayOfToday) // Verifica se não é um dos dias do Final de Semana (Sabado/Domingo)
						&&	(p_timeDifference > new TimeSpan(0, 2, 0, 0, 0)) // Verifica se tem mais de 2h de diferença
						&&	(p_now.Hour < 18 && p_now.Hour > 10) // Verifica se está dentre o horário de funcionamento do mercado BR
					) 
					? GetFiis() 
					: GetFiisFromFile(p_fileInfo);
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("\n\n{0,10}\t{1,20}\t{2,10}\t{3,17}\t{4,10}\t{5,10}\t{6,10}", "Name", "Segment", "Price", "Average Vacancy", "Qtd Imoveis", "Preço por m2", "Aluguel por m2\n\n");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
				ShowWindow(ThisConsole, MAXIMIZE);
				foreach (var p_fii in l_fiis)
				{
					Console.WriteLine("{0,10}\t{1,20}\t{2,10}\t{3,10}\t{4,10}\t{5,10}\t{6,10}", p_fii.Name, p_fii.Segment, p_fii.Price, p_fii.AverageVacancy, p_fii.RealEstateQuantity, p_fii.PricePerM2, p_fii.RentPerM2);
				}
				Console.ReadKey();
				Console.ForegroundColor = ConsoleColor.White;


			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}

		} 
		//public void UpdateFIIsInJson()
		//{
		//	try
		//	{
		//		string p_folderFinance = @"\finance";
		//		string p_fileFii = @"\fii.json";
		//		string p_fileFiis = Utility.CreateFile(p_folderFinance, p_fileFii);
		//		FileInfo p_fileInfo = new FileInfo(p_fileFiis);
				
		//	}
		//	catch (Exception ex)
		//	{

		//		Console.WriteLine(ex.Message);
		//	}
			
		//}


		public static void SaveFIIsInJson(List<Fii> l_fiis)
		{
			try
			{
				string p_folderFinance = @"\finance";
				string p_fileFii = @"\fii.json";
				string p_fileFiis = Utility.CreateFile(p_folderFinance, p_fileFii);
				var p_json = JsonConvert.SerializeObject(l_fiis);
				File.WriteAllText(p_fileFiis, p_json);
				#region old
				//var p_itenJson = using (XmlWriter p_xmlWriter = XmlWriter.Create(p_fileFiis))
				//{


				//	//var p_dateTime = DateTime.Now;
				//	//p_xmlWriter.WriteStartElement("Date");
				//	//p_xmlWriter.WriteElementString("Update", p_dateTime.ToString());
				//	//p_xmlWriter.WriteEndElement();

				//	//Problema ao criar o XML com varios  WriteStartElement

				//	foreach (var p_fii in l_fiis)
				//	{
				//		p_xmlWriter.WriteStartElement(p_fii.Name.ToString());
				//		p_xmlWriter.WriteElementString("AverageVacancy", p_fii.AverageVacancy.ToString());
				//		p_xmlWriter.WriteElementString("CapRate", p_fii.CapRate.ToString());
				//		p_xmlWriter.WriteElementString("DividendYield", p_fii.DividendYield.ToString());
				//		p_xmlWriter.WriteElementString("FfoYield", p_fii.FfoYield.ToString());
				//		p_xmlWriter.WriteElementString("Liquidity", p_fii.Liquidity.ToString());
				//		p_xmlWriter.WriteElementString("MarketValue", p_fii.MarketValue.ToString());
				//		p_xmlWriter.WriteElementString("Name", p_fii.Name);
				//		p_xmlWriter.WriteElementString("Points", p_fii.Points.ToString());
				//		p_xmlWriter.WriteElementString("Price", p_fii.Price.ToString());
				//		p_xmlWriter.WriteElementString("PricePerM2", p_fii.PricePerM2.ToString());
				//		p_xmlWriter.WriteElementString("P_VP", p_fii.P_VP.ToString());
				//		p_xmlWriter.WriteElementString("RealEstateQuantity", p_fii.RealEstateQuantity.ToString());
				//		p_xmlWriter.WriteElementString("RentPerM2", p_fii.RentPerM2.ToString());
				//		p_xmlWriter.WriteElementString("Segment", p_fii.Segment);
				//		p_xmlWriter.WriteEndElement();

				//	}
				//	p_xmlWriter.Flush();
				//	Console.WriteLine("Finalizou Async SaveFIIsInXml");
				//}
				#endregion
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			
		}
		public static List<Fii> GetFiisFromFile(FileInfo p_fileInfo)
		{
			List<Fii> l_fiis = new List<Fii>();
			try
			{
				string p_JsonOfFiis = File.ReadAllText(p_fileInfo.FullName);
				l_fiis = JsonConvert.DeserializeObject<List<Fii>>(p_JsonOfFiis);
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			return l_fiis;
		}
		public static List<Fii> GetFiis()
		{
			try
			{
				var p_html = @"https://www.fundamentus.com.br/fii_resultado.php";

				HtmlWeb p_web = new HtmlWeb();

				var p_htmlDoc = p_web.Load(p_html);

				var p_bodyTable = p_htmlDoc.DocumentNode.SelectSingleNode("//body//div//table");
				p_htmlDoc = new HtmlDocument();
				p_htmlDoc.LoadHtml(p_bodyTable.OuterHtml);
				var p_trs = p_htmlDoc.DocumentNode.SelectNodes("//tr");
				int counter = 0;
				p_htmlDoc = new HtmlDocument();

				List<Fii> l_fiis = new List<Fii>();
				Console.WriteLine("Carregando FII´s");
				foreach (var item in p_trs)
				{

					if (counter > 0)
					{
						Fii p_fii = new Fii();
						p_htmlDoc.LoadHtml(item.OuterHtml);
						p_fii.Name = p_htmlDoc.DocumentNode.SelectSingleNode("//td//span//a").InnerText;
						p_fii.Segment = p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[contains(@ckass,'res_papel')][2]").InnerText;
						p_fii.Price = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[3]").InnerText);
						p_fii.FfoYield = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[4]").InnerText);
						p_fii.DividendYield = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[5]").InnerText);
						p_fii.P_VP = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[6]").InnerText);
						p_fii.MarketValue = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[7]").InnerText);
						p_fii.Liquidity = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[8]").InnerText);
						p_fii.RealEstateQuantity = int.Parse(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[9]").InnerText);
						p_fii.PricePerM2 = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[10]").InnerText);
						p_fii.RentPerM2 = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[11]").InnerText);
						p_fii.CapRate = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[12]").InnerText);
						p_fii.AverageVacancy = p_fii.RemoveCharacters(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[12]").InnerText);
						l_fiis.Add(p_fii);
					}
					counter += 1;
				}
				Console.WriteLine(l_fiis.Count + " FII´s Carregados");
				SaveFIIsInJson(l_fiis);
				return l_fiis;
			}
			catch (Exception ex)
			{
				//Console.WriteLine(ex.Message);
				throw ex;
			}
			
		}
		public double CalculateMagicNumberByName(string p_name, List<Fii> l_fii)
		{
			try
			{
				Fii fii = new Fii();
				fii = l_fii.Find(p => p.Name == p_name.ToUpper());
				double result = fii.Price/ fii.P_VP;
				return Math.Ceiling(result);
			}
			catch (Exception ex)
			{

				throw ex;
			}
			return double.NaN;
		}
		public Fii GetFiiByName(string p_name, List<Fii> m_fii)
		{
			Fii p_fii = new Fii();
			try
			{

				p_fii = m_fii.Find(p => p.Name == p_name.ToUpper());

				Console.WriteLine("{0,10}\t{1,20}\t{2,10}\t{3,10}\t{4,10}\t{5,10}\t{6,10}", "Name", "Segment", "Price", "Average Vacancy", "Qtd Imoveis", "Preço por m2", "Aluguel por m2");
				Console.WriteLine("{0,10}\t{1,20}\t{2,10}\t{3,10}\t{4,10}\t{5,10}\t{6,10}", p_fii.Name, p_fii.Segment, p_fii.Price, p_fii.AverageVacancy, p_fii.RealEstateQuantity, p_fii.PricePerM2, p_fii.RentPerM2);
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
			return p_fii;
		}
		public List<Fii> GetBestFii(List<Fii> m_fii)
		{
			List<Fii> l_fiis = new List<Fii>();
			
			foreach (var item in m_fii)
			{
				if (item.AverageVacancy < 15)
					item.Points += 1;
				
			}
			return l_fiis;
		}

		public void GetFiiBySegmnt(List<Fii> m_fii)
		{
			/*
			 Fazer busca de Fii por segmento, 
			 não pode ser busca do seguimento em forma literal
			*/


			//m_fii.Find(s)
		}
		#endregion
		public enum FiiSegment
		{
			Shoppings = 1,
			Hibrido = 2,
			TitulosValMob= 3,
			LajesCorporativas = 4,
			Logistica = 5,
			Residencial = 6,
			Outros = 7

		}
		//enum QualityClassifier
		//{
		//	good,
		//	medium,
		//	bad,
		//	veryBad
		//}
	}
}

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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
		public List<Fii> GetFiis()
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
		public void GetFiiByName(string p_name, List<Fii> m_fii)
		{
			Fii fii = m_fii.Find(p => p.Name == p_name.ToUpper());

			Console.WriteLine("{0,10}\t{1,20}\t{2,10}\t{3,10}\t{4,10}\t{5,10}\t{6,10}", "Name", "Segment","Price", "Average Vacancy", "Qtd Imoveis","Preço por m2","Aluguel por m2");
			Console.WriteLine("{0,10}\t{1,20}\t{2,10}\t{3,10}\t{4,10}\t{5,10}\t{6,10}", fii.Name, fii.Segment, fii.Price, fii.AverageVacancy, fii.RealEstateQuantity, fii.PricePerM2, fii.RentPerM2);
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

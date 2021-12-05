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

					//if (Regex.IsMatch(p_stringWord.ToString(), ","))
					//	p_stringWord = p_stringWord.Replace(Regex.Match(p_stringWord, ",").Value, ".");
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
		#endregion
		//enum QualityClassifier
		//{
		//	good,
		//	medium,
		//	bad,
		//	veryBad
		//}
	}
}

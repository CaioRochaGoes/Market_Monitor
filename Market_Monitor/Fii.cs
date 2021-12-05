using System;
using System.Collections.Generic;
using System.Text;

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
				double number = 0;

				return number;
				
			}
			catch (Exception ex)
			{
				Console.WriteLine("Erro - RemoveCharacters - "+ ex.Message);
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

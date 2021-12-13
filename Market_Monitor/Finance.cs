using System;
using System.Collections.Generic;
using System.Text;

namespace Market_Monitor
{
	public class Finance
	{
		public void CalculateEmergencyReserve(double p_monthlySalary, double p_monthlyCost, double p_monthly_Savings, int p_monthsProtected)
		{
			try
			{
				Console.Clear();
				//p_monthly_Savings = p_monthly_Savings / 100;
				p_monthsProtected = (p_monthsProtected >= 0) ? p_monthsProtected : 12;
				double p_emergencyReservePurpose = p_monthsProtected * p_monthlyCost;
				double p_monthlyProfitability = 0.40/ 100;
				//Console.WriteLine(p_monthlyProfitability);
				double p_annualProfitability = Math.Round((Math.Pow((1 + p_monthlyProfitability), 12) - 1)*100, 1);
				double p_valueMonthlySalary = ((p_monthlySalary * p_monthly_Savings) / 100);
				double p_lastFinalValue = 0;
				double p_finalValue = 0;
				double p_monthlyIncome = 0;


				Console.WriteLine("{0,10}\t{1,10}\t{2,20}\t{3,15}\t{4,10}\t{5,10}\t{6,10}", "Mês", "Valor Inicial", "Poupança Mensal", "Rentabilidade Mensal", "Valor Final", "Rendimento Mensal($)","Ano");
				for (int i = 0; p_emergencyReservePurpose > p_lastFinalValue; i++)
				{
					int p_mouth = i + 1;
					double p_year = Math.Round((p_mouth +0.0) / 12, 1);
					if (p_mouth == 1)
					{
						p_lastFinalValue = Math.Round((p_valueMonthlySalary * (1 + p_monthlyProfitability)), 2);
						Console.WriteLine("{0,10}\t{1,10}\t{2,15}\t{3,20}\t{4,15}\t{5,20}\t{6,19}", p_mouth, "-", p_valueMonthlySalary, p_monthlyProfitability * 100, p_lastFinalValue, "-" , p_year);
						p_lastFinalValue += p_valueMonthlySalary;
					}
					else
					{
						p_finalValue = Math.Round(p_lastFinalValue * (1 + p_monthlyProfitability), 2);
						p_monthlyIncome = Math.Round(p_finalValue - p_lastFinalValue, 2);
						p_lastFinalValue = Math.Round(p_finalValue + p_valueMonthlySalary, 2);

						Console.WriteLine("{0,10}\t{1,10}\t{2,15}\t{3,20}\t{4,15}\t{5,20}\t{6,19}", p_mouth, p_lastFinalValue, p_valueMonthlySalary, p_monthlyProfitability * 100, p_finalValue, p_monthlyIncome, p_year);

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

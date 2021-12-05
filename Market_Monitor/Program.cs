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
			foreach (var item in p_trs)
			{
				
				if (counter > 0 )
				{
					Fii p_fii = new Fii();
					p_htmlDoc.LoadHtml(item.OuterHtml);
					p_fii.Name = p_htmlDoc.DocumentNode.SelectSingleNode("//td//span//a").InnerText;
					p_fii.Segment = p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[contains(@ckass,'res_papel')][2]").InnerText;
					p_fii.Price = double.Parse(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[3]").InnerText);
					p_fii.FfoYield = double.Parse(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[4]").InnerText);
					p_fii.DividendYield = double.Parse(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[5]").InnerText);
					p_fii.P_VP = double.Parse(p_htmlDoc.DocumentNode.SelectSingleNode("//tr/td[6]").InnerText);
					Console.WriteLine($"Nome: {p_fii.Name}, Segmento: {p_fii.Segment}");
				}
				counter += 1;
			}
			
		}
	}
}

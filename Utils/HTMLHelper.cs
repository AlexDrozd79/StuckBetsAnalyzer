using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuckBetsAnalyzer.Utils
{
	public class HTMLHelper
	{
		public static string StartHeadAndBody(string htmlContent)
		{
			htmlContent += "<html>" + Environment.NewLine +
				"<head>" + Environment.NewLine + "<style> table { border: 1px double black; }" + Environment.NewLine +
				" .NoCreditCall { background-color: cornsilk; } " + Environment.NewLine +
				" .SessionError { background-color: chocolate; color: white; } " + Environment.NewLine +
				" .Unknown { background-color: crimson; color: white; } " + Environment.NewLine +
				" .MissingCloseround { background-color:floralwhite; }" + Environment.NewLine +
				"</style></head><body>" + Environment.NewLine;
			return htmlContent;
		}

		public static string StartTable(string htmlContent)
		{
			htmlContent += "<table border=1>" + Environment.NewLine;
			return htmlContent;
		}

		
		public static string StartRow(string htmlContent, string className)
		{
			htmlContent += "<tr class='" + className + "'>";
			return htmlContent;
		}


		public static string AddCell(string htmlContent, string cellValue)
		{
			htmlContent += "<td>" + cellValue + "</td>";
			return htmlContent;
		}

		public static string EndRow(string htmlContent)
		{
			htmlContent += "</tr>";
			return htmlContent;
		}

		public static string AddBR(string htmlContent)
		{
			htmlContent += "<br/>";
			return htmlContent;
		}

		public static string EndTable(string htmlContent)
		{
			htmlContent += "</table>";
			return htmlContent;
		}

		public static string EndBody(string htmlContent)
		{
			htmlContent += "</body></html>";
			return htmlContent;
		}


		
	}

}

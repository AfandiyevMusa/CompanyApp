using System;
namespace ServiceLayer.Helpers
{
	public static class Helper
	{
		public static void WriteWithColor(this ConsoleColor color, string words)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(words);
			Console.ResetColor();
		}
	}
}
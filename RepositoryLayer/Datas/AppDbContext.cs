using System;
namespace RepositoryLayer.Datas
{
	public static class AppDbContext<T>
	{
		public static List<T>? values;

		static AppDbContext()
		{
			values = new List<T>();
		}
	}
}


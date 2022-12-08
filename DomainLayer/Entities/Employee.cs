using System;
using DomainLayer.Common;

namespace DomainLayer.Entities
{
	public class Employee : BaseEntity
	{
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public int Age { get; set; }
		public string? Address { get; set; }
		public Department? Department { get; set; }
	}
}
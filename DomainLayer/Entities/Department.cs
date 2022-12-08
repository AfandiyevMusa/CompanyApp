using System;
using DomainLayer.Common;

namespace DomainLayer.Entities
{
	public class Department: BaseEntity
	{
		public string? Name { get; set; }
		public int Capacity { get; set; }
	}
}
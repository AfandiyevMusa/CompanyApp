﻿using System;
namespace RepositoryLayer.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string message) : base(message) { }
	}
}
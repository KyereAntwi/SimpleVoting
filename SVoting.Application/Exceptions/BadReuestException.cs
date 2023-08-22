using System;
namespace SVoting.Application.Exceptions;

public class BadReuestException : Exception
{
	public BadReuestException(string message) : base(message)
	{
	}
}


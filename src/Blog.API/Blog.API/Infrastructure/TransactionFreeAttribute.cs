using System;

namespace Blog.API.Infrastructure
{
	[AttributeUsage(AttributeTargets.Class)]
	public class TransactionFreeAttribute : Attribute
	{
		
	}
}
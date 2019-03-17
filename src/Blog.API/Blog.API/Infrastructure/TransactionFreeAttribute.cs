using System;

namespace Blog.API.Infrastructure
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class TransactionFreeAttribute : Attribute
	{
		
	}
}
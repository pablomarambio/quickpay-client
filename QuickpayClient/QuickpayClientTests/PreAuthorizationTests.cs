using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickpayClient.Data;
using QuickpayClientTests.Factories;

namespace QuickpayClient.Data.Tests
{
	[TestClass()]
	public class PreAuthorizationTests
	{
		[TestMethod()]
		public void PreAuthorizationTest()
		{
			PreAuthorizationFactory.Simple().Send();
			Assert.Fail();
		}
	}
}
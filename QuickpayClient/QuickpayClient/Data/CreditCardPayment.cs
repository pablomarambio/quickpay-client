using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data
{
	public struct CreditCardPayment
	{
		public decimal Amount { get; set; }
		public int Installments { get; set; }
		public int DeferredMonths { get; set; }
		public CurrencyType Currency { get; set; }
	}

	public enum CurrencyType
	{
		ChileanPeso,
		ArgentinianPeso,
		PeruvianSol,
		ColombianPeso,
		UsDollar
	}
}

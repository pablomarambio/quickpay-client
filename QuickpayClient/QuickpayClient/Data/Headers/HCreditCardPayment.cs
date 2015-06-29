using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data.Headers
{
	public class HCreditCardPayment
	{
		public CreditCardPayment PaymentData { get; set; }
		public static Quickpay.currency ConvertCurrencyType(CurrencyType c)
		{
			switch (c)
			{
				case CurrencyType.ArgentinianPeso:
					return Quickpay.currency.ARS;
				case CurrencyType.ChileanPeso:
					return Quickpay.currency.CLP;
				case CurrencyType.ColombianPeso:
					return Quickpay.currency.COP;
				case CurrencyType.PeruvianSol:
					return Quickpay.currency.PEN;
				default:
					return Quickpay.currency.USD;
			}
		}

		public Quickpay.cardPaymentHeader ToProxyObj()
		{
			return new Quickpay.cardPaymentHeader()
			{
				amount = PaymentData.Amount,
				deferred = PaymentData.DeferredMonths,
				installments = PaymentData.Installments,
				currency = ConvertCurrencyType(PaymentData.Currency)
			};
		}
	}
}

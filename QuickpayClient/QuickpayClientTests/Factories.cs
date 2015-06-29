using QuickpayClient.Data;
using QuickpayClient.Data.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClientTests
{
	namespace Factories
	{
		static class MerchantFactory
		{
			public static Merchant M1()
			{
				return new Merchant() {
					MerchantCode = "009948785",
					BranchCode = 100,
					TerminalCode = 101 };
			}

			public static HMerchant HM1(string transactionId = "103", string logicalId = "104")
			{
				return new HMerchant() {
					Sender = M1(),
					Timestamp = DateTime.Now,
					TransactionId = transactionId,
					LogicalId = logicalId };
			}
		}

		static class CardPaymentFactory
		{
			public static CreditCardPayment C1()
			{
				return new CreditCardPayment() {
					Amount = 100,
					Currency = CurrencyType.ChileanPeso,
					DeferredMonths = 0,
					Installments = 1 };
			}

			public static HCardPayment HC1()
			{
				return new HCardPayment() { PaymentData = C1() };
			}
		}

		static class PreAuthorizationFactory
		{
			public static PreAuthorization Simple()
			{
				return new PreAuthorization(
					"bbc5ea2f74d2da9377eacd2f19ac108fffe165558baaddcaf48267c7b0adc1c6",
					MerchantFactory.HM1(),
					CardPaymentFactory.HC1());
            }
		}
	}
}

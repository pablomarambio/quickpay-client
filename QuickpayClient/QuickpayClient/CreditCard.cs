using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient
{
	namespace Data
	{
		struct CreditCard
		{
			public long Pan { get; set; }
			public int Cvv { get; set; }
			public int Exp { get; set; }

			public static CreditCard Fail { get { return new CreditCard() { Pan = 4653751010397529, Cvv = 112, Exp = 1301 }; } }
		}

		struct Merchant
		{
			public string MerchantCode { get; set; }
			public int BranchCode { get; set; }
			public int TerminalCode { get; set; }
		}

		struct CreditCardPayment
		{
			public decimal Amount { get; set; }
			public int Installments { get; set; }
			public int DeferredMonths { get; set; }
			public CurrencyType Currency { get; set; }
		}

		enum CurrencyType {
			ChileanPeso,
			ArgentinianPeso,
			PeruvianSol,
			ColombianPeso,
			UsDollar
		}

		namespace Headers {
			delegate string ComputeSignatureDelegate();

			struct HMerchant
			{
				public Merchant Sender { get; set; }
				public DateTime Timestamp { get; set; }
				public string TransactionId { get; set; }
				public string LogicalId { get; set; }
				public event ComputeSignatureDelegate SignatureRequested;

				public Quickpay.merchantHeader ToProxyObj()
				{
					if (SignatureRequested == null)
						throw new Exception("Nobody listenting to SignatureRequested");
					return new Quickpay.merchantHeader()
					{
						merchantId = Sender.MerchantCode,
						branchId = Sender.BranchCode,
						terminalId = Sender.TerminalCode,
						timestamp = Timestamp,
						merchTxnId = TransactionId,
						signature = SignatureRequested(),
						logicalTxnId = LogicalId
					};
				}
			}

			struct HCardPayment
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

		abstract class Transaction {
			public Headers.HMerchant MerchantHeader { get; set; }
			public string AuthKey { get; set; }

			public Transaction(string authKey, Headers.HMerchant merchantHeader)
			{
				this.AuthKey = authKey;
				this.MerchantHeader = merchantHeader;
			}

			public abstract string TransactionName
			{
				get;
			}

			protected abstract string ComputeSignatureSeed();

			protected string ComputeSignature()
			{
				SHA256Managed crypt = new SHA256Managed();
				var seed = ComputeSignatureSeed();
				byte[] crypto = crypt.ComputeHash(
					Encoding.UTF8.GetBytes(seed),
					0,
					Encoding.UTF8.GetByteCount(seed));
				return Convert.ToBase64String(crypto);
			}
		}

		abstract class FinancialTransaction : Transaction
		{
			public Headers.HCardPayment CardPaymentHeader { get; set; }
			public FinancialTransaction(string authKey, Headers.HMerchant merchantHeader, Headers.HCardPayment cardPaymentHeader) :
				base(authKey, merchantHeader)
			{
				this.CardPaymentHeader = cardPaymentHeader;
			}

			protected override string ComputeSignatureSeed()
			{
				return String.Concat(
					TransactionName,
					MerchantHeader.Sender.MerchantCode,
					MerchantHeader.Sender.BranchCode,
					MerchantHeader.Sender.TerminalCode,
					MerchantHeader.Timestamp,
					MerchantHeader.TransactionId,
					CardPaymentHeader.PaymentData.Amount,
					Headers.HCardPayment.ConvertCurrencyType(CardPaymentHeader.PaymentData.Currency).ToString(),
					CardPaymentHeader.PaymentData.Installments,
					CardPaymentHeader.PaymentData.DeferredMonths,
					MerchantHeader.LogicalId,
					AuthKey
                );
			}
		}

		class PreAuthorization : FinancialTransaction
		{
			public PreAuthorization(string authKey, Headers.HMerchant merchantHeader, Headers.HCardPayment cardPaymentHeader) :
				base(authKey, merchantHeader, cardPaymentHeader)
			{ }

            public override string TransactionName { get { return "Preauthorization";  } }
		}
	}
}

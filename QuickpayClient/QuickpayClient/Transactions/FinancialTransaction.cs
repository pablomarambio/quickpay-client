using System;
using QuickpayClient.Data;
using QuickpayClient.Data.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Transactions
{
	public abstract class FinancialTransaction : Transaction
	{
		public HCreditCardPayment CardPaymentHeader { get; set; }
		public FinancialTransaction(string authKey, HMerchant merchantHeader, HCreditCardPayment cardPaymentHeader) :
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
				HCreditCardPayment.ConvertCurrencyType(CardPaymentHeader.PaymentData.Currency).ToString(),
				CardPaymentHeader.PaymentData.Installments,
				CardPaymentHeader.PaymentData.DeferredMonths,
				MerchantHeader.LogicalId,
				AuthKey
			);
		}
	}
}

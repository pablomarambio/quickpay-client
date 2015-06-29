using QuickpayClient.Data.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Transactions
{
	public class PreAuthorization : FinancialTransaction
	{
		public PreAuthorization(
			string authKey, 
			HMerchant merchantHeader, 
			HCreditCardPayment cardPaymentHeader,
			HUser userHeader) :
			base(authKey, merchantHeader, cardPaymentHeader, userHeader)
		{ }

		public override string TransactionName { get { return "Preauthorization"; } }

		protected override bool InternalSend(Quickpay.FinancialControllerClient cont)
		{
			var bh = new Quickpay.baseHeader();
			var clih = new Quickpay.creditLegalInfoHeader();
			cont.Preauthorization(
				new Quickpay.preAuthRequest() { customerEmailAddress = null, customerPhoneNumber = null, weddingCode = null, dispatchType = Quickpay.dispatchType.DESPACHO_DOMICILIO },
				MerchantHeader.ToProxyObj(),
				UserHeader.ToProxyObj(),
				null,
				CardPaymentHeader.ToProxyObj(),
				null, null, null,
				out bh,
				out clih);
			return bh.responseCode == 0;
		}
	}
}

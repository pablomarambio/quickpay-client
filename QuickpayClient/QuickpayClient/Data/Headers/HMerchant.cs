using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data.Headers
{
	public class HMerchant
	{
		public Merchant Sender { get; set; }
		public DateTime Timestamp { get; set; }
		public string TransactionId { get; set; }
		public string LogicalId { get; set; }
		public event Func<string> SignatureRequested;

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
}

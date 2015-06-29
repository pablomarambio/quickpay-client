using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data
{
	public struct Merchant
	{
		public string MerchantCode { get; set; }
		public int BranchCode { get; set; }
		public int TerminalCode { get; set; }
	}
}

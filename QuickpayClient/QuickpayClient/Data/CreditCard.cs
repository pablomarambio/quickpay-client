using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data
{
	public struct CreditCard
	{
		public long Pan { get; set; }
		public int Cvv { get; set; }
		public int Exp { get; set; }
	}
}

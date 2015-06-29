using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data
{
	public struct Card
	{
		public string Pan { get; set; }
		public string Cvv { get; set; }
		public string Exp { get; set; }
	}
}

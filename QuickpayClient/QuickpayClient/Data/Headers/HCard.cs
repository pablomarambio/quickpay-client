using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data.Headers
{
	public class HCard
	{
		public Card CardData { get; set; }

		public Quickpay.cardHeader ToProxyObj()
		{
			return new Quickpay.cardHeader()
			{
				pan = CardData.Pan,
				cvv = CardData.Cvv,
				expDate = CardData.Exp,
				qpCardId = null
			};
		}
	}
}

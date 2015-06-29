using QuickpayClient.Data.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Transactions
{
	public abstract class Transaction
	{
		public HMerchant MerchantHeader { get; set; }
		public string AuthKey { get; set; }

		public Transaction(string authKey, HMerchant merchantHeader)
		{
			this.AuthKey = authKey;
			this.MerchantHeader = merchantHeader;
			this.MerchantHeader.SignatureRequested += this.ComputeSignature;
		}

		public abstract string TransactionName
		{
			get;
		}

		protected abstract string ComputeSignatureSeed();

		public string ComputeSignature()
		{
			SHA256Managed crypt = new SHA256Managed();
			var seed = ComputeSignatureSeed();
			byte[] crypto = crypt.ComputeHash(
				Encoding.UTF8.GetBytes(seed),
				0,
				Encoding.UTF8.GetByteCount(seed));
			return Convert.ToBase64String(crypto);
		}

		public bool Send()
		{
			var cont = new Quickpay.FinancialControllerClient();
			return InternalSend(cont);
		}

		protected abstract bool InternalSend(Quickpay.FinancialControllerClient cont);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data.Headers
{
	public class HUser
	{
		public User UserInfo { get; set; }

		public static Quickpay.country ConvertCountryName(CountryName c)
		{
			switch (c)
			{
				default:
					return Quickpay.country.CL;
			}
		}

		public static Quickpay.documentType ConvertDocumentType(DocumentType c)
		{
			switch (c)
			{
				default:
					return Quickpay.documentType.RUT;
			}
		}

		public Quickpay.userHeader ToProxyObj()
		{
			return new Quickpay.userHeader()
			{
				country = ConvertCountryName(this.UserInfo.Country),
				countrySpecified = true,
				documentType = ConvertDocumentType(this.UserInfo.Document),
				documentTypeSpecified = true,
				documentId = this.UserInfo.DocumentId,
				qpUserId = this.UserInfo.QuickpayUserId
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickpayClient.Data
{
	public class User
	{
		public CountryName Country { get; set; }
		public DocumentType Document { get; set; }
		public string DocumentId { get; set; }
		public string QuickpayUserId { get; set; }
	}

	public enum CountryName
	{
		Chile,
		Argentina,
		Perú,
		Colombia
	}

	public enum DocumentType
	{
		RUT
	}
}

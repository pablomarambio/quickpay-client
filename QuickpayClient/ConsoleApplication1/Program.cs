using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("lorles");
            OT.Preauthorization p = new OT.Preauthorization();
            OT.preAuthRequest pr = new OT.preAuthRequest();
            OT.FinancialControllerClient fcc = new OT.FinancialControllerClient();
            var bh = new OT.baseHeader();
            var clih = new OT.creditLegalInfoHeader();
            fcc.Preauthorization(
                new OT.preAuthRequest() { weddingCode = null, customerEmailAddress = null, customerPhoneNumber = null, dispatchType = OT.dispatchType.DESPACHO_DOMICILIO },
                new OT.merchantHeader() { },
                new OT.userHeader() { },
                new OT.cardHeader() { },
                new OT.cardPaymentHeader() { },
                new OT.productListHeader() { },
                new OT.riskAssessmentHeader() { },
                new OT.purchaseOrderHeader() { },
                out bh,
                out clih);
            Console.ReadLine();
        }
    }
}

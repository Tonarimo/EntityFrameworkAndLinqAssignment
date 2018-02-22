using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAndLinqAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            APEntities db = new APEntities();

            var vendorsWithInvoices = (from v in db.Vendors
                           join i in db.Invoices
                           on v.VendorID equals i.VendorID
                           where i.PaymentTotal > 800
                           select v).ToList();

            Console.WriteLine("Vendors And their invoices: \n");
            foreach (Vendor ven in vendorsWithInvoices)
            {
                foreach (Invoice inv in ven.Invoices)
                {
                    Console.WriteLine("Vendor: " + ven.VendorName + "     "
                        + inv.InvoiceNumber + "     " + inv.PaymentTotal);
                }
            }

            Console.WriteLine("\nVendors: \n");
            var vendors = (from v in db.Vendors
                           where v.VendorState == "CA"
                           select v).ToList();

            foreach(Vendor vend in vendors)
            {
                Console.WriteLine(vend.VendorName);
            }

            Console.WriteLine("\nLine Item Amounts greater than 2000 and their vendors: \n");


            var lineItems = (from v in db.Vendors
                             join i in db.Invoices
                             on v.VendorID equals i.VendorID
                             join l in db.InvoiceLineItems
                             on i.InvoiceID equals l.InvoiceID
                             where l.InvoiceLineItemAmount > 2000
                             select new
                             {
                                 v.VendorName,
                                 l.InvoiceLineItemAmount,
                                 l.InvoiceLineItemDescription
                             }).ToList();

            foreach(var item in lineItems)
            {
                Console.WriteLine("Vendor Name: {0}     Line Item Amount: {1}     Description: {2} ",
                    item.VendorName, item.InvoiceLineItemAmount, item.InvoiceLineItemDescription);
            }
            
            Console.ReadKey();
        }
    }
}

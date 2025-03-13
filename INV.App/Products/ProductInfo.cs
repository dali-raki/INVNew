using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INV.App.Receipts;

namespace INV.App.Products
{
    public class ProductInfo
    {
        public Guid Id { get; set; }
        public string Designation { get; set; }
        public string UnitMeasure { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int TVA { get; set; }
        public Guid DefaultWareHouseId { get; set; }
        public int Rest { get; set; }
        public string WareHouse { get; set; }
        public List<ReceiptInfo> ReceiptInfos { get; set; }
    }
}
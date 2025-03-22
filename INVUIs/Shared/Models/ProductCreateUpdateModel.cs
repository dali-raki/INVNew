using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INVUIs.Products.ProductsModel;
using INVUIs.Purchases.PurchaseModels;

namespace INVUIs.Shared.Models
{
    public class ProductCreateUpdateModel
    {
        public ProductModel productModel { get; set; }

        public PurchaseProductModel purchaseProductModel { get; set; }
    }
}
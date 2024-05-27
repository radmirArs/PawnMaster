using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary
{
    public interface IProductService
    {
        event EventHandler<ProductEventArgs> ProductAdded;
        event EventHandler<ProductEventArgs> ProductPurchased;
        event EventHandler<ProductEventArgs> ProductCardOpened;

        void AddProduct(Product product);
        void PurchaseProduct(Product product, string dateSale, string priceSale, Employee employee);
        void FindProductAndOpenCard(Product product);
        List<Product> GetAllProducts();
    }

    public class ProductEventArgs : EventArgs
    {
        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary
{
    public interface IDataService
    {
        event EventHandler<ProductEventArgs> ProductAdded;
        event EventHandler<ProductEventArgs> ProductPurchased;
        event EventHandler<ProductEventArgs> ProductCardOpened;

        void AddProduct(Product product);
        void PurchaseProduct(Product product, string dateSale, string priceSale, Employee employee);
        Product FindProduct(Product product);
        List<Product> GetAvailableProducts();
        List<Product> GetSoldProducts();
    }

    public class ProductEventArgs : EventArgs
    {
        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary
{
    public class DataService : IDataService
    {
        public event EventHandler<ProductEventArgs> ProductAdded;
        public event EventHandler<ProductEventArgs> ProductPurchased;
        public event EventHandler<ProductEventArgs> ProductCardOpened;

        public void AddProduct(Product product)
        {
            if (!CheckRepeatEmpty(product))
            {
                ObjectControl.AddSerialize(product);
                OnProductAdded(new ProductEventArgs { Product = product });
            }
            else
            {
                throw new InvalidOperationException("Повторяются названия продукта и дата покупки");
            }
        }

        public void PurchaseProduct(Product product, string dateSale, string priceSale, Employee employee)
        {
            var products = ObjectControl.Deserialize<Product>();
            var foundProduct = products.FirstOrDefault(p => p.ProductName == product.ProductName && p.ProductDateBuy == product.ProductDateBuy);
            if (foundProduct != null)
            {
                foundProduct.IsSale = true;
                foundProduct.ProductPriceSale = priceSale;
                foundProduct.EmployeeName = employee.FullName;
                foundProduct.ProductDateSale = dateSale;
                ObjectControl.ListSerialize(products);
                OnProductPurchased(new ProductEventArgs { Product = foundProduct });
            }
        }

        public Product FindProduct(Product product)
        {
            return ProductControl.FindProduct(product);
        }

        public List<Product> GetAvailableProducts()
        {
            var products = ObjectControl.Deserialize<Product>();
            return products.Where(p => !p.IsSale).ToList();
        }

        public List<Product> GetSoldProducts()
        {
            var products = ObjectControl.Deserialize<Product>();
            return products.Where(p => p.IsSale).ToList();
        }

        private bool CheckRepeatEmpty(Product product)
        {
            var products = ObjectControl.Deserialize<Product>();
            return products.Any(p => p.ProductName == product.ProductName && p.ProductDateBuy == product.ProductDateBuy);
        }

        protected virtual void OnProductAdded(ProductEventArgs e)
        {
            ProductAdded?.Invoke(this, e);
        }

        protected virtual void OnProductPurchased(ProductEventArgs e)
        {
            ProductPurchased?.Invoke(this, e);
        }

        protected virtual void OnProductCardOpened(ProductEventArgs e)
        {
            ProductCardOpened?.Invoke(this, e);
        }
    }
}

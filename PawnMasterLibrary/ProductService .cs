using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnMasterLibrary
{
    public class ProductService : IProductService
    {
        public event EventHandler<ProductEventArgs> ProductAdded;
        public event EventHandler<ProductEventArgs> ProductPurchased;
        public event EventHandler<ProductEventArgs> ProductCardOpened;

        public void AddProduct(Product product)
        {
            ObjectControl.AddSerialize(product);
            OnProductAdded(new ProductEventArgs { Product = product });
        }

        public void PurchaseProduct(Product product, string dateSale, string priceSale, Employee employee)
        {
            var products = ObjectControl.Deserialize<Product>();
            var foundProduct = products.FirstOrDefault(p => p.Name == product.Name && p.DateBuy == product.DateBuy);
            if (foundProduct != null)
            {
                foundProduct.IsSale = true;
                foundProduct.PriceSale = priceSale;
                foundProduct.EmployeeName = employee.FullName;
                foundProduct.DateSale = dateSale;
                ObjectControl.ListSerialize(products);
                OnProductPurchased(new ProductEventArgs { Product = foundProduct });
            }
        }

        public void FindProductAndOpenCard(Product product)
        {
            string filePath = ObjectControl.SerializedFilesPath(typeof(Product).Name);
            List<Product> products = ObjectControl.Deserialize<Product>();
            Product findProduct = products.FirstOrDefault(productFind =>
                productFind.Name == product.Name &&
                productFind.Description == product.Description);
            OnProductCardOpened(new ProductEventArgs { Product = findProduct });
        }

        public List<Product> GetAllProducts()
        {
            var products = ObjectControl.Deserialize<Product>();
            return products;
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

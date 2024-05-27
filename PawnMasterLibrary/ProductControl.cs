namespace PawnMasterLibrary
{
    public static class ProductControl
    {
        public static Product FindProduct(Product productDesired)
        {
            string filePath = ObjectControl.SerializedFilesPath(typeof(Product).Name);
            List<Product> products = ObjectControl.Deserialize<Product>();
            Product findProduct = products.FirstOrDefault(product =>
                product.ProductName == productDesired.ProductName &&
                product.ProductDescription == productDesired.ProductDescription);
            return findProduct;
        }
    }
}

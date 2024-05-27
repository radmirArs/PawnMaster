using System.Windows;
using PawnMasterLibrary;
using System.IO;
using System.Windows.Media.Imaging;

namespace PawnMasterWPF
{
    public partial class ProductCardWindow : Window
    {
        public ProductCardWindow()
        {
            InitializeComponent();
        }

        public void FillingInData(Product selectedProduct)
        {
            string productName = selectedProduct.ProductName;
            string productDescription = selectedProduct.ProductDescription;
            byte[] productImageData = selectedProduct.ProductImageData;

            if (productImageData != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(productImageData);
                bitmapImage.EndInit();
                ImageCard.Source = bitmapImage;
            }

            if (!string.IsNullOrEmpty(productName))
                NameProduct.Content = productName;

            if (!string.IsNullOrEmpty(productDescription))
                DescriptionProducText.Text = productDescription;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

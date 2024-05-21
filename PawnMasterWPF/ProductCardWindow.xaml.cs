using ModelForPawnMaster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PawnMasterWPF
{
    /// <summary>
    /// Логика взаимодействия для ProductCardWindow.xaml
    /// </summary>
    public partial class ProductCardWindow : Window
    {
        public ProductCardWindow()
        {
            InitializeComponent();

        }

        public void FillingInData(Product selectProduct)
        {
            string productName = selectProduct.ProductName;
            string productDescription = selectProduct.ProductDescription;
            byte[] productImageData = selectProduct.ProductImageData;
            if (productImageData != null)
            {
                byte[] decodedBytes = productImageData;
                // Создание изображения из массива байтов
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(decodedBytes);
                bitmapImage.EndInit();
                ImageCard.Source = bitmapImage;
            }
            if(productName != null)
                NameProduct.Content = productName;
            if (productDescription != null)
                DescriptionProduct.Content = productDescription;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

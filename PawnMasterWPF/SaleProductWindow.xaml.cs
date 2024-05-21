using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SaleProductWindow.xaml
    /// </summary>
    public partial class SaleProductWindow : Window
    {
        public string Price { get; private set; }
        public string Date { get; private set; }
        public SaleProductWindow()
        {
            InitializeComponent();
        }

        private void SaleProduct_click(object sender, RoutedEventArgs e)
        {
            Price = PriceSaleProductTextBox.Text;
            Date = PriceSaleProductTextBox.Text;
            Close();
        }
    }
}

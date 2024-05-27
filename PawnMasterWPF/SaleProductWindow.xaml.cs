using System.Windows;
using System;

namespace PawnMasterWPF
{
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
            Date = DateSaleProductTextBox.Text;
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(Price))
                errorMessage += "Заполните поле \"Цена\"" + '\n';

            if (string.IsNullOrWhiteSpace(Date))
                errorMessage += "Заполните поле \"Дата\"" + '\n';

            if (errorMessage == "")
                Close();
            else
                MessageBox.Show(errorMessage);
        }
    }
}

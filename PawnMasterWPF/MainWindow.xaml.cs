using System.Windows;
using PawnMasterLibrary;
using Microsoft.Win32;
using System.IO;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media;

namespace PawnMasterWPF;

public partial class MainWindow : Window
{
    string _role;
    byte[] _dataImage;
    private Employee loggedEmployee;
    private IDataService dataService;

    public MainWindow()
    {
        InitializeComponent();
        dataService = new DataService();
        dataService.ProductAdded += DataService_ProductAdded;
        dataService.ProductPurchased += DataService_ProductPurchased;
    }

    private void DataService_ProductAdded(object sender, ProductEventArgs e)
    {
        MessageBox.Show("Продукт успешно добавлен");
    }

    private void DataService_ProductPurchased(object sender, ProductEventArgs e)
    {
        MessageBox.Show("Продукт успешно продан");
    }

    private void ImageAdd_Click(object sender, RoutedEventArgs e)
    {
        ReadSelectedImagePath();
    }

    void ReadSelectedImagePath()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

        if (openFileDialog.ShowDialog() == true)
        {
            string selectedImagePath = openFileDialog.FileName;
            _dataImage = File.ReadAllBytes(selectedImagePath);
        }
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        LoginWindow loginWindow = new LoginWindow();
        loginWindow.Show();
        Close();
    }

    private void AdminPanel_Click(object sender, RoutedEventArgs e)
    {
        if (_role == "A")
        {
            AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
            adminPanelWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Недостаточно прав");
        }
    }

    public void LoggedUserAdd(Employee LoggedEmployee)
    {
        loggedEmployee = LoggedEmployee;
        NameUser.Text = loggedEmployee.FullName;
        _role = "U";
    }

    public void LoggedAdmin()
    {
        NameUser.Text = "Админ";
        _role = "A";
        AddAdminPanelButton();
    }

    private void AddAdminPanelButton()
    {
        bool conditionMet = true;

        if (conditionMet)
        {
            Button adminPanelButton = new Button
            {
                Height = 60,
                Width = 230,
                Content = "AdminPanel",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                FontSize = 40,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("SlateGray")),
                Foreground = new SolidColorBrush(Colors.White),
            };

            adminPanelButton.Click += AdminPanel_Click;

            var borderStyle = new Style(typeof(Border));
            borderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
            borderStyle.Setters.Add(new Setter(Border.BorderBrushProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("SlateGray"))));
            borderStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(5)));

            adminPanelButton.Resources.Add(typeof(Border), borderStyle);

            FirstPageGrid.Children.Add(adminPanelButton);
            Grid.SetColumn(adminPanelButton, 0);
            Grid.SetRow(adminPanelButton, 2);
        }
    }

    private void ProductAdd_Click(object sender, RoutedEventArgs e)
    {
        string productName = NameItemTextBox.Text;
        string productDateBuy = ProductDateBuyPicker.Text;
        string productPrice = ProductPriceTextBox.Text;
        string productDescription = DescriptionTextBox.Text;
        byte[] dataImage = _dataImage;

        if (CheckForNullAndSpaces(productName, productDateBuy, productPrice, productDescription, dataImage))
        {
            var newProduct = new Product()
            {
                ProductName = productName,
                ProductDateBuy = productDateBuy,
                ProductDescription = productDescription,
                ProductImageData = dataImage,
                ProductPriceBuy = productPrice,
                IsSale = false
            };
            try
            {
                dataService.AddProduct(newProduct);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    private bool CheckForNullAndSpaces(string productName, string productDateBuy, string productPrice, string productDescription, byte[] dataImage)
    {
        string errorMessage = "";

        if (string.IsNullOrWhiteSpace(productName))
            errorMessage += "Название продукта не может быть пустым.\n";

        if (string.IsNullOrWhiteSpace(productDateBuy))
            errorMessage += "Дата покупки продукта не может быть пустой.\n";

        if (string.IsNullOrWhiteSpace(productPrice))
            errorMessage += "Цена продукта не может быть пустой.\n";

        if (string.IsNullOrWhiteSpace(productDescription))
            errorMessage += "Описание продукта не может быть пустым.\n";

        if (dataImage == null || dataImage.Length == 0)
            errorMessage += "Не добавлено фото продукта.\n";

        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            MessageBox.Show(errorMessage);
            return false;
        }
        return true;
    }

    private void ProductAvailabilityDataGrid_OnLoaded(object sender, RoutedEventArgs e)
    {
        var products = dataService.GetAvailableProducts();
        ProductAvailabilityDataGrid.ItemsSource = products;
    }

    private void OpenCardProduct_click(object sender, RoutedEventArgs e)
    {
        var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
        var findProduct = dataService.FindProduct(selectedProduct);
        if (findProduct != null)
        {
            ProductCardWindow productCardWindow = new ProductCardWindow();
            productCardWindow.FillingInData(findProduct);
            productCardWindow.ShowDialog();
        }
    }

    private void PurchaseProduct_click(object sender, RoutedEventArgs e)
    {
        var saleProductWindow = new SaleProductWindow();
        saleProductWindow.ShowDialog();
        string dateSale = saleProductWindow.Date;
        string priceSale = saleProductWindow.Price;
        var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
        if (selectedProduct != null)
        {
            dataService.PurchaseProduct(selectedProduct, dateSale, priceSale, loggedEmployee);
        }
    }

    private void salesDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        var products = dataService.GetSoldProducts();
        salesDataGrid.ItemsSource = products;
    }
}
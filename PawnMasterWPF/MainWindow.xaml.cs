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
    private IProductService productService;

    public MainWindow()
    {
        InitializeComponent();
        productService = new ProductService();
        productService.ProductAdded += ProductAddedService;
        productService.ProductPurchased += ProductPurchasedService;
        productService.ProductCardOpened += ProductOpenCardService;
    }

    private void ProductAddedService(object sender, ProductEventArgs e)
    {
        MessageBox.Show("Продукт успешно добавлен");
    }

    private void ProductPurchasedService(object sender, ProductEventArgs e)
    {
        MessageBox.Show("Продукт успешно продан");
    }

    void ProductOpenCardService(object sender, ProductEventArgs e)
    {
        ProductCardWindow productCardWindow = new ProductCardWindow();
        productCardWindow.FillingInData(e.Product);
        productCardWindow.ShowDialog();
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
                Name = productName,
                DateBuy = productDateBuy,
                Description = productDescription,
                ImageData = dataImage,
                PriceBuy = productPrice,
                IsSale = false
            };
            try
            {
                productService.AddProduct(newProduct);
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
            errorMessage += "Название товара не может быть пустым.\n";

        if (string.IsNullOrWhiteSpace(productDateBuy))
            errorMessage += "Дата покупки товара не может быть пустой.\n";

        if (string.IsNullOrWhiteSpace(productPrice))
            errorMessage += "Цена товара не может быть пустой.\n";

        if (string.IsNullOrWhiteSpace(productDescription))
            errorMessage += "Описание товара не может быть пустым.\n";

        if (dataImage == null || dataImage.Length == 0)
            errorMessage += "Не добавлено фото товара.\n";

        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            MessageBox.Show(errorMessage);
            return false;
        }
        return true;
    }

    private void ProductAvailabilityDataGrid_OnLoaded(object sender, RoutedEventArgs e)
    {
        var products = productService.GetAllProducts();
        var availabilityProducts = products.Where(p => !p.IsSale).ToList();
        ProductAvailabilityDataGrid.ItemsSource = availabilityProducts;
    }

    private void OpenCardProduct_click(object sender, RoutedEventArgs e)
    {
        var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
        if(selectedProduct != null)
            productService.FindProductAndOpenCard(selectedProduct);
    }

    private void PurchaseProduct_click(object sender, RoutedEventArgs e)
    {
        if (_role == "U")
        {
            var saleProductWindow = new SaleProductWindow();
            saleProductWindow.ShowDialog();
            string dateSale = saleProductWindow.Date;
            string priceSale = saleProductWindow.Price;
            var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
            if (selectedProduct != null)
            {
                productService.PurchaseProduct(selectedProduct, dateSale, priceSale, loggedEmployee);
            }
        }
        else
            MessageBox.Show("Админ не может продавать товар");
        
    }

    private void salesDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        var products = productService.GetAllProducts();
        var salesProducts = products.Where(p => p.IsSale).ToList();
        salesDataGrid.ItemsSource = salesProducts;
    }
}
using System.Windows;
using PawnMasterLibrary;
using Microsoft.Win32;
using System.IO;
using System.Data;

namespace PawnMasterWPF;

public partial class MainWindow : Window
{
    string _role;
    byte[] _dataImage;

    private Employee loggedEmployee;

    public MainWindow()
    {
        InitializeComponent();
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
        if(_role == "A")
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
            if (!CheckRepeatEmpty(newProduct))
                ObjectControl.AddSerialize(newProduct);
            else
                MessageBox.Show("Повторяются названия продукта и дата покупки");
        }
    }

    public bool CheckRepeatEmpty(Product product)
    {
        List<Product> products = ObjectControl.Deserialize<Product>();
        foreach(var productInList in products)
        {
            if(product.ProductName == productInList.ProductName && product.ProductDateBuy == productInList.ProductDateBuy)
            {
                return true;
            }
        }
        return false;


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

        if (string.IsNullOrWhiteSpace(Convert.ToString(dataImage)))
            errorMessage += "Не добавлено фото продукта.\n";

        if (errorMessage != "")
        {
            MessageBox.Show(errorMessage);
            return false;
        }
        else
            return true;
    }

    private void ProductAvailabilityDataGrid_OnLoaded(object sender, RoutedEventArgs e)
    {
        List<Product> products = ObjectControl.Deserialize<Product>();
        List<Product> productIsNotSale = new List<Product>();
        foreach (var product in products)
        {
            if (product.IsSale == false)
                productIsNotSale.Add(product);
        }
        ProductAvailabilityDataGrid.ItemsSource = productIsNotSale;
    }
    
    private void OpenCardProduct_click(object sender, RoutedEventArgs e)
    {
        var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
        Product findProuct = ProductControl.FindProduct(selectedProduct);
        if (findProuct != null)
        {
            ProductCardWindow productCardWindow = new ProductCardWindow();
            productCardWindow.FillingInData(findProuct);
            productCardWindow.ShowDialog();
        }
    }

    private void PurchaseProduct_click(object sender, RoutedEventArgs e)
    {
        PurchaseProduct();
    }

    void PurchaseProduct()
    {
        SaleProductWindow saleProductWindow = new SaleProductWindow();
        saleProductWindow.ShowDialog();
        string dateSale = saleProductWindow.Date;
        string priceSale = saleProductWindow.Price;
        var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
        if (selectedProduct != null)
        {
            List<Product> products = ObjectControl.Deserialize<Product>();
            Product foundProduct = products.FirstOrDefault(product => product.ProductName == selectedProduct.ProductName && product.ProductDateBuy == selectedProduct.ProductDateBuy);
            foundProduct.IsSale = true;
            foundProduct.ProductPriceSale = priceSale;
            foundProduct.EmployeeName = loggedEmployee.FullName;
            foundProduct.ProductDateSale = dateSale;
            ObjectControl.ListSerialize(products);
        }
    }

    private void salesDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        List<Product> products = ObjectControl.Deserialize<Product>();
        List<Product> productIsSale = new List<Product>();
        foreach (var product in products)
        {
            if (product.IsSale)
                productIsSale.Add(product);
        }
        salesDataGrid.ItemsSource = productIsSale;
    }
}
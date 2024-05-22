using System.Windows;
using PawnMasterLibrary;
using Microsoft.Win32;
using System.IO;
using ModelForPawnMaster;

namespace PawnMasterWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    string _role;

    List<byte> imageDataList = new List<byte>();

    private Employee loggedEmployee;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ImageAdd_Click(object sender, RoutedEventArgs e)
    {
        ImageAdd();
    }

    void ImageAdd()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
       
        if (openFileDialog.ShowDialog() == true)
        {
            string selectedImagePath = openFileDialog.FileName;
            byte[] imageBytes = File.ReadAllBytes(selectedImagePath);
            foreach(byte imageByte in imageBytes)
            {
                imageDataList.Add(imageByte);
            }
        }
    }


    private void Login_Click(object sender, RoutedEventArgs e)
    {
        LoginWindow loginWindow = new LoginWindow();
        // loginWindow.Topmost = true;
        // loginWindow.Closed += (s, args) => { this.IsEnabled = true; };
        // this.IsEnabled = false;
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
        NameUser.Text = loggedEmployee.EmployeeFullName;
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
        byte[] imageData = imageDataList.ToArray();
        List<Product> products = ProductControl.ReceivingProduct();
        var newProduct = new Product()
        {
            ProductName = productName, ProductDateBuy = productDateBuy, ProductDescription = productDescription,
            ProductImageData = imageData, ProductPriceBuy = productPrice, IsSale = false
        };
        products.Add(newProduct);
        ProductControl.AddProduct(products);
    }

    private void ProductAvailabilityDataGrid_OnLoaded(object sender, RoutedEventArgs e)
    {
        List<Product> products = ProductControl.ReceivingProduct();
        List<Product> productIsNotSale = new List<Product>();
        foreach (var product in products)
        {
            if (product.IsSale == false)
            {
                productIsNotSale.Add(product);
            }
        }
        ProductAvailabilityDataGrid.ItemsSource = productIsNotSale;
    }
    
    private void OpenCardProduct_click(object sender, RoutedEventArgs e)
    {
        var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
        if(selectedProduct != null)
        {
            List<Product> products = ProductControl.ReceivingProduct();
            foreach (Product product in products)
            {
                if (product.ProductName == selectedProduct.ProductName &&
                    product.ProductPriceBuy == selectedProduct.ProductPriceBuy &&
                    product.ProductDateBuy == selectedProduct.ProductDateBuy)
                {
                    ProductCardWindow productCardWindow = new ProductCardWindow();
                    productCardWindow.FillingInData(product);
                    productCardWindow.ShowDialog();
                }
            }
        }
    }

    private void PurchaseProduct_click(object sender, RoutedEventArgs e)
    {
        SaleProductWindow saleProductWindow = new SaleProductWindow();
        saleProductWindow.ShowDialog();
        string dateSale = saleProductWindow.Date;
        string priceSale = saleProductWindow.Price;
        var selectedProduct = (Product)ProductAvailabilityDataGrid.SelectedItem;
        if (selectedProduct != null)
        {
            List<Product> products = ProductControl.ReceivingProduct();
            foreach (Product product in products)
            {
                if (product.ProductName == selectedProduct.ProductName &&
                    product.ProductPriceBuy == selectedProduct.ProductPriceBuy &&
                    product.ProductDateBuy == selectedProduct.ProductDateBuy)
                {
                    product.IsSale = true;
                    product.ProductPriceSale = priceSale;
                    product.EmployeeName = loggedEmployee.EmployeeFullName;
                    product.ProductDateSale = dateSale;
                }
                ProductControl.AddProduct(products);
            }
        }
    }

    private void salesDataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        List<Product> products = ProductControl.ReceivingProduct();
        List<Product> productIsSale = new List<Product>();
        foreach (var product in products)
        {
            if (product.IsSale)
            {
                productIsSale.Add(product);
            }
        }
        salesDataGrid.ItemsSource = productIsSale;
    }
}
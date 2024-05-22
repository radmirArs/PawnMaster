using PawnMasterWPF;
using ModelForPawnMaster;
using System.Text.Json;

namespace PawnMasterLibrary
{
    public static class ProductControl
    {
        private static string SerializedFilesPath()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory; //директория откуда запущено приложение
            string projectRoot = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..")); //избегаем bin\Debug\net8.0-windows
            string serializedFilesPath = Path.Combine(projectRoot, "SerializedFiles"); //путь к папке SerializedFiles
            return serializedFilesPath;
        }
        
        public static void AddProduct(List<Product> products) 
        {
            if (products != null)
            {
                    string serializedFilesPath = SerializedFilesPath();
                    string filePath = Path.Combine(serializedFilesPath, "Product.json"); //путь к файлу Employee.json
                    string productsSerialized = JsonSerializer.Serialize(products);
                    File.WriteAllText(filePath, productsSerialized);
            }
        }
        
        public static List<Product> ReceivingProduct()
        {
            string serializedFilesPath = SerializedFilesPath(); 
            string filePath = Path.Combine(serializedFilesPath, "Product.json"); 
            string[] jsonLines = File.ReadAllLines(filePath);
            string jsonString = File.ReadAllText(filePath);
            List<Product> products = new List<Product>();
            if(!string.IsNullOrWhiteSpace(jsonString))
                products = JsonSerializer.Deserialize<List<Product>>(jsonString);
            return products;
        }
        
    }
}

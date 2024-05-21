using PawnMasterWPF;
using ModelForPawnMaster;
using System.Text.Json;

namespace ArslanbekovLibrary
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
        public static void AddProduct(object product) 
        {
            if (product != null)
            {
                string serializedFilesPath = SerializedFilesPath();
                string filePath = Path.Combine(serializedFilesPath, "Product.json"); //путь к файлу Employee.json
                string employeeSerialized = JsonSerializer.Serialize(product);
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(employeeSerialized);// Запись строки в конец файла
                }
            }
        }

        public static List<Product> ReceivingProduct()
        {
            string serializedFilesPath = SerializedFilesPath(); 
            string filePath = Path.Combine(serializedFilesPath, "Product.json"); 
            string[] jsonLines = File.ReadAllLines(filePath);

            List<Product> products = new List<Product>();
            foreach (string line in jsonLines)
            {
                if (line != null || line != "{ }" || line != "{}")
                {
                    Product product = JsonSerializer.Deserialize<Product>(line);
                    products.Add(product);
                }
            }

            return products;
        }
    }
}

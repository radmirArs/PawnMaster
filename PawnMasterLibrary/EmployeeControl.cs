using System.Text.Json;
using PawnMasterWPF;

namespace PawnMasterLibrary;

public static class EmployeeControl
{
    private static string SerializedFilesPath()
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory; //директория откуда запущено приложение
        string projectRoot = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..")); //избегаем bin\Debug\net8.0-windows
        string serializedFilesPath = Path.Combine(projectRoot, "SerializedFiles"); //путь к папке SerializedFiles
        return serializedFilesPath;
    }
    
    public static void AddEmployee(object employee)
    {
        if (employee != null)
        {
            string serializedFilesPath = SerializedFilesPath();
            string filePath = Path.Combine(serializedFilesPath, "Employee.json"); //путь к файлу Employee.json
            string employeeSerialized = JsonSerializer.Serialize(employee);
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(employeeSerialized);// Запись строки в конец файла
            }
        }
    }
    
    public static List<Employee> ReceivingEmployeeInfo()
    {
        string serializedFilesPath = SerializedFilesPath(); // Получение пути к директории SerializedFiles
        string filePath = Path.Combine(serializedFilesPath, "Employee.json"); // Формирование полного пути к файлу Employee.json
        
        // Чтение всех строк файла
        string[] jsonLines = File.ReadAllLines(filePath);

        List<Employee> employees = new List<Employee>();

        // Десериализация каждой строки в объект Employee
        foreach (string line in jsonLines)
        {
            if (line != null || line != "{ }" || line != "{}")
            {
                Employee employee = JsonSerializer.Deserialize<Employee>(line);
                employees.Add(employee);
            }
            
        }
        return employees;
    }

}

using System.Text.Json;


namespace PawnMasterLibrary
{
    public static class ObjectControl
    {
        public static string SerializedFilesPath(string nameObject)
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory; //директория откуда запущено приложение
            string projectRoot = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..")); //избегаем bin\Debug\net8.0-windows
            string serializedFilesPath = Path.Combine(projectRoot, "SerializedFiles"); //путь к папке SerializedFiles
            string nameObjectJson = nameObject + ".json";
            string filePath = Path.Combine(serializedFilesPath, nameObjectJson);
            return filePath;
        }

        public static void AddSerialize(object objSerialize)
        {
            Type objectType = objSerialize.GetType();
            string nameObject = objectType.Name;
            string filePath = SerializedFilesPath(nameObject);
            if (objSerialize != null)
            {
                string objecterialized = JsonSerializer.Serialize(objSerialize);
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(objecterialized);
                }
            }
        }

        public static void ListSerialize<T>(List<T> objects)
        {
            if (objects != null)
            {
                string filePath = SerializedFilesPath(objects[0].GetType().Name);
                File.WriteAllText(filePath, string.Empty);
                foreach (T obj in objects)
                {
                    AddSerialize(obj);
                }
            }
        }

        public static List<T> Deserialize<T>()
        {
            string filePath = SerializedFilesPath(typeof(T).Name);
            string[] jsonLines = File.ReadAllLines(filePath);
            List<T> items = new List<T>();

            foreach (string line in jsonLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    T item = JsonSerializer.Deserialize<T>(line);
                    items.Add(item);
                }
            }
            return items;
        }

        

    }
}

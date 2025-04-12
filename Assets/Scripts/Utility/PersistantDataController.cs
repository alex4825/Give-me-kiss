using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class PersistantDataController
{
    public static void Save<T>(string objectName, T data)
    {
        string dataFilePath = Path.Combine(Application.persistentDataPath, objectName + "_data.json");

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(dataFilePath, json);

        Debug.Log("��������� ������: " + dataFilePath);
    }

    public static T Load<T>(string objectName)
    {
        string dataFilePath = Path.Combine(Application.persistentDataPath, objectName + "_data.json");
        T data;

        if (File.Exists(dataFilePath))
        {
            string json = File.ReadAllText(dataFilePath);
            data = JsonConvert.DeserializeObject<T>(json);
            Debug.Log($"������ ���������: {dataFilePath}");
            return data;
        }
        else
        {
            Debug.Log($"���� ������ {dataFilePath} �� ������.");
            return default;
        }
    }

    public static bool IsFileExists(string objectName)
    {
        string dataFilePath = Path.Combine(Application.persistentDataPath, objectName + "_data.json");
        return File.Exists(dataFilePath);
    }
}

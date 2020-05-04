using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public static class GameData<T>
{
    private static string path = "Assets/Gamedata/";
    public static bool Save(string fileName, T value)
    {
        string jsonContent = JsonConvert.SerializeObject(value, Formatting.Indented);
        File.WriteAllText(path + fileName + ".json", jsonContent);

        return true;
    }

    public static T Load(string fileName)
    {
        string jsonContent = File.ReadAllText(path + fileName + ".json");
        return JsonConvert.DeserializeObject<T>(jsonContent);
    }
}
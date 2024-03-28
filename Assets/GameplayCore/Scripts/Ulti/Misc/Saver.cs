using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class Saver
{
    public static void Write(PlayerData data, string key)
    {
        string Text = JsonConvert.SerializeObject(data);
        Debug.Log(Text);

        PlayerPrefs.SetString(key, Text);
        PlayerPrefs.Save();
        //string path = Application.persistentDataPath + "/test.txt";
        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine(Text);
        //writer.Close();
    }
    public static T Read<T>(string key)
    {
        //string path = Application.persistentDataPath + "/test.txt";
        //Debug.Log(path);

        //Read the text from directly from the test.txt file
        try
        {
            string value = PlayerPrefs.GetString(key, "");

            //StreamReader reader = new StreamReader(path);
            //string value = reader.ReadToEnd();
            T playerData = JsonConvert.DeserializeObject<T>(value);
            //reader.Close();
            return playerData;
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read");
            Debug.Log(e.Message);
        }
        return default;
    }
}

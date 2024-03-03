using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class Saver
{
    public static void Write(PlayerData data)
    {
        string Text = JsonConvert.SerializeObject(data);
        Debug.Log(Text);

        PlayerPrefs.SetString(GameConstant.PLAYERDATA, Text);
        PlayerPrefs.Save();
        //string path = Application.persistentDataPath + "/test.txt";
        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine(Text);
        //writer.Close();
    }
    public static PlayerData Read()
    {
        //string path = Application.persistentDataPath + "/test.txt";
        //Debug.Log(path);

        //Read the text from directly from the test.txt file
        try
        {
            string value = PlayerPrefs.GetString(GameConstant.PLAYERDATA, "");

            //StreamReader reader = new StreamReader(path);
            //string value = reader.ReadToEnd();
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(value);
            //reader.Close();
            return playerData;
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read");
            Debug.Log(e.Message);
        }
        return null;
    }
}

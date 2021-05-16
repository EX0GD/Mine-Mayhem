using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    [Serializable]
    public class GameData
    {
        public Level[] GameLevels;
    }
    public static GameData data;

    public static void SaveGame()
    {
        Debug.Log("Saving the game.");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MM_GameData";
        FileStream stream = new FileStream(path, FileMode.Create);

        data.GameLevels = LevelInformation.Levels;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        Debug.Log("Loading the game.");

        string path = Application.persistentDataPath + "/MM_GameData";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = (GameData)formatter.Deserialize(stream);
            Debug.Log($"Data = {data.GameLevels[0]}.");
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("No save file found.");
            return null;
        }
    }
}

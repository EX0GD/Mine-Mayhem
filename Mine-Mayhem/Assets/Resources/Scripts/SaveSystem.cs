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
        private Level[] GameLevels;

        public GameData()
        {
            GameLevels = LevelInformation.Levels;
        }
    }
    public static GameData data;

    public static void SaveGame()
    {
        Debug.Log("Saving the game.");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MM_GameData";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
    }

    public static void LoadGame()
    {
        Debug.Log("Loading the game.");
    }
}

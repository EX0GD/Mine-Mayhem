﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame()
    {
        Debug.Log("Saving the game.");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/MM_GameData";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(LevelInformation.Levels);
        


        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        Debug.Log("Loading the game...");
        string path = Application.persistentDataPath + "/MM_GameData";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //LevelInformation.Levels = (Level[])formatter.Deserialize(stream);
            GameData data = formatter.Deserialize(stream) as GameData;
            //LevelInformation.Levels = data.levels;
            //Debug.Log(LevelInformation.Levels[0].name);
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

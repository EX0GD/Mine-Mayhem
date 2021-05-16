using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[Serializable]
public static class LevelInformation
{
    public static Level[] Levels;

    static LevelInformation()
    {

        if (File.Exists(Application.persistentDataPath + "/MM_GameData"))
        {
            Levels = SaveSystem.LoadGame().GameLevels;
        }
        else
        {
            Levels = new Level[SceneManager.sceneCountInBuildSettings];

            for (int i = 0; i < Levels.Length; i++)
            {
                // if this is the Main Menu or level 1 - don't lock (Main Menu and level 1 should never be locked).
                if (i == 0 || i == 1)
                {
                    Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", false, Level.LevelStars.ZERO);
                }
                else
                {
                    Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO);
                }
                //Debug.Log($"Name: {Levels[i].name}, Path: {Levels[i].path}, Locked: {Levels[i].levelLocked}, Stars: {Levels[i].stars}.");
            }
        }
    }
}

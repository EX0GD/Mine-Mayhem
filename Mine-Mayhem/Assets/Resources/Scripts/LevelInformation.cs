using System;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;

[Serializable]
public static class LevelInformation
{
    public static Level[] Levels;
    private static readonly GameData data;

    static LevelInformation()
    {
        data = SaveSystem.LoadGame();
        Levels = data != null ? LoadSavedSetup() : LoadDefaultSetup();
    }

    private static Level[] LoadDefaultSetup()
    {
        Levels = new Level[SceneManager.sceneCountInBuildSettings];

        for (int i = 0; i < Levels.Length; i++)
        {
            // if this is the Main Menu or level 1 - don't lock (Main Menu and level 1 should never be locked).
            if (i == 0 || i == 1)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", false, Level.LevelStars.ZERO, Level.LevelGems.NONE, 0);
            }
            else if(i > 1 && i < 7)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.NONE, 0);
            }
            else if(i == 7)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.Gem1, 0);
            }
            else if(i > 7 && i < 11)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.Gem2, 0);
            }
            else if(i == 11)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.Gem3, 0);
            }
            else if(i == 12)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.NONE, 0);
            }
            else if(i == 13)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.Gem1, 0);
            }
            else if(i > 13 && i < 17)
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.Gem2, 0);
            }
            else
            {
                Levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.Gem3, 0);
            }
        }

        return Levels;
    }

    private static Level[] LoadSavedSetup()
    {
        // Temporarily store copy of data and resize the data.levels array.
        Level[] tempDataCopy = data.levels;
        data.levels = new Level[SceneManager.sceneCountInBuildSettings];
        Debug.Log(data.levels.Length);

        // Reassign existing values from temporary copied data back to data.levels array.
        if(data.levels.Length < tempDataCopy.Length)
        {
            for(int i = 0; i < data.levels.Length; i++)
            {
                if(data.levels[i] != tempDataCopy[i])
                {
                    data.levels[i] = tempDataCopy[i];
                }
            }
        }
        else
        {
            for(int i = 0; i < tempDataCopy.Length; i++)
            {
                if(data.levels[i] != tempDataCopy[i])
                {
                    data.levels[i] = tempDataCopy[i];
                }
            }
        }
        
        // If any element(level) in the array is null, create a new level
        for(int i = 0; i < data.levels.Length; i++)
        { 
            if(data.levels[i] == null)
            {
                data.levels[i] = new Level(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)), $"Level {i}", true, Level.LevelStars.ZERO, Level.LevelGems.Gem3, 0);
                // If the new level is locked, but the previous one has been completed, unlock new level.
                if(data.levels[i - 1].stars != Level.LevelStars.ZERO && data.levels[i].levelLocked)
                {
                    data.levels[i].levelLocked = false;
                }
            }
        }

        return data.levels;
    }
}

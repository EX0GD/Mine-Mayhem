using System;
using UnityEngine.SceneManagement;
using System.IO;

[Serializable]
public static class LevelInformation
{
    public static Level[] Levels;

    static LevelInformation()
    {
        GameData data = SaveSystem.LoadGame();
        Levels = data != null ? data.levels : LoadDefaultSetup();
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
            else if(i > 1 && i < 6)
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
            //Debug.Log($"Name: {Levels[i].name}, Path: {Levels[i].path}, Locked: {Levels[i].levelLocked}, Stars: {Levels[i].stars}.");
        }

        return Levels;
    }
}

﻿
[System.Serializable]
public class Level
{
    public string name;
    public string displayName;
    public string path;
    public bool levelLocked;

    public enum LevelStars
    {
        ZERO,
        Star1,
        Star2,
        Star3
    };
    public LevelStars stars;

    public enum LevelGems
    {
        NONE,
        Gem1,
        Gem2,
        Gem3
    };
    public LevelGems gems;

    public int gemsAcquired;

    public Level(string name, string displayName, bool levelLocked, LevelStars stars, LevelGems gems, int gemsAcquired)
    {
        this.name = name;
        this.displayName = displayName;
        this.levelLocked = levelLocked;
        this.stars = stars;
        this.gems = gems;
        this.gemsAcquired = gemsAcquired;
    }
}

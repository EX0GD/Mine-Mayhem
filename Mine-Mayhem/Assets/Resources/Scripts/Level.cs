using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Level(string name, string displayName, bool levelLocked, LevelStars stars)
    {
        this.name = name;
        this.displayName = displayName;
        this.levelLocked = levelLocked;
        this.stars = stars;
    }
}

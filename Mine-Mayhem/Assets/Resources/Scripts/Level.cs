
[System.Serializable]
public class Level
{
    public string name = null;
    public string displayName = null;
    public string path = null;
    public bool levelLocked = true;

    public enum LevelStars
    {
        ZERO,
        Star1,
        Star2,
        Star3
    };
    public LevelStars stars = LevelStars.ZERO;

    public enum LevelGems
    {
        NONE,
        Gem1,
        Gem2,
        Gem3
    };
    public LevelGems gems = LevelGems.NONE;

    public int gemsAcquired = 0;

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

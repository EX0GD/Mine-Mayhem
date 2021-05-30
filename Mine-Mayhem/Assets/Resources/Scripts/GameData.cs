
[System.Serializable]
public class GameData
{
    public Level[] levels;

    public GameData(Level[] levels)
    {
        //levels = LevelInformation.Levels;
        this.levels = levels;
    }
}

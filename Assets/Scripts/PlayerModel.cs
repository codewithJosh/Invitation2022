[System.Serializable]
public class PlayerModel
{

    public float levelEXP;
    public float nextLevelEXP;
    public int isMale;
    public int[,,,] MAP_INT; 
    public int lastSkinUsed;
    public int lastMapUsed;
    public int lastDivisionUsed;
    public int lastRoundStepUsed;
    public int unlockedSkins;
    public int unlockedMaps;
    public int level;

    public PlayerModel(PlayerManager _playerManager)
    {

        levelEXP = _playerManager.levelEXP;
        nextLevelEXP = _playerManager.nextLevelEXP;
        isMale = _playerManager.isMale;
        MAP_INT = _playerManager.MAP_INT;
        lastSkinUsed = _playerManager.lastSkinUsed;
        lastMapUsed = _playerManager.lastMapUsed;
        lastDivisionUsed = _playerManager.lastDivisionUsed;
        lastRoundStepUsed = _playerManager.lastRoundStepUsed;
        unlockedSkins = _playerManager.unlockedSkins;
        unlockedMaps = _playerManager.unlockedMaps;
        level = _playerManager.level;

    }

}

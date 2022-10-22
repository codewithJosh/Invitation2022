[System.Serializable]
public class PlayerModel
{

    public bool isMale;
    public int[,,] MAP_INT; 
    public int lastSkinUsed;
    public int lastMapUsed;
    public int lastMapDivisionUsed;
    public int lastMapRoundStepUsed;
    public int unlockedSkins;
    public int unlockedMaps;

    public PlayerModel(PlayerManager _playerManager)
    {

        isMale = _playerManager.isMale;
        MAP_INT = _playerManager.MAP_INT;
        lastSkinUsed = _playerManager.lastSkinUsed;
        lastMapUsed = _playerManager.lastMapUsed;
        lastMapDivisionUsed = _playerManager.lastMapDivisionUsed;
        lastMapRoundStepUsed = _playerManager.lastMapRoundStepUsed;
        unlockedSkins = _playerManager.unlockedSkins;
        unlockedMaps = _playerManager.unlockedMaps;

    }

}

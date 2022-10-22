[System.Serializable]
public class PlayerModel
{

    public bool isMale;
    public int[,,] MAP_INT; 
    public int lastSkinUsed;
    public int lastMapUsed;
    public int unlockedSkins;
    public int unlockedMaps;

    public PlayerModel(PlayerManager _playerManager)
    {

        isMale = _playerManager.isMale;
        MAP_INT = _playerManager.MAP_INT;
        lastSkinUsed = _playerManager.lastSkinUsed;
        lastMapUsed = _playerManager.lastMapUsed;
        unlockedSkins = _playerManager.unlockedSkins;
        unlockedMaps = _playerManager.unlockedMaps;

    }

}

[System.Serializable]
public class PlayerModel
{

    public int lastSkinUsed;
    public int lastMapUsed;
    public int unlockedSkins;
    public int unlockedMaps;

    public PlayerModel(PlayerManager _playerManager)
    {

        lastSkinUsed = _playerManager.lastSkinUsed;
        lastMapUsed = _playerManager.lastMapUsed;
        unlockedSkins = _playerManager.unlockedSkins;
        unlockedMaps = _playerManager.unlockedMaps;

    }

}

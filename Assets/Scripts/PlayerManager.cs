using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [HideInInspector] public int lastSkinUsed;
    [HideInInspector] public int lastMapUsed;
    [HideInInspector] public int unlockedSkins;
    [HideInInspector] public int unlockedMaps;

    public void NewPlayer()
    {

        lastSkinUsed = 0;
        lastMapUsed = 0;
        unlockedSkins = 0;
        unlockedMaps = 0;

        SavePlayer();

    }

    public void SavePlayer()
    {

        Database.SavePlayer(this);

    }

    public void LoadPlayer()
    {

        PlayerModel playerManager = Database.LoadPlayer();

        lastSkinUsed = playerManager.lastSkinUsed;
        lastMapUsed = playerManager.lastMapUsed;
        unlockedSkins = playerManager.unlockedSkins;
        unlockedMaps = playerManager.unlockedMaps;

    }

}

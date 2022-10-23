using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [HideInInspector] public int isMale;
    [HideInInspector] public int[,,,] MAP_INT;
    [HideInInspector] public int lastSkinUsed;
    [HideInInspector] public int lastMapUsed;
    [HideInInspector] public int lastDivisionUsed;
    [HideInInspector] public int lastRoundStepUsed;
    [HideInInspector] public int unlockedSkins;
    [HideInInspector] public int unlockedMaps;

    public void NewPlayer(int[,,,] _mAP_INT)
    {

        isMale = 0;
        MAP_INT = _mAP_INT;
        lastSkinUsed = 0;
        lastMapUsed = 0;
        lastDivisionUsed = 0;
        lastRoundStepUsed = 0;
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

        isMale = playerManager.isMale;
        MAP_INT = playerManager.MAP_INT;
        lastSkinUsed = playerManager.lastSkinUsed;
        lastMapUsed = playerManager.lastMapUsed;
        lastDivisionUsed = playerManager.lastDivisionUsed;
        lastRoundStepUsed = playerManager.lastRoundStepUsed;
        unlockedSkins = playerManager.unlockedSkins;
        unlockedMaps = playerManager.unlockedMaps;

    }

}

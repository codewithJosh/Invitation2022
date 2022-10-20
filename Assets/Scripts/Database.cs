using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{
    public static void SavePlayer(PlayerManager _playerManager)
    {

        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/player.invitation";
        FileStream stream = new(path, FileMode.Create);

        PlayerModel playerManager = new(_playerManager);
        formatter.Serialize(stream, playerManager);
        stream.Close();

    }

    public static PlayerModel LoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.mango";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            PlayerModel playerManager = formatter.Deserialize(stream) as PlayerModel;
            stream.Close();

            return playerManager;

        }
        else
        {

            Debug.Log("Savefile Not Found in " + path);
            return null;

        }

    }

}

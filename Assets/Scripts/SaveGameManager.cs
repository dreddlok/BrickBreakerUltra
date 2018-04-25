using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;                                           // Allows us to add second class into the script. System contains "serialisable" attribute.
using System.Runtime.Serialization.Formatters.Binary;   // This allows us to use the binary formatter.
using System.IO;                                        // Allows us to manipulate files.

public static class SaveGameManager {

    public static void SavePlayer(PlayerSave save)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/Save.sav", FileMode.Create);

        PlayerData data = new PlayerData(save);

        binaryFormatter.Serialize(stream, data);
        stream.Close();

    }

    public static bool[] LoadPlayer(out float volume, out float sfx, out bool screenShake)
    {
        if (File.Exists(Application.persistentDataPath + "/Save.sav"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/Save.sav", FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close();
            volume = data.volume;
            sfx = data.sfx;
            screenShake = data.screenShake;
            return data.levelsUnlocked;
        } else
        {
            Debug.Log("File does not exist");
            volume = 1;
            sfx = 1;
            screenShake = true;
            return new bool[9];
        }
        
    }


    [Serializable]
    public class PlayerData
    {
        public bool[] levelsUnlocked;
        public float volume;
        public float sfx;
        public bool screenShake;

        public PlayerData(PlayerSave playerSave )
        {
            levelsUnlocked = playerSave.level;
            volume = playerSave.volume;
            sfx = playerSave.sfx;
            screenShake = playerSave.screenShake;
        }
    }

}

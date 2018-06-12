using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSystem : MonoBehaviour {

    public void UnlockAllLevels()
    {
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        for (int i = 0; i < 26; i++)
        {
            playerSave.level[i] = true;
            Debug.Log("Level " + i + " unlocked");
        }
    }
}

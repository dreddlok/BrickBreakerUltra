using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour {

    public bool[] level;
    public float volume;
    public float sfx;
    public bool screenShake;
    public static PlayerSave instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate destroyed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Load();

        level[0] = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Save();
        }
        else if (Input.GetKeyDown("backspace"))
        {
            Load();
            Debug.Log("Game Loaded");
        }
    }

    public void Save()
    {
        SaveGameManager.SavePlayer(this);
        Debug.Log("Game Saved");
    }

    public void Load()
    {
        
        level = SaveGameManager.LoadPlayer(out volume, out sfx, out screenShake);
    }

    public void ChangeVolume(float amount)
    {
        volume += amount;
        volume = Mathf.Clamp(volume, 0, 1);
    }

    public void ChangeSFX(float amount)
    {
        sfx += amount;
        sfx = Mathf.Clamp(sfx, 0, 1);
    }

    public void ClearSave()
    {
        for (int i = 1; i < level.Length; i++)
        {
            level[i] = false;
        }
        volume = 1;
        sfx = 1;
        screenShake = true;
        Save();
        Debug.Log("save cleared");
    }
}

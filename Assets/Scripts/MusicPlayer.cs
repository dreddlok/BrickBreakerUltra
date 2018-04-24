using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private PlayerSave playerSave;

    public static MusicPlayer instance = null;


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
        playerSave = FindObjectOfType<PlayerSave>();
    }

    private void Update()
    {
        GetComponent<AudioSource>().volume = playerSave.volume;
    }

}

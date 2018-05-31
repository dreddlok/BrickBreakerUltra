using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private PlayerSave playerSave;

    public static MusicPlayer instance = null;
    private AudioSource audioSource;

    [Header("Tracks")]
    public AudioClip titleTrack;
    public AudioClip levelSelect;
    public AudioClip levelStart;
    public AudioClip level;
    public AudioClip levelComplete;
    public AudioClip levelFailed;
    public AudioClip Boss;

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
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //GetComponent<AudioSource>().volume = playerSave.volume;
    }

    public void ChangeTrack(AudioClip track, bool loop)
    {
        audioSource.Stop();
        audioSource.clip = track;
        audioSource.Play();
        audioSource.loop = loop;
    }

}

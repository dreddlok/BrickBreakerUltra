using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {

    public PlayerSave playerSave;
    public AudioClip audioClip;
    public MusicPlayer musicPlayer;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    public void ChangeVolume(float amount)
    {
        FindObjectOfType<PlayerSave>().ChangeVolume(amount);
        musicPlayer.GetComponent<AudioSource>().volume = FindObjectOfType<PlayerSave>().volume;
    }

    public void ChangeSFX(float amount)
    {
        FindObjectOfType<PlayerSave>().ChangeSFX(amount);
        playerSave = FindObjectOfType<PlayerSave>();
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, playerSave.sfx);
    }

}

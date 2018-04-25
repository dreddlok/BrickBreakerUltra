using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {

    public PlayerSave playerSave;
    public AudioClip audioClip;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();   
    }

    public void ChangeVolume(float amount)
    {
        FindObjectOfType<PlayerSave>().ChangeVolume(amount);
    }

    public void ChangeSFX(float amount)
    {
        FindObjectOfType<PlayerSave>().ChangeSFX(amount);
        playerSave = FindObjectOfType<PlayerSave>();
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, playerSave.sfx);
    }

}

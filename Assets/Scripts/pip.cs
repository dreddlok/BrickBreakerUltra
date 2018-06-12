using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pip : MonoBehaviour {

    public float pipValue; // determines where in the order of pips it lies and the number needed to "activate"
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public AudioClip audioClip;

    private Button button;
    public enum sliderType { volume, sfx};
    public sliderType target;
    private PlayerSave playerSave;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();
        button = GetComponent<Button>();
    }

    private void OnGUI()
    {
        if (target == sliderType.volume)
        {
            if (FindObjectOfType<PlayerSave>().volume >= pipValue)
            {
                button.image.sprite = activeSprite;
            }
            else
            {
                button.image.sprite = inactiveSprite;
            }
        } else
        {
            if (FindObjectOfType<PlayerSave>().sfx >= pipValue)
            {
                button.image.sprite = activeSprite;
            }
            else
            {
                button.image.sprite = inactiveSprite;
            }
        }

    }

    public void SetVolumeToSelf()
    {
        FindObjectOfType<PlayerSave>().volume = pipValue;
        FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>().volume = FindObjectOfType<PlayerSave>().volume;
    }

    public void SetSFXToSelf()
    {
        FindObjectOfType<PlayerSave>().sfx = pipValue;
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, pipValue);
        Debug.Log("play sound " + audioClip.ToString() + " at: " + Vector3.zero.ToString() + "at a volume of " + playerSave.sfx.ToString());
    }
}

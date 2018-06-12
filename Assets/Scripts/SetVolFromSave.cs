using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolFromSave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float vol = FindObjectOfType<PlayerSave>().sfx;
        GetComponent<AudioSource>().volume = vol;
	}

    public void UpdateVolume()
    {
        float vol = FindObjectOfType<PlayerSave>().sfx;
        GetComponent<AudioSource>().volume = vol;
    }
}

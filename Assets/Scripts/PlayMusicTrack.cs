using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicTrack : MonoBehaviour {

    public AudioClip track;
    public bool loop;

	// Use this for initialization
	void Start () {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer !=null)
        {
            //musicPlayer.ChangeTrack(track, loop);
            AudioSource audioSource = musicPlayer.GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.clip = track;
            audioSource.Play();
            Debug.Log("track changed by" + this.gameObject.name);
        }
        else
        {
            Debug.Log("no music player found");
        }
	}
}

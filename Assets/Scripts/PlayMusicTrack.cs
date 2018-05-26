using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicTrack : MonoBehaviour {

    public AudioClip track;
    public bool loop;

	// Use this for initialization
	void Start () {
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer != null)
        {
            musicPlayer.ChangeTrack(track, loop);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicTrack : MonoBehaviour {

    public AudioClip track;
    public bool loop;

	// Use this for initialization
	void Start () {
        FindObjectOfType<MusicPlayer>().ChangeTrack(track, loop);
	}
}

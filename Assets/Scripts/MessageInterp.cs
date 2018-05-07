using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageInterp : MonoBehaviour {
    public float speed = 1;
    public Vector3 secondEndPos;
    public Vector3 endPos;
    public float countDown = 2.5f;
    private float currentCountdown;
    public bool easingIn = true;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(transform.position.x * 2,transform.position.y);
        currentCountdown = countDown;
        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer != null)
        {
            musicPlayer.ChangeTrack(musicPlayer.level, true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (easingIn == true && currentCountdown > 0)
        {
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * speed);
        }
        else if (easingIn == true)
        {
            easingIn = false;
            currentCountdown = countDown;
        }
        else if (easingIn == false)
        {
            transform.position = Vector3.Lerp(transform.position, secondEndPos, Time.deltaTime * speed);
            iTween.FadeTo(gameObject, 0, 1);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -5.0f);//Mathf.Clamp(transform.position.z,-10,-10)
        
        if (currentCountdown <= 2f && easingIn == false)
        {
            BannerGrow banner = FindObjectOfType<BannerGrow>();
            banner.endScale = new Vector3(1,0,1);
            banner.fading = true;

        }

        currentCountdown -= Time.deltaTime * speed;
        if (currentCountdown <= 0 && easingIn == false)
        {            
            Destroy(transform.parent.gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            iTween.FadeTo(gameObject, 0, 1);
        }
    }
}

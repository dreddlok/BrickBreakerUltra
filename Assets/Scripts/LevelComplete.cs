using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

    public float speed = 1;
    public Vector3 secondEndPos;
    public Vector3 endPos;
    public float countDown = 2.5f;
    private float currentCountdown;
    public bool easingIn = true;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(transform.position.x * 2, transform.position.y);
        currentCountdown = countDown;
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -5.0f);//Mathf.Clamp(transform.position.z,-10,-10)

        if (currentCountdown <= 1.8f && easingIn == false)
        {
            FindObjectOfType<BannerGrow>().endScale = new Vector3(1, 0, 1);
        }

        // TODO this casues a bug when a new level begins (the level starts in slow-mo )
        if (Input.GetMouseButton(0))
        {
            easingIn = false;
            iTween.FadeTo(gameObject, 0, 1);                        
        }

        currentCountdown -= Time.deltaTime * speed;
        if (currentCountdown <= 0 && easingIn == false)
        {
            FindObjectOfType<LevelManager>().LoadNextLevel();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public bool bGameisPaused = false;
    public GameObject helpScreen;
	
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("p"))
        {
            Pause();
            //bGameisPaused = !bGameisPaused;
            //Canvas canvas = FindObjectOfType<Canvas>();
            //if (canvas.tag == "Menu")
            //{
                //canvas.enabled = !canvas.enabled;
            //}
        }
	}

    public void Pause()
    {        
        bGameisPaused = !bGameisPaused;
        Canvas [] canvas = FindObjectsOfType<Canvas>();
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].tag == "Menu")
            {
                canvas[i].enabled = !canvas[i].enabled;
            }
        }
        BossHealth boss = FindObjectOfType<BossHealth>();
        if (boss)
        {
            boss.bIsPaused = !boss.bIsPaused;
        }

        if (bGameisPaused)
        {
            Cursor.visible = true;
        } else
        {
            Cursor.visible = false;
        }
        if (FindObjectOfType<LevelManager>().isLevelCompleted == false)
        {
            Ball[] ballsInScene = FindObjectsOfType<Ball>();
            for (int i = 0; i < ballsInScene.Length; i++)
            {
                ballsInScene[i].Pause();
            }
        }

        MultiBall[] multiBallsInScene = FindObjectsOfType<MultiBall>();
        for (int i = 0; i < multiBallsInScene.Length; i++)
        {
            multiBallsInScene[i].Pause();
        }

        Pickup[] pickupsInScene = FindObjectsOfType<Pickup>();
        for (int i = 0; i < pickupsInScene.Length; i++)
        {
            pickupsInScene[i].Pause();
        }

        FindObjectOfType<Paddle>().Pause();
    }

    public void ShowHelp()
    {
        Instantiate(helpScreen);
        Canvas[] canvas = FindObjectsOfType<Canvas>();
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].tag == "Menu")
            {
                canvas[i].enabled = !canvas[i].enabled;
            }
        }

    }
}

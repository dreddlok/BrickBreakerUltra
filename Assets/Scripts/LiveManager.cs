using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveManager : MonoBehaviour {

    public Texture image;
    private int currentLives;
    public int maxLives = 3;
    public GameObject ball;
    public GameObject levelFailed;

    private void Start()
    {
        currentLives = maxLives;
    }

    private void OnGUI()
    {
        if (currentLives > 0)
        {
            for (int i = 0; i < currentLives; i++)
            {
                GUI.DrawTexture(new Rect(10 + i* image.width, 15, 40, 40), image);
            }
        }
    }

    public void AddLife(int n)
    {
        if (currentLives < maxLives)
        {
            currentLives += n;
        }
    }

    public void SubtractLife(int n)
    {
        if (currentLives >= 1)
        {
            currentLives -= n;
            Instantiate(ball);
        }
        else
        {
            Instantiate(levelFailed);
        }
    }


}

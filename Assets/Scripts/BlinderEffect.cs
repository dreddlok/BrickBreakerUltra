using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinderEffect : MonoBehaviour {

    
    public bool activated;
    public float shutterSpeed = 1.5f;
    public Texture blinderImage;    
    public Vector2 rectTargetTop;
    public Vector2 rectTargetBottom;
    public Vector2 rectCurrentTop;
    public Vector2 rectCurrentBottom;
    Vector2 size;

    private void Start()
    {        
        rectTargetTop = new Vector2(0, 0 -100);
        rectTargetBottom = new Vector2(0, 500 + 100);

        rectCurrentTop = new Vector2(0, 0 - 100);
        rectCurrentBottom = new Vector2(0, 500 + 100);

        size = new Vector2(800, 100);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            activated = !activated;
        }

        ActivateBlinders();
    }

    public void ActivateBlinders()
    {
        if (activated == false)
        {
            rectTargetTop = new Vector2(0, 0 - 100);
            rectTargetBottom = new Vector2(0, 500 + 100);
        }
        else
        {
            rectTargetTop = new Vector2(0, 0);
            rectTargetBottom = new Vector2(0, 500);
        }

        rectCurrentTop = Vector2.Lerp(rectCurrentTop, rectTargetTop, shutterSpeed * Time.deltaTime);
        rectCurrentBottom = Vector2.Lerp(rectCurrentBottom, rectTargetBottom, shutterSpeed * Time.deltaTime);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(rectCurrentTop, new Vector2(800, 100)), blinderImage);
        GUI.DrawTexture(new Rect(rectCurrentBottom, new Vector2(800, 100)), blinderImage);
    }
}

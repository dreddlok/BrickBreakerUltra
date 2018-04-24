using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleSide : MonoBehaviour {

    public bool bAutoPlay = false;
    public bool bIsPaused = false;
    public bool bInverted = false;
    public float paddleSpeed = 1;

    private float mousePosInBlocks;

    // Update is called once per frame
    void Update()
    {
        if (!bAutoPlay && !bIsPaused && !Input.GetMouseButton(0))
        {
            MoveWithMouse();
        }
        else if (bAutoPlay)
        {
            //AutoPlay();
        }
    }

    public void Pause()
    {
        bIsPaused = !bIsPaused;
    }

    void MoveWithMouse()
    {
        if (!bInverted)
        {
            Vector3 paddlePos = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            // dividing mouse y position by screen width gives y value as a percentage represented by 0-1. 12 is the number of world units in the scene's height
            mousePosInBlocks = (Input.mousePosition.y / Screen.height * 12);
            paddlePos.y = Mathf.Clamp(mousePosInBlocks, 1.0f, 11.0f);
            this.transform.position = Vector3.Lerp(this.transform.position, paddlePos, paddleSpeed);
        }
        else
        {
            Vector3 paddlePos = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            // dividing mouse y position by screen width gives y value as a percentage represented by 0-1. 12 is the number of world units in the scene's height
            mousePosInBlocks = ((Screen.height - Input.mousePosition.y) / Screen.height * 12);
            paddlePos.y = Mathf.Clamp(mousePosInBlocks, 1.0f, 13.0f);
            this.transform.position = Vector3.Lerp(this.transform.position, paddlePos, .2f); ;
        }
    }
}

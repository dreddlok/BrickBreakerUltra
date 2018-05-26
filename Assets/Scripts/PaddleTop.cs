using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleTop : MonoBehaviour {

    public bool bAutoPlay = false;
    public bool bIsPaused = false;
    public bool bInverted = false;
    public float paddleSpeed = 1;

    private float mousePosInBlocks;
	
	// Update is called once per frame
	void Update () {
        MoveWithMouse();
    }

    void MoveWithMouse()
    {
        if (!bInverted)
        {
            Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0);
            // dividing mouse x position by screen width gives x value as a percentage represented by 0-1. 16 is the number of world units in the scene's width
            mousePosInBlocks = (Input.mousePosition.x / Screen.width * 16);
            paddlePos.x = Mathf.Clamp(mousePosInBlocks, 1.0f, 15.0f);
            this.transform.position = Vector3.Lerp(this.transform.position, paddlePos, paddleSpeed);
        }
        else
        {
            Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0);
            // dividing mouse x position by screen width gives x value as a percentage represented by 0-1. 16 is the number of world units in the scene's width
            mousePosInBlocks = ((Screen.width - Input.mousePosition.x) / Screen.width * 16);
            paddlePos.x = Mathf.Clamp(mousePosInBlocks, 1.0f, 15.0f);
            this.transform.position = Vector3.Lerp(this.transform.position, paddlePos, .2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour {

    public Vector3 targetPos;
    public bool creditsFinishedScrolling;
    public GameObject titlebutton;
    public float scrollSpeed = 1;     

	// Update is called once per frame
	void Update () {
        if (transform.position.y < targetPos.y)
        {
            transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        } else if (creditsFinishedScrolling == false)
        {
            titlebutton.SetActive(true);
            creditsFinishedScrolling = true;
        }
	}
}

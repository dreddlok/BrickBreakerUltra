using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailed : MonoBehaviour {

    public float countDown = 1.5f;
    public float fallSpeed = 1;
    public Vector3 endPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime * fallSpeed;
        transform.position = Mathfx.Berp(transform.position, endPos, Time.deltaTime * fallSpeed);
        if (countDown <= 0)
        {
            FindObjectOfType<LevelManager>().LoadLevel("Level Select");
        }
    }
}

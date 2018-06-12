using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBlock : MonoBehaviour {

    public float slowMotionSpeed = .2f;
    public float zoom = 4f;
    public GameObject boss;
    public bool isBoss = false;
    private Vector3 originalCameraPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBoss)
        {
            BossHealth bossHealth = boss.GetComponent<BossHealth>();
            if (bossHealth.currentHealth <= bossHealth.damageAmount && Brick.numberOfBricksInScene < 2)
            {
                Debug.Log(Brick.numberOfBricksInScene);
                originalCameraPosition = Camera.main.transform.position;
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
                Camera.main.orthographicSize = zoom;
                Camera.main.GetComponent<BlinderEffect>().activated = true;
                FindObjectOfType<LiveManager>().enabled = false;
                SlowTime();
            }
        }
        else
        {
            if (Brick.numberOfBricksInScene < 2)
            {        
            Debug.Log(Brick.numberOfBricksInScene);
            originalCameraPosition = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
            Camera.main.orthographicSize = zoom;
            Camera.main.GetComponent<BlinderEffect>().activated = true;
            FindObjectOfType<LiveManager>().enabled = false;
            SlowTime();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            RestoreTime();
            Camera.main.transform.position = originalCameraPosition;
            Camera.main.orthographicSize = 6;
            Camera.main.GetComponent<BlinderEffect>().activated = false;
            FindObjectOfType<LiveManager>().enabled = true;
    }

    private void OnDestroy()
    {
        //RestoreTime();
        //Camera.main.transform.position = originalCameraPosition;
        //Camera.main.orthographicSize = 6;
    }

    private void SlowTime()
    {
        Time.timeScale = slowMotionSpeed;
        Time.fixedDeltaTime = slowMotionSpeed * 0.02f;
    }

    public void RestoreTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

}

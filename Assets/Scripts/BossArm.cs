using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArm : MonoBehaviour {

    public float moveTime;
    public float speed;
    public float direction; //-1 = left 1 = right
    private float countDown;


    private void Start()
    {
        countDown = moveTime;
    }
    // Update is called once per frame
    void Update () {
        countDown -= Time.deltaTime;
        if (countDown > 0)
        {
            transform.position += Vector3.right * (speed * direction) * Time.deltaTime;
        }
        else
        {
            if (direction == -1)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            countDown = moveTime;
        }
	}
}

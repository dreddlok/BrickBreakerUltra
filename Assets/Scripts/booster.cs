using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booster : MonoBehaviour {

    public float boostStrength = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            collision.GetComponent<Rigidbody2D>().velocity = this.transform.up * boostStrength;
        }
    }

}

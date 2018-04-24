using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            GetComponent<AudioSource>().Play();
            FindObjectOfType<LiveManager>().SubtractLife(1);
            Destroy(collider.gameObject);
            FindObjectOfType<Camera>().GetComponent<CameraShake>().shaking = true;
        }
        else
        {
            Destroy(collider);
        }
            
    }
}

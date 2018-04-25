using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {
    public AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            PlayerSave playerSave = FindObjectOfType<PlayerSave>();
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, playerSave.sfx);
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

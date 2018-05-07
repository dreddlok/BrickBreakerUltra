using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour {
    public AudioClip audioClip;

    private float sfxVol = 1;
    private PlayerSave playerSave;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();
        if (playerSave != null)
        {
            sfxVol = playerSave.sfx;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, sfxVol);
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

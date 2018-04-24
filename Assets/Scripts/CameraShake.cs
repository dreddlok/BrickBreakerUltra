using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float shakeStrength = 0.5f;
    public float duration = 1.0f;
    public bool shaking = false;
    private float currentDuration;
    private Transform cameraTransform;
    private Vector3 startPos;
    private PlayerSave playerSave;
    
    // Use this for initialization
	void Start () {
        cameraTransform = FindObjectOfType<Camera>().transform;
        startPos = cameraTransform.localPosition;
        currentDuration = duration;
        playerSave = FindObjectOfType<PlayerSave>();
    }
	
	// Update is called once per frame
	void Update () {
		if (shaking && playerSave.screenShake)
        {
            if (currentDuration > 0)
            {
                Vector3 shakePos = startPos + Random.insideUnitSphere * shakeStrength;
                cameraTransform.localPosition = new Vector3 (shakePos.x,shakePos.y,cameraTransform.localPosition.z);
                currentDuration -= Time.deltaTime;
            }
            else
            {
                cameraTransform.localPosition = startPos;
                shaking = false;
                currentDuration = duration;
            }
        }
	}
}

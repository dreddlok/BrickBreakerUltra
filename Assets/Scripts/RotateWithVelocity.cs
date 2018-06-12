using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithVelocity : MonoBehaviour {


    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start () {

        rigidBody = GetComponentInParent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 v = rigidBody.velocity;
        var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.LookRotation(rigidBody.velocity); 
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, Quaternion.LookRotation(rigidBody.velocity).z, 0);
    }
}

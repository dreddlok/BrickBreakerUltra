using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeAndBrowMovement : MonoBehaviour {

    public float EyeBrowHeight;
    public float browBuffer;
    public float browClampMin;
    public float browClampMax;
    public GameObject target;
    public Vector2 startPos;
    public Transform eyeLeft;
    public Transform eyeRight;
    public float eyeRotSpeed = 1;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update () {

        if (transform.parent.GetComponent<BossHealth>().bossIsAlive)
        {
            MoveEyeBrows();
        }
        RotateEye(eyeLeft);
        RotateEye(eyeRight);
	}

    private void MoveEyeBrows()
    {
        EyeBrowHeight = Vector2.Distance(transform.position, target.transform.position);
        EyeBrowHeight = Mathf.Round(EyeBrowHeight * 1000.0f) / 1000.0f;
        EyeBrowHeight = Mathf.Clamp(EyeBrowHeight, browClampMin, browClampMax);

        transform.position = new Vector2(transform.position.x, startPos.y + browBuffer - EyeBrowHeight * .1f);
    }

    private void RotateEye(Transform eyeTransform)
    {
        Vector3 vectorToTarget = target.transform.position - eyeTransform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        eyeTransform.rotation = Quaternion.Slerp(eyeTransform.rotation, q, Time.deltaTime * eyeRotSpeed);
    }
}

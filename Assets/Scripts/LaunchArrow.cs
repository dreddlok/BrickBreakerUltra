using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArrow : MonoBehaviour {

    public Ball ball; // ball is set by the actual ball
    public Sprite arrowImage;

	// Update is called once per frame
	void Update () {
        MatchBallRotation();
    }

    public void MatchBallRotation()
    {
        transform.rotation = Quaternion.AngleAxis(ball.launchAngle - 90, Vector3.forward);
    }

}

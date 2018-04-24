using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEffect : MonoBehaviour {

    public Vector3 springAmount = Vector3.one;
    public float time = 1;

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            //PlaySpringEffect();
            iTween.PunchScale(this.gameObject, springAmount, time);
        }
        //float scale = minScale + Mathf.PingPong(Time.deltaTime * 0.2f, 1f - minScale);
        //transform.localScale = new Vector3(scale, scale, scale);
    }

    public void PlaySpringEffect()
    {
        //Vector3 springScale = new Vector3(Mathfx.Berp(6,4,berpPower),4,1);
        //transform.localScale = springScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        iTween.PunchScale(this.gameObject, springAmount, time);
        FindObjectOfType<Camera>().GetComponent<SubtleCameraShake>().shaking = true;
    }
}

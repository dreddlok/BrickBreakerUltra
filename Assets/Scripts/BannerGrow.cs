using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerGrow : MonoBehaviour {
    
    public Vector3 endScale;
    public bool fading = false;
    private RectTransform rectTransform;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, endScale, Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            iTween.FadeTo(gameObject, 0, 1);
        }

        if (fading)
        {
            iTween.FadeTo(gameObject, 0, 1);
        }
    }

    
}

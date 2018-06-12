using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeLevelName : MonoBehaviour {

    public float fadeCountDown = 1;
    public float fadeSpeed = 1f;
    public float fadeDelay = 3f;
    private Text text;
    private Color textColor;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        textColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (fadeDelay > 0 )
        {
            fadeDelay -= Time.deltaTime;
        } else
        {
            if (fadeCountDown > 0)
            {
                fadeCountDown -= Time.deltaTime * fadeSpeed;
            }
            textColor.a = fadeCountDown;
            text.color = textColor;
        }   
    }
}

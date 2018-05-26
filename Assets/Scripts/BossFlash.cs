
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlash : MonoBehaviour {

    public float flashCountDown = 0;
    public float flashSpeed = 1f;
    private SpriteRenderer spriteRenderer;
    private Color flashColor;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashCountDown > 0)
        {
            flashCountDown -= Time.deltaTime * flashSpeed;
        }
        flashColor.a = flashCountDown;
        spriteRenderer.color = flashColor;

        if (Input.GetKeyDown(KeyCode.F))
        {
            FlashSprite();
        }


    }

    public void FlashSprite()
    {
        flashCountDown = 1;

    }
}

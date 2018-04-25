using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : MonoBehaviour {

    public bool bIsPaused = false;
    private Vector2 storedVelocity;
    public AudioClip bounceSFX;

    [Header("Combo Sounds")]
    public AudioClip[] comboSFX = new AudioClip[8];

    [Header("Combo Variables")]
    public float comboLinkDuration = 1;
    public float comboCountDown = 0;
    public int comboStage = 0;
    public int maxComboStage = 7;


    private void Update()
    {
        if (comboCountDown > 0)
        {
            comboCountDown -= Time.deltaTime;
        }
        else
        {
            comboStage = 0;
        }
    }
    public void Pause()
    {
        bIsPaused = !bIsPaused;
        if (bIsPaused)
        {
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            storedVelocity = rigidbody2D.velocity;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.velocity = storedVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
            AudioSource audioSource = GetComponent<AudioSource>();
            if (collision.gameObject.tag == "Breakable")
            {

                AudioSource.PlayClipAtPoint(comboSFX[comboStage], Vector3.zero, playerSave.sfx);
                comboCountDown = comboLinkDuration;
                if (comboStage < maxComboStage)
                {
                    comboStage++;
                }
            }
            else
            {
                AudioSource.PlayClipAtPoint(bounceSFX, Vector3.zero, playerSave.sfx);
            }

            GetComponent<Rigidbody2D>().velocity += tweak;
    }
}

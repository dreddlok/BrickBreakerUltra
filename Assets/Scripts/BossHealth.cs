using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    public static int numberOfBricksInScene = 0;
    public float maxHealth = 1;
    public float currentHealth;
    public Slider healthSlider;
    public float damageAmount = .05f;
    public float speed = 1;
    public float buffer = 50;
    public AudioClip collisionSFX;
    public GameObject debris;
    public GameObject shockwave;
    public bool bIsPaused = false;

    private bool movingRight;
    private float spriteWidth;
    private LevelManager levelManager;
    private BossFlash bossFlash;
    private PlayerSave playerSave;
    private float sfxVol = 1;

    private void Start()
    {
        currentHealth = maxHealth;
        levelManager = FindObjectOfType<LevelManager>();
        bossFlash = transform.Find("BossFlash").GetComponent<BossFlash>();
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        spriteWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        if (playerSave != null)
        {
            sfxVol = playerSave.sfx;
        }
    }

    private void Update()
    {
        if(!bIsPaused)
        {
            MoveLeftAndRight();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (currentHealth > 0)
            {
                currentHealth -= damageAmount;
                healthSlider.value = currentHealth;
                Instantiate(shockwave, transform.position, Quaternion.identity);
                bossFlash.FlashSprite();
                AudioSource.PlayClipAtPoint(collisionSFX, Vector3.zero, sfxVol);
            }
            else
            {
                BossDead();
            }
        }
    }

    private void BossDead()
    {
        levelManager.LevelComplete();
    }

    private void MoveLeftAndRight()
    {
        Vector3 positionInWorldUnits = Camera.main.WorldToScreenPoint(transform.position);
        if (positionInWorldUnits.x - (spriteWidth/2) <= buffer)
        {
            movingRight = true;
        }
        else if (positionInWorldUnits.x + (spriteWidth / 2) >= (Screen.width - buffer))
        {
            movingRight = false;
        }

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}

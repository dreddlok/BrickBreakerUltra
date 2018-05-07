using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Brick : MonoBehaviour {

    public int hitPoints = 1;
    public Sprite [] hitSprite;
    public static int numberOfBricksInScene = 0;
    public AudioClip collisionSFX;
    public GameObject debris;
    public GameObject shockwave;

    private LevelManager levelManager;
    private bool isBreakable;
    public bool ultra = false;
    private BrickFlash brickFlash;
    private PlayerSave playerSave;
    private float sfxVol = 1;

    void Start()
    {
        isBreakable = (this.tag == "Breakable");
        levelManager = FindObjectOfType<LevelManager>();
        if (isBreakable)
        {
            numberOfBricksInScene++;
        }
        if (isBreakable)
        {
            brickFlash = transform.Find("brick flash").GetComponent<BrickFlash>();
            PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        }
        if (playerSave != null)
        {
            sfxVol = playerSave.sfx;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (isBreakable)
        {
            Instantiate(shockwave, transform.position, Quaternion.identity);
            brickFlash.FlashSprite();
            if (ultra)
            {                
                if (collision.gameObject.GetComponent<Ball>().bSlingShotted)
                {
                    AudioSource.PlayClipAtPoint(collisionSFX, Vector3.zero, sfxVol);
                    HandleHits();
                    collision.gameObject.GetComponent<Ball>().bSlingShotted = false;
                }
            } else
            {
                AudioSource.PlayClipAtPoint(collisionSFX, Vector3.zero, sfxVol);
                HandleHits();
                collision.gameObject.GetComponent<Ball>().bSlingShotted = false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBreakable)
        {
            Instantiate(shockwave, transform.position, Quaternion.identity);
            brickFlash.FlashSprite();
            if (ultra)
            {
                if (collision.gameObject.GetComponent<Ball>().bSlingShotted)
                {
                    AudioSource.PlayClipAtPoint(collisionSFX, Vector3.zero, sfxVol);
                    HandleHits();
                    collision.gameObject.GetComponent<Ball>().bSlingShotted = false;
                }
            }
            else
            {
                AudioSource.PlayClipAtPoint(collisionSFX, Vector3.zero, sfxVol);
                HandleHits();
                collision.gameObject.GetComponent<Ball>().bSlingShotted = false;
            }
        }
    }

    void HandleHits()
    {       
        hitPoints -= 1;
        if (hitPoints <= 0)
        {
            numberOfBricksInScene--;
            levelManager.BrickDestroyed();
            GameObject instantiatedDebris = Instantiate(debris,transform.position, Quaternion.identity);
            var ps = instantiatedDebris.GetComponent<ParticleSystem>();
            ps.startColor = GetComponent<SpriteRenderer>().color;
            Destroy(gameObject);            
        }
        else
        {
            LoadSprite();
        }
    }

    private void LoadSprite()
    {  
        int maxHP = hitSprite.Length + 1;
        int spriteIndex = maxHP - hitPoints - 1;
        if (hitSprite[spriteIndex] == null)
        {
            Debug.LogError("No sprite found");
        }else
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprite[spriteIndex];
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Brick Clicked");
    }

}

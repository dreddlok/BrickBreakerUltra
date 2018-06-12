using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public bool bAutoPlay = false;
    public bool bIsPaused = false;
    public bool bInverted = false;
    public float paddleSpeed = 1;
    public bool slingshotAvailable = true;
    public Sprite GemOn;
    public Sprite GemOff;
    public AudioClip engineRoar;
    public GameObject gemActivatedPS;
    public AudioClip gemActivatedSFX;
    public AudioClip Leveltrack;

    [Header("Power Ups")]
    [HideInInspector]
    // Spear
    public float spearActive = 0;
    // how long the spear powerup will stay active for
    public float spearPowerupDuration = 1.5f;
    private bool bricksChangedToTriggers = false;
    // infinity
    public float infinityActive = 0;    
    public float infinityPowerupDuration = 3f; // how long the infinity powerup will stay active for
    public bool infinityActivated = false;
    // shrink
    public float shrinkActive = 0;    
    public float shrinkPowerupDuration = 3f; // how long the shrink powerup will stay active for
    public bool shrinkActivated = false;
    // confusion
    public float confusionActive = 0;
    public float confusionPowerupDuration = 2.6f; // how long the confusion powerup will stay active for
    public bool confusionActivated = false;
    // expand
    public float expandActive = 0;
    public float expandPowerupDuration = 2.6f; // how long the expand powerup will stay active for
    public bool expandActivated = false;
    public float expandSpeed = 1.5f;
    public float expandAmount = 1.2f;
    public Vector3 targetPositionL;
    public Vector3 targetPositionR;
    private Transform expandPaddleL;
    private Transform expandPaddleR;

    private float sfxVol = 1;
    private TrailRenderer trailRenderer;

    private float mousePosInBlocks;
    private Ball ball;

    private void Start()
    {
        Cursor.visible = false;
        FindObjectOfType<LevelManager>().isLevelCompleted = false;
        expandPaddleL = transform.Find("Paddle Wing R");
        expandPaddleR = transform.Find("Paddle Wing L");
        Vector3 targetPositionL = new Vector3(expandPaddleL.transform.localPosition.x, expandPaddleL.transform.localPosition.y, expandPaddleL.transform.localPosition.z);
        Vector3 targetPositionR = new Vector3(expandPaddleR.transform.localPosition.x, expandPaddleL.transform.localPosition.y, expandPaddleL.transform.localPosition.z);

        MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer != null)
        {
           musicPlayer.ChangeTrack(Leveltrack, true);
        }

        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        if (playerSave != null)
        {
            sfxVol = playerSave.sfx;
        }

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.sortingLayerName = "gem trail";

        ActivateSlingshot();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bAutoPlay && !bIsPaused && !Input.GetMouseButton(0))
        {
            MoveWithMouse();
        } else if (bAutoPlay)
        {
            AutoPlay();
        }
        
        SpearPowerup();
        InfinityPowerup();
        ShrinkPowerup();
        ConfusionPowerup();
        ExpandPowerup();
    }

    public void Pause()
    {
        bIsPaused = !bIsPaused;
    }

    void MoveWithMouse()
    {
        if (!bInverted)
        {
            Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0);
            // dividing mouse x position by screen width gives x value as a percentage represented by 0-1. 16 is the number of world units in the scene's width
            mousePosInBlocks = (Input.mousePosition.x / Screen.width * 16);
            paddlePos.x = Mathf.Clamp(mousePosInBlocks, 1.0f, 15.0f);
            this.transform.position = Vector3.Lerp(this.transform.position, paddlePos, paddleSpeed);
        }
        else
        {
            Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0);
            // dividing mouse x position by screen width gives x value as a percentage represented by 0-1. 16 is the number of world units in the scene's width
            mousePosInBlocks = ((Screen.width - Input.mousePosition.x) / Screen.width * 16);
            paddlePos.x = Mathf.Clamp(mousePosInBlocks, 1.0f, 15.0f);
            this.transform.position = Vector3.Lerp(this.transform.position, paddlePos, .2f);
        }
    }

    void AutoPlay()
    {
        ball = FindObjectOfType<Ball>();
        float ballXPos = ball.transform.position.x;
        ballXPos = Mathf.Clamp(ballXPos, 0.5f, 15.5f);
        this.transform.position = new Vector3(ballXPos, this.transform.position.y);
    }

    public void ActivateSlingshot()
    {
        if (slingshotAvailable == false)
        {
            trailRenderer.enabled = true;
            slingshotAvailable = true;
            GetComponent<SpriteRenderer>().sprite = GemOn;
            transform.Find("SmallPaddle").GetComponent<SpriteRenderer>().sprite = GemOn;
            AudioSource.PlayClipAtPoint(gemActivatedSFX, Vector3.zero, sfxVol);
            Instantiate(gemActivatedPS, transform);
        }
    }

    public void DeactivateSlingshot()
    {
        trailRenderer.enabled = false;
        slingshotAvailable = false;
        GetComponent<SpriteRenderer>().sprite = GemOff;
        transform.Find("SmallPaddle").GetComponent<SpriteRenderer>().sprite = GemOff;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ActivateSlingshot(); 
    }

    private void SpearPowerup()
    {
        if (spearActive > 0)
        {
            spearActive -= Time.deltaTime;
            if (bricksChangedToTriggers == false)
            {
                Brick[] brickArray = FindObjectsOfType<Brick>();
                for (int i = 0; i < brickArray.Length; i++)
                {
                    brickArray[i].GetComponent<BoxCollider2D>().isTrigger = true;
                }
                bricksChangedToTriggers = true;
                Ball[] ballArray = FindObjectsOfType<Ball>();
                for (int i = 0; i < ballArray.Length; i++)
                {
                    ballArray[i].transform.Find("Spear").gameObject.SetActive(true);
                }
            }
        }
        else if (bricksChangedToTriggers == true)
        {
            Brick[] brickArray = FindObjectsOfType<Brick>();
            for (int i = 0; i < brickArray.Length; i++)
            {
                brickArray[i].GetComponent<BoxCollider2D>().isTrigger = false;
            }
            Ball[] ballArray = FindObjectsOfType<Ball>();
            for (int i = 0; i < ballArray.Length; i++)
            {
                ballArray[i].transform.Find("Spear").gameObject.SetActive(false);
            }
            bricksChangedToTriggers = false;
        }
    }

    private void InfinityPowerup()
    {
        if (infinityActive > 0)
        {
            infinityActive -= Time.deltaTime;
            if (infinityActivated == false)
            {
                infinityActivated = true;
            }
        } else if (infinityActivated == true)
        {
            infinityActivated = false;
        }
    }

    private void ShrinkPowerup()
    {
        if (shrinkActive > 0)
        {
            shrinkActive -= Time.deltaTime;
            if (shrinkActivated == false)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                //GetComponent<PolygonCollider2D>().enabled = false;
                transform.Find("Gear").gameObject.SetActive(false);
                transform.Find("SmallPaddle").gameObject.SetActive(true);
                expandPaddleL.gameObject.SetActive(false);
                expandPaddleR.gameObject.SetActive(false);
                shrinkActivated = true;
            }
        }
        else if (shrinkActivated == true)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<PolygonCollider2D>().enabled = true;
            transform.Find("Gear").gameObject.SetActive(true);
            transform.Find("SmallPaddle").gameObject.SetActive(false);
            expandPaddleL.gameObject.SetActive(true);
            expandPaddleR.gameObject.SetActive(true);
            shrinkActivated = false;
        }
    }

    private void ConfusionPowerup()
    {
        if (confusionActive > 0)
        {
            confusionActive -= Time.deltaTime;
            if (confusionActivated == false)
            {
                confusionActivated = true;
                bInverted = true;
            }
        }
        else if (confusionActivated == true)
        {
            confusionActivated = false;
            bInverted = false;
        }
    }

    private void ExpandPowerup()
    {
        if (expandActive > 0)
        {
            expandActive -= Time.deltaTime;
            if (expandActivated == false)
            {
                targetPositionL = new Vector3(-expandAmount, expandPaddleL.transform.localPosition.y, expandPaddleL.transform.localPosition.z);
                targetPositionR = new Vector3(expandAmount, expandPaddleR.transform.localPosition.y, expandPaddleR.transform.localPosition.z);
                expandActivated = true;
            }
        }
        else if (expandActivated == true)
        {
            targetPositionL = new Vector3(0, expandPaddleL.transform.localPosition.y, expandPaddleL.transform.localPosition.z);
            targetPositionR = new Vector3(0, expandPaddleR.transform.localPosition.y, expandPaddleR.transform.localPosition.z);
            expandActivated = false;
        }

        expandPaddleL.transform.localPosition = Vector3.Lerp(expandPaddleL.transform.localPosition, targetPositionL, expandSpeed * Time.deltaTime);
        expandPaddleR.transform.localPosition = Vector3.Lerp(expandPaddleR.transform.localPosition, targetPositionR, expandSpeed * Time.deltaTime);
    }
}

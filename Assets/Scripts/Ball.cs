using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Paddle paddle;    
    public LaunchArrow launchArrow;
    public float launchAngle = 50;
    public float launchRotateSpeed = 20.0f;
    public float launchPower = 7.5f;
    public bool bIsPaused = false;
    public bool bSlingShotted = false;
    public float slowMotionSpeed = 0.2f;    
    public AudioClip bounceSFX;
    public AudioClip laucnhSFX;
    public bool boosted = false;
    public float slowFactor = .8f;
    public bool slowed = false;
    public GameObject debris;

    [Header("Combo Variables")]
    public float comboLinkDuration = 1;
    public float comboCountDown = 0;
    public int comboStage = 0;
    public int maxComboStage = 7;

    [Header("Combo Sounds")]
    public AudioClip[] comboSFX = new AudioClip [8];

    private bool bHasStarted = false;
    private int launchAnglerRotDir = 1;
    private Vector2 storedVelocity;
    private Vector3 slingShotVector;
    private Vector3 initialCursorPosition;
    private float sfxVolume;
    private PlayerSave playerSave;

    // Use this for initialization
    void Start () {
        paddle = FindObjectOfType<Paddle>();
        launchArrow = FindObjectOfType<LaunchArrow>();
        launchArrow.GetComponent<Renderer>().enabled = true;
        launchArrow.ball = this;
        this.transform.position = paddle.transform.position;
        playerSave = FindObjectOfType<PlayerSave>();
        if (playerSave == null)
        {
            sfxVolume = 1;
        } else
        {
            sfxVolume = playerSave.sfx;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!bIsPaused)
        {
            RotateLaunchAngleBackAndForth();
            if (comboCountDown > 0)
            {
                comboCountDown -= Time.deltaTime;
            }
            else
            {
                comboStage = 0;
            }
        }
        if (bHasStarted == false)
        {
            this.transform.position = paddle.transform.position;
            if (!bIsPaused)
            {                
                if (Input.GetMouseButtonUp(0))
                {
                    LaunchBall();
                }
            }
        }
        else
        {
            Paddle paddle = FindObjectOfType<Paddle>();
            if (paddle.slingshotAvailable || paddle.infinityActivated)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SlowTime();
                    initialCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                if (Input.GetMouseButton(0))
                {
                    SlingShot();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    RestoreTime();
                    bSlingShotted = true;
                    FindObjectOfType<Paddle>().DeactivateSlingshot();
                }
            }

        }      
	}

    private void SlowTime()
    {
        Time.timeScale = slowMotionSpeed;
        Time.fixedDeltaTime = slowMotionSpeed * 0.02f;
        initialCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void LaunchBall()
    {        
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<TrailRenderer>().enabled = true;
        rigidbody2D.velocity = CalculateLaunchVector(launchAngle) * launchPower;
        launchArrow.GetComponent<Renderer>().enabled = false;
        AudioSource.PlayClipAtPoint(laucnhSFX, Vector3.zero, sfxVolume);
        bHasStarted = true;
    }

    private void RestoreTime()
    {
        GetComponent<Rigidbody2D>().velocity = slingShotVector.normalized * launchPower;
        GetComponent<LineRenderer>().enabled = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
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

    private Vector2 CalculateLaunchVector(float angle)
    {
        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.right);
        return dir;
    }

    private void RotateLaunchAngleBackAndForth()
    {
        if (launchAngle >= 135)
        {
            launchAnglerRotDir = -1;
        } else if (launchAngle <= 45)
        {
            launchAnglerRotDir = 1;
        }
        launchAngle += launchRotateSpeed * Time.deltaTime * launchAnglerRotDir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();

        Vector2 tweak = new Vector2(Random.Range(0f,0.2f), Random.Range(0f, 0.2f));
        if (bHasStarted)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (collision.gameObject.tag == "Breakable")
            {
                AudioSource.PlayClipAtPoint(comboSFX[comboStage], Vector3.zero, sfxVolume);
                comboCountDown = comboLinkDuration;
                if (comboStage < maxComboStage)
                {                   
                    comboStage++;
                }
            }
            else if (collision.gameObject.tag == "Paddle")
            {
                AudioSource.PlayClipAtPoint(bounceSFX, Vector3.zero, sfxVolume);
                bSlingShotted = false;
            }
            else
            {
                AudioSource.PlayClipAtPoint(bounceSFX, Vector3.zero, sfxVolume);
                bSlingShotted = false;
                Instantiate(debris, transform.position, Quaternion.identity);
            }
            
            GetComponent<Rigidbody2D>().velocity += tweak;
        }
    }

    private void SlingShot()
    {
        Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        slingShotVector = initialCursorPosition - CursorPosition;
        slingShotVector = Vector3.ClampMagnitude(slingShotVector, 1.5f);
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position - slingShotVector);
    }
}

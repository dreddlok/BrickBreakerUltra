using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp {Spear, Multi, Expand, Infinity, Boost, Slow, Shrink, Confusion };

public class Pickup : MonoBehaviour {

    private Vector2 storedVelocity;
    public bool bIsPaused = false;
    public PowerUp powerUp;
    public Sprite[] powerUpSprite;
    public GameObject multiBall;
    public float boostFactor = 1.5f;
    public float slowFactor = .5f;

    private void Start()
    {
        // find component in child and assign sprite;
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = powerUpSprite[(int)powerUp];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = FindObjectOfType<Paddle>();
        Ball[] ballsInLevel = FindObjectsOfType<Ball>();

        switch (powerUp)
        {
            case PowerUp.Spear:
                
                Debug.Log("Spear PowerUp attained!");
                paddle.spearActive = paddle.spearPowerupDuration;
                break;
            case PowerUp.Multi:
                for (int i = 0; i < ballsInLevel.Length; i++)
                {
                    GameObject multiBall1 = Instantiate(multiBall, ballsInLevel[i].transform.position, ballsInLevel[i].transform.rotation);
                    GameObject multiBall2 =  Instantiate(multiBall, ballsInLevel[i].transform.position, ballsInLevel[i].transform.rotation);
                }
                Debug.Log("Multi PowerUp attained!");
                break;
            case PowerUp.Expand:
                paddle.expandActive = paddle.expandPowerupDuration;
                Debug.Log("Expand PowerUp attained!");
                break;
            case PowerUp.Infinity:
                paddle.infinityActive = paddle.infinityPowerupDuration;
                Debug.Log("Infinity PowerUp attained!");
                break;
            case PowerUp.Boost:
                for (int i = 0; i < ballsInLevel.Length; i++)
                {
                    if (!ballsInLevel[i].boosted)
                    {
                        ballsInLevel[i].GetComponent<Rigidbody2D>().velocity = ballsInLevel[i].GetComponent<Rigidbody2D>().velocity * boostFactor;
                        ballsInLevel[i].boosted = true;
                    }
                }
                Debug.Log("Boost PowerUp attained!");
                break;
            case PowerUp.Slow:
                for (int i = 0; i < ballsInLevel.Length; i++)
                {
                    if (ballsInLevel[i].slowed == false)
                    {
                        if (ballsInLevel[i].boosted == false)
                        {
                            ballsInLevel[i].GetComponent<Rigidbody2D>().velocity = ballsInLevel[i].GetComponent<Rigidbody2D>().velocity * slowFactor;
                            ballsInLevel[i].slowed = true;
                        }
                        else
                        {
                            ballsInLevel[i].GetComponent<Rigidbody2D>().velocity = (ballsInLevel[i].GetComponent<Rigidbody2D>().velocity / boostFactor) * slowFactor;
                        }
                    }                    
                    ballsInLevel[i].boosted = false;
                }
                Debug.Log("Slow PowerUp attained!");
                break;
            case PowerUp.Shrink:
                paddle.shrinkActive = paddle.shrinkPowerupDuration;
                Debug.Log("Shrink PowerUp attained!");
                break;
            case PowerUp.Confusion:
                paddle.confusionActive = paddle.confusionPowerupDuration;
                Debug.Log("Confusion PowerUp attained!");
                break;
        }
        Destroy(gameObject);

        //TODO Remove temporary Player Save prefad from Level_01    
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
}

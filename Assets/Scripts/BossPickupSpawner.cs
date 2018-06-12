using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPickupSpawner : MonoBehaviour {

    public GameObject pickup;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float currentSpawnCountdown;
    public float spawnOffset;
    public bool bIsPaused = false;

    private void Start()
    {
        currentSpawnCountdown = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update()
    {
           if (!bIsPaused && GetComponent<BossHealth>().bossIsAlive)
        {
            SpawnPickup();
        }
    }

    private void SpawnPickup()
    {
        if (currentSpawnCountdown > 0)
        {
            currentSpawnCountdown -= Time.deltaTime;
        }
        else
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - spawnOffset);
            currentSpawnCountdown = Random.Range(minSpawnTime, maxSpawnTime);
            GameObject spawnedPickup = Instantiate(pickup, spawnPos, transform.rotation);
            int i = Random.Range(0, 2);
            spawnedPickup.GetComponent<SpriteRenderer>().color = Color.red;
            spawnedPickup.GetComponent<Rigidbody2D>().velocity = Vector2.down * 2;
            switch (i)
            {
                case 0:
                    spawnedPickup.GetComponent<Pickup>().powerUp = PowerUp.Shrink;
                    break;
                case 1:
                    spawnedPickup.GetComponent<Pickup>().powerUp = PowerUp.Slow;
                    break;
                case 2:
                    spawnedPickup.GetComponent<Pickup>().powerUp = PowerUp.Confusion;
                    break;
            }
        }
    }
}

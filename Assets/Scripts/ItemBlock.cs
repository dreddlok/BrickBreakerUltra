using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : MonoBehaviour {

    public GameObject pickup;
    //public int[] possiblePickups;
    public List<PowerUp> possiblePickups = new List<PowerUp>();
    public PowerUp powerUp;

    [Header("Pickups")]
    public bool spear;
    public bool multi;
    public bool expand;
    public bool infinity;
    public bool boost;
    public bool slow;
    public bool shrink;
    public bool confusion;

    private void Start()
    {
        if (spear)
        {
            possiblePickups.Add(PowerUp.Spear);
        }
        if (multi)
        {
            possiblePickups.Add(PowerUp.Multi);
        }
        if (expand)
        {
            possiblePickups.Add(PowerUp.Expand);
        }
        if (infinity)
        {
            possiblePickups.Add(PowerUp.Infinity);
        }
        if (boost)
        {
            possiblePickups.Add(PowerUp.Boost);
        }
        if (slow)
        {
            possiblePickups.Add(PowerUp.Slow);
        }
        if (shrink)
        {
            possiblePickups.Add(PowerUp.Shrink);
        }
        if (confusion)
        {
            possiblePickups.Add(PowerUp.Confusion);
        }

        powerUp = possiblePickups[Random.Range(0, possiblePickups.Count)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject spawnedPickup = Instantiate(pickup, transform.position, transform.rotation);
        spawnedPickup.GetComponent<Pickup>().powerUp = powerUp;
        if (spawnedPickup.GetComponent<Pickup>().powerUp == PowerUp.Shrink || spawnedPickup.GetComponent<Pickup>().powerUp == PowerUp.Slow || spawnedPickup.GetComponent<Pickup>().powerUp == PowerUp.Confusion)
        {
            spawnedPickup.GetComponent<SpriteRenderer>().color = Color.red;
        }
        Destroy(gameObject);
    }
}

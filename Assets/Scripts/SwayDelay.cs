using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayDelay : MonoBehaviour {

    private Animator animator;
    public float delayMin = 0;
    public float delayMax = 1;
    public bool hasSwayStarted = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(0, -1, Random.Range(delayMin, delayMax));
        //StartCoroutine(StartSway());
    }

    public IEnumerator StartSway()
    {
        yield return new WaitForSeconds(Random.Range(delayMin,delayMax));
        //animator.SetTrigger("Sway Animation");
        hasSwayStarted = true;
    }

}

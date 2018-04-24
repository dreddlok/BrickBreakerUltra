using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    public int level;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    private Button button;
    public float delay;
    public bool juiceAdded = false;

    private void Awake()
    {
        delay = level * .1f;
    }

    private void AddJuice()
    {
        iTween.PunchPosition(this.gameObject, Vector3.up * 30, 2f);
    }

    private void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime * 1;
        } else if (!juiceAdded)
        {
            AddJuice();
            juiceAdded = true;
        }
    }

    private void OnGUI()
    {        
        PlayerSave playerData = FindObjectOfType<PlayerSave>();
        button = GetComponent<Button>();
        if (playerData.level[level])
        {
            button.image.sprite = unlockedSprite;
            button.interactable = true;
        }
        else
        {
            button.image.sprite = lockedSprite;
            button.interactable = false;
        }
    }

}

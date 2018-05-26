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
    public float punchAmount = 1f;

    private void Awake()
    {
        delay = Random.Range(0.0f, 1f); //(level * .1f) +.3f;
        Cursor.visible = true;
    }

    private void AddJuice()
    {
        Vector3 punchVector = new Vector3(punchAmount, punchAmount);
        iTween.PunchScale(this.gameObject, Vector3.one * .5f, 2f);
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

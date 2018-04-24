using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SsCheckBox : MonoBehaviour {

    private PlayerSave playerSave;
    private Button button;

    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private void Start()
    {
        playerSave = FindObjectOfType<PlayerSave>();
        button = GetComponent<Button>();
    }

    private void OnGUI()
    {
        if (FindObjectOfType<PlayerSave>().screenShake)
        {
            button.image.sprite = activeSprite;
        }
        else
        {
            button.image.sprite = inactiveSprite;
        }
    }

    public void toggle()
    {
        FindObjectOfType<PlayerSave>().screenShake = !FindObjectOfType<PlayerSave>().screenShake;
        Debug.Log("screen shake toggled");
    }
}

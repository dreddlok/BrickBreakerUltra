using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreen : MonoBehaviour {

    public void CloseHelp()
    {
        Canvas[] canvas = FindObjectsOfType<Canvas>();
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].tag == "Menu")
            {
                canvas[i].enabled = !canvas[i].enabled;
            }
        }

        Destroy(this);
    }

}

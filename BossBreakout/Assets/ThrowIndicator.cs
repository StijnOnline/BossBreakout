using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowIndicator : MonoBehaviour
{
    public static ThrowIndicator activeIndicator;
    public TextMeshPro text;

    void Start()
    {
        activeIndicator = this;
    }


    public void SetIndicator(int dir, int type)
    {
        string txt = "";
        switch(dir) {
            case 0: txt += "Left "; break;
            case 1: txt += "Down "; break;
            case 2: txt += "Right "; break;
        }
        switch(type) {
            case 0: txt += "Normal "; break;
            case 1: txt += "Spiked "; break;
            case 2: txt += "Curved "; break;
        }
        text.SetText(txt);
    }
}

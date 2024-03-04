using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckConnection : MonoBehaviour
{
    public bool Connected = false;
    public GameObject Screen;
    public TMP_Text dontKnow;
    public GameObject connectionCheck;

    public void proceed()
    {
        if (Connected)
        {
            connectionCheck.SetActive(false);
            Screen.SetActive(true);
        }
        else
        {
            dontKnow.color = Color.red;
        }
    }
}

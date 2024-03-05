using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("testlevel1_Art");
        }
        else
        {
            dontKnow.color = Color.red;
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("testlevel1_Art");
    }
}

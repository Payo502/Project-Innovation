using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup UIgroup;
    public string nextScene; // Name of the scene to switch to
    private bool switching;
    [SerializeField] string sound;
    public GameObject Screen;
    public bool Working;
    public string Url;

    void Update()
    {
        if (Working)
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0) && switching == false)
            {
                switching = true;
                Debug.Log("played sound " + sound);
                this.GetComponent<ServerMessageManager>().SendStringMessagesToClient(ServerToClientId.stringMessage, sound);
                StartCoroutine(loadingScene());
            }
        }
        if (switching)
        {
            UIgroup.alpha -= 0.01f;
        }

    }
    
    public void lost()
    {
        Application.OpenURL(Url);
    }

    IEnumerator loadingScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(nextScene);
    }
   
}

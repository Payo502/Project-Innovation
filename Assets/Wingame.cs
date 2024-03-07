using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wingame : MonoBehaviour
{
    public GameObject winScreen;
    [SerializeField] string sound;
    public CanvasGroup UIgroup;
    bool winning = false;

    private void Update()
    {
        if (winning)
        {
            UIgroup.alpha += 0.01f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetActive(true);
            ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, sound);
            winning = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingame : MonoBehaviour
{
    public GameObject winScreen;
    [SerializeField] string sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetActive(true);
            ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, sound);
        }
    }
}

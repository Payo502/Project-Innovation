using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiotrigger : MonoBehaviour
{
    [SerializeField] bool hasBeenHeard;
    [SerializeField] string sound;

    public void ActivateTrigger()
    {
        if (!hasBeenHeard)
        {
            Debug.Log("played sound " + sound);
            ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, sound);
            hasBeenHeard = true;
        }
    }
}

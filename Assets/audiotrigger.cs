using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiotrigger : MonoBehaviour
{
    [SerializeField] bool hasBeenHeard;
    [SerializeField] string sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

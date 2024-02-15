using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

[Serializable]
public struct SoundEvent
{
    public string name;
    public UnityEvent execute;
}

public class EventManager : MonoBehaviour
{
    [SerializeField] public List<SoundEvent> soundEvents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void soundPlayed (string ss)
    {
        foreach (var sound in soundEvents)
        {
            if (sound.name == ss)
            {
                sound.execute.Invoke();
            }
        }
    }
}

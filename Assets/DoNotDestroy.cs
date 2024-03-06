using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    public AudioSource audioSource;
    private void Awake()
    {
        //Keeps the audio playing through the game
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}

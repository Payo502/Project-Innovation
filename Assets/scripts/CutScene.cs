using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public string nextScene; // Name of the next scene

    void Start()
    {
        // Check if the audioSource is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        // Subscribe to the AudioSource's end event
        audioSource.loop = false;
        audioSource.Play();
    }

    void Update()
    {
        // Check if the audio has finished playing
        if (!audioSource.isPlaying)
        {
            // Load the next scene
            SceneManager.LoadScene(nextScene);
        }
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

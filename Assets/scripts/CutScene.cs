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
        StartCoroutine(WaitForCutscene());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            audioSource.Stop();
            SceneManager.LoadScene(nextScene);
        }
    }

    IEnumerator WaitForCutscene()
    {
        yield return new WaitForSeconds(25.7f);
        SceneManager.LoadScene(nextScene);
    }
}

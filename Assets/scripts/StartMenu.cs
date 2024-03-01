using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string nextScene; // Name of the scene to switch to

    void Update()
    {
        // Check if any button is pressed
        if (Input.anyKeyDown)
        {
            // Load the scene with the specified name
            SceneManager.LoadScene(nextScene);
        }
    }

    void OnMouseDown() 
    { 
        SceneManager.LoadScene(nextScene);
    } 
   
}

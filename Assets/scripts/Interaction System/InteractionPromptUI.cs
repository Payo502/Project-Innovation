using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    private void Start()
    {
        mainCamera = Camera.main;
        UIPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward,
            rotation * Vector3.up);
    }

    public bool IsDisplayed = false;

    public void SetUp(string _promptText)
    {
        promptText.text = _promptText;
        UIPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        UIPanel.SetActive(false);
        IsDisplayed = false;
    }
}

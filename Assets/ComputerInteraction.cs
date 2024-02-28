using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private int newFrequency = 2;
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        PlayerState player = interactor.GetComponent<PlayerState>();
        if (player != null)
        {
            player.ChangeFrequency(newFrequency);
            return true;
        }

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Frequency newFrequency;
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        PlayerState player = interactor.GetComponent<PlayerState>();
        if (player != null)
        {
            player.ChangeFrequency(newFrequency);
            ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, "Guard");
            return true;
        }

        return false;
    }
}

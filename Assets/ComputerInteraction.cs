using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Frequency newFrequency;
    [SerializeField] private string prompt;

    float enumVaue;

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        PlayerState player = interactor.GetComponent<PlayerState>();
        if (player != null)
        {
            player.ChangeFrequency(newFrequency);
            ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, "Guard");
            
            if (player.GetFrequency() == Frequency.frequency01)
            {
                enumVaue = 176f;
            }else if (player.GetFrequency() == Frequency.frequency02)
            {
                enumVaue = 254f;
            }else if(player.GetFrequency() == Frequency.frequency03)
            {
                enumVaue = 324f;
            }

            ServerMessageManager.Singleton.SendFloatMessagesToClient(ServerToClientId.floatMessage, enumVaue);
            return true;
        }

        return false;
    }
}

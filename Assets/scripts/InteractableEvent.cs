using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEvent : MonoBehaviour, IInteractabe
{
    public void Interact()
    {
        Debug.Log("You are interacting");
    }
}

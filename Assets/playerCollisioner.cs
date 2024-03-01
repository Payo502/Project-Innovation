using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollisioner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var comp = other.gameObject.GetComponent<audiotrigger>();
        if (comp != null)
        {
            comp.ActivateTrigger();
        }
    }
}

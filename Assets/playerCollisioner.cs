using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollisioner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var comp = other.gameObject.GetComponent<audiotrigger>();
        if (comp != null)
        {
            comp.ActivateTrigger();
        }
    }
}

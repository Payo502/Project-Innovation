using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkEnabled : MonoBehaviour
{
    public GameObject Manager;
    private void OnEnable()
    {
        StartCoroutine(Enabled());
    }

    IEnumerator Enabled()
    {
        yield return new WaitForSeconds(0.75f);
        Manager.GetComponent<StartMenu>().Working = true;
    }
}

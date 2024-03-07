using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;
using System;

[Serializable]
public struct GuardLocation
{
    public string name;
    public Frequency locationFrequency;
    public Transform guardLocation;
}


public class PlayerState : MonoBehaviour
{
    [Header("Developer Options")]
    [SerializeField] bool isDebugging;

    [Header("Frequency")]
    [SerializeField] private Frequency currentFrequency;

    [Header("State")]
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private vThirdPersonController playerController;
    [SerializeField] private playerstate mystate;
    [SerializeField] private bool testphonecontrols;

    private float SpeedNow;
    private float SpeedCrouchNow;
    [Header("Speed")]
    [SerializeField] private float SpeedMin;
    [SerializeField] private float SpeedMove;
    [SerializeField] private float SpeedPhone;
    [SerializeField] private float SpeedCrouch;

    [Header("Guards Locations")]
    [SerializeField] GuardLocation[] guardLocations;

    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        playerController.freeSpeed.walkSpeed = SpeedMin;
        playerController.freeSpeed.runningSpeed = SpeedMove;
        playerController.freeSpeed.sprintSpeed = SpeedPhone;

        playerController.strafeSpeed.walkSpeed = SpeedMin;
        playerController.strafeSpeed.runningSpeed = SpeedMove;
        playerController.strafeSpeed.sprintSpeed = SpeedPhone;

        ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, "Arrived");
    }

    // Update is called once per frame
    void Update()
    {
        switch (mystate)
        {
            case playerstate.move:
            default:
                SpeedNow = SpeedMove;
                SpeedCrouchNow = SpeedCrouch;
                break;
            case playerstate.phone:
                SpeedNow = SpeedPhone;
                SpeedCrouchNow = SpeedMin;
                break;
            case playerstate.machine:
                SpeedNow = SpeedMin;
                SpeedCrouchNow = SpeedMin;
                if (!isInteracting)
                {
                    mystate = playerstate.move;
                }
                break;
        }
        playerController.freeSpeed.runningSpeed = SpeedNow;
        playerController.strafeSpeed.runningSpeed = SpeedNow;
        playerController.freeSpeed.sprintSpeed = SpeedCrouchNow;
        playerController.strafeSpeed.sprintSpeed = SpeedCrouchNow;

        if (isDebugging)
        {
            DebugInput();

        }

        /*        if (Input.GetKeyDown("space"))
                {
                    ServerMessageManager.Singleton.SendStringMessagesToClient(ServerToClientId.stringMessage, "tape1");

                }*/
    }

    public void PickupPhone(bool Pickedup)
    {
        if (Pickedup) { if (mystate == playerstate.move) { mystate = playerstate.phone; } } else { if (mystate == playerstate.phone) { mystate = playerstate.move; } }
    }

    private void OnTriggerEnter(Collider other)
    {
        var comp = other.gameObject.GetComponent<audiotrigger>();
        if (comp != null)
        {
            comp.ActivateTrigger();
        }
    }

    public void DirectGuardsToLocation()
    {
        foreach (GuardLocation guardLocation in guardLocations)
        {
            if (guardLocation.locationFrequency == currentFrequency)
            {
                GameObject[] guardObjects = GameObject.FindGameObjectsWithTag("Guard");
                foreach (GameObject guard in guardObjects)
                {
                    GuardManager guardManager = guard.GetComponentInChildren<GuardManager>();
                    if (guardManager != null && guardManager.currentFrequency == currentFrequency)
                    {
                        guardManager.GetComponentInParent<GuardLocomotion>().MoveToLocation(guardLocation.guardLocation.position);
                    }
                }
            }
        }
    }

    public void ChangeFrequency(Frequency newFrequency)
    {
        currentFrequency = newFrequency;
    }

    public Frequency GetFrequency()
    {
        return currentFrequency;
    }

    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Directing Guards to location");
            DirectGuardsToLocation();
        }
    }

    public void ScreamReceived(bool screamReceived)
    {
        if (screamReceived)
        {
            Debug.Log("Directing Guards to location");
            DirectGuardsToLocation();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;
using System;


public class PlayerState : MonoBehaviour
{
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

    [Header("Controlling Guards")]
    [SerializeField] private Transform guardsLocation;

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

        if (Input.GetKeyDown("space"))
        {
            GameObject.Find("networkManager").GetComponent<ServerMessageManager>().SendStringMessagesToClient(ServerToClientId.stringMessage, "tape1");

        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            DirectGuardsToLocation(guardsLocation.position);
        }
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

    public void DirectGuardsToLocation(Vector3 location)
    {
        GameObject[] guardObjects = GameObject.FindGameObjectsWithTag("Guard");

        foreach (GameObject guard in guardObjects)
        {
            GuardManager guardManager = guard.GetComponentInChildren<GuardManager>();
            if (guardManager != null && guardManager.currentFrequency == currentFrequency)
            {
                guardManager.GetComponentInParent<GuardLocomotion>().MoveToLocation(location);
                Debug.Log(guardManager);
            }
        }
    }

    public void ChangeFrequency(Frequency newFrequency)
    {
        currentFrequency = newFrequency;
    }
}

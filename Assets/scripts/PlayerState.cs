using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;
using System;

public class PlayerState : MonoBehaviour
{
    enum playerstate
    {
        move,
        stop,
        phone,
        machine
    };

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

    private bool isInteracting;
    private GameObject interactObject;
    [Header("Interacting")]
    [SerializeField] private float interactSize;
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private LayerMask interactMask;


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

                if (isInteracting && Input.GetKeyDown(KeyCode.E))
                {
                    mystate = playerstate.machine;
                }
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



    }

    private void FixedUpdate()
    {
        if (mystate == playerstate.move || mystate == playerstate.machine)
        {
            isInteracting = false;
            RaycastHit hit;
            if (Physics.SphereCast(bodyTransform.position, interactSize, bodyTransform.forward, out hit, maxDistance, interactMask))
            {
                float distanceToObstacle = hit.distance;
                isInteracting = true;
            }
            if (isInteracting && mystate != playerstate.machine) { interactUI.SetActive(true); } else { interactUI.SetActive(false); }
        }
    }

    public void PickupPhone(bool Pickedup)
    {
        if (Pickedup) { if (mystate == playerstate.move) { mystate = playerstate.phone; } } else { if (mystate == playerstate.phone) { mystate = playerstate.move; } }
    }
}

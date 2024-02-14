using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;


public class PlayerState : MonoBehaviour
{
    enum playerstate
    {
        move,
        stop,
        phone
    };

    [Header("State")]
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
        switch (mystate) {
            case playerstate.move:
            default:
                SpeedNow = SpeedMove;
                SpeedCrouchNow = SpeedCrouch;
                break;
            case playerstate.phone:
                SpeedNow = SpeedPhone;
                SpeedCrouchNow = SpeedMin;
                break;
        }
        playerController.freeSpeed.runningSpeed = SpeedNow;
        playerController.strafeSpeed.runningSpeed = SpeedNow;
        playerController.freeSpeed.sprintSpeed = SpeedCrouchNow;
        playerController.strafeSpeed.sprintSpeed = SpeedCrouchNow;

        if (testphonecontrols && Input.GetKeyDown("space"))
        {
            PickupPhone((mystate!=playerstate.phone));
        }
    }

    public void PickupPhone(bool Pickedup)
    {
        if (Pickedup) { if (mystate == playerstate.move) { mystate = playerstate.phone; } } else { if (mystate == playerstate.phone) { mystate = playerstate.move; } }
    }
}

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

    private float SpeedNow;
    [SerializeField] private float SpeedMin;
    [SerializeField] private float SpeedMove;
    [SerializeField] private float SpeedPhone;
    [SerializeField] private float SpeedCrouch;

    [SerializeField] private vThirdPersonController playerController;

    [SerializeField] private playerstate mystate;


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
                break;
            case playerstate.phone:
                SpeedNow = SpeedPhone;
                break;
        }
        playerController.freeSpeed.runningSpeed = SpeedNow;
        playerController.strafeSpeed.runningSpeed = SpeedMove;

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public enum AlertStage
{
    Peaceful,
    Intrigued,
    Alerted
}

public class GuardManager : MonoBehaviour
{
    public float fov;
    [Range(0, 360)] public float fovAngle; // in degrees

    public GuardMovement Movement;

    public AlertStage alertStage;
    [Range(0, 100)] public float alertLevel; // 0: Peaceful, 100: Alerted

    public Vector3 interestPosition;

    private void Awake()
    {
        alertStage = AlertStage.Peaceful;
        alertLevel = 0;
    }

    private void FixedUpdate()
    {
        bool playerInFOV = false;
        float signedAngle = Vector3.Angle(transform.forward, Movement.Player.transform.position - transform.position);
        if (Mathf.Abs(signedAngle) < fovAngle / 2)
        {
            Debug.Log("Layer 1");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, fov))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "ThirdPersonController_LITE")
                {
                    Debug.Log("Layer 2");
                }/*
                if (hit.collider.name == "ThirdPersonController_LITE")
                {
                    playerInFOV = true;
                    Debug.Log("Layer 3");
                }*/
            }
        }
        _UpdateAlertState(playerInFOV);

    }

    private void _UpdateAlertState(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Peaceful:
                if (playerInFOV && Movement.canDo)
                {
                    alertStage = AlertStage.Intrigued;
                }
                alertLevel = 0;
                break;
            case AlertStage.Intrigued:
                if (playerInFOV)
                {
                    if (Movement.canDo)
                    {
                        alertLevel++;
                        StartCoroutine(gettingAngry());
                    }
                }
                else
                {
                    alertLevel--;
                    StartCoroutine(gettingAngry());
                }
                break;
            case AlertStage.Alerted:
                if (!playerInFOV)
                {
                    alertStage = AlertStage.Intrigued;
                }
                break;
        }
    }

    IEnumerator gettingAngry()
    {
        if (alertLevel >= 100)
        {
            alertLevel = 100;
            yield return new WaitForSeconds(1f);
            alertStage = AlertStage.Alerted;

        }
        else if (alertLevel <= 0)
        {
            alertLevel = 0;
            yield return new WaitForSeconds(1f);
            alertStage = AlertStage.Peaceful;
        }
    }

}

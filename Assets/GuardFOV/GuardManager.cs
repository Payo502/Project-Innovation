using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        alertStage = AlertStage.Peaceful;
        alertLevel = 0;
    }

    private void Update()
    {
        bool playerInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(
            transform.position, fov);
        foreach (Collider c in targetsInFOV)
        {
            if (c.CompareTag("Player") && Movement.canDo)
            {
                float signedAngle = Vector3.Angle(
                    transform.forward,
                    c.transform.position - transform.position);
                if (Mathf.Abs(signedAngle) < fovAngle / 2)
                    playerInFOV = true;
                break;
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

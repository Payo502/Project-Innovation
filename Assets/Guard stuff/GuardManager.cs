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
    [Range(0, 360)] public float fovAngle;

    public AlertStage alertStage;
    [Range(0, 100)] public float alertLevel;

    [SerializeField] LayerMask obstacleLayer;

    GuardLocomotion guardLocomotion;

    private void Awake()
    {
        alertStage = AlertStage.Peaceful;
        alertLevel = 0;

        guardLocomotion = GetComponentInParent<GuardLocomotion>();
    }

    private void Update()
    {
        bool playerInFOV = false;
        Collider[] targetsInFOV = Physics.OverlapSphere(
            transform.position, fov);
        foreach (Collider c in targetsInFOV)
        {
            if (c.CompareTag("Player"))
            {
                Vector3 directionToTarget = (c.transform.position - transform.position).normalized;
                float distanceToTarget = Vector3.Distance(transform.position, c.transform.position);
                float signedAngle = Vector3.Angle(
                        transform.forward,
                        directionToTarget);

                if (Mathf.Abs(signedAngle) < fovAngle / 2)
                {
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                    {
                        playerInFOV = true;
                        break;
                    }

                }
            }
        }

        _UpdateAlertState(playerInFOV);
    }

    private void _UpdateAlertState(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Peaceful:
                if (playerInFOV)
                    alertStage = AlertStage.Intrigued;
                break;
            case AlertStage.Intrigued:
                if (playerInFOV)
                {
                    alertLevel++;
                    if (alertLevel >= 100)
                    {
                        alertStage = AlertStage.Alerted;
                        FindObjectOfType<GameManager>().EndGame();
                    }
                }
                else
                {
                    alertLevel--;
                    if (alertLevel <= 0)
                    {
                        alertStage = AlertStage.Peaceful;
                        guardLocomotion.ResumePatrolling();
                    }
                }
                break;
            case AlertStage.Alerted:
                if (!playerInFOV)
                    alertStage = AlertStage.Intrigued;
                break;
        }
    }
}



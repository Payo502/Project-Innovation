using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AlertStage
{
    Peaceful,
    Intrigued,
    Alerted
}

public class GuardManager : MonoBehaviour
{
    [Header("How fast the guard detects you")]
    [SerializeField] float detectionRate = 0.1f;

    [Header("FOV Settings")]
    public float fov;
    [Range(0, 360)] public float fovAngle;

    public AlertStage alertStage;
    [Range(0, 100)] public float alertLevel;

    [SerializeField] LayerMask obstacleLayer;

    GuardLocomotion guardLocomotion;

    public Frequency currentFrequency;

    [Header("UI Elements")]
    [SerializeField] GameObject warningExclamationMark;

    private void Awake()
    {
        alertStage = AlertStage.Peaceful;
        warningExclamationMark.SetActive(false);
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
                { 

                    alertStage = AlertStage.Intrigued;
                }

                warningExclamationMark.SetActive(false); 

                break;
            case AlertStage.Intrigued:
                if (playerInFOV)
                {
                    warningExclamationMark.SetActive(true);

                    alertLevel = alertLevel + detectionRate;
                    if (alertLevel >= 100)
                    {
                        alertStage = AlertStage.Alerted;
                        EndGame();
                    }
                }
                else
                {
                    alertLevel = alertLevel - detectionRate;
                    if (alertLevel <= 0)
                    {
                        alertStage = AlertStage.Peaceful;
                        guardLocomotion.ResumePatrolling();
                    }
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

    void EndGame()
    {
        FindObjectOfType<GameManager>().EndGame();
    }
}



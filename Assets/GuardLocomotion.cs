using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardLocomotion : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    [SerializeField] List<Transform> waypoints;

    [SerializeField] float distanceThreshold;
    [SerializeField] float stopDuration = 1f;

    private bool isStopped = false;

    private float stopTimer = 0f;

    int waypointIndex;
    Vector3 target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateDestination();
    }

    private void Update()
    {
        if (!isStopped && !agent.pathPending && agent.remainingDistance < distanceThreshold)
        {
            if (stopTimer <= 0f)
            {
                StartCoroutine(StopAtWaypoint());
            }
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private IEnumerator StopAtWaypoint()
    {
        isStopped = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(stopDuration); 


        agent.isStopped = false;
        isStopped = false;

        IterateWaypointIndex();
        UpdateDestination();
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    private void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Count)
        {
            waypointIndex = 0;
            Debug.Log(waypointIndex);
        }
    }
}

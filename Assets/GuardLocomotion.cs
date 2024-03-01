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

    GuardManager guardManager;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        guardManager = GetComponentInChildren<GuardManager>();
    }

    private void Start()
    {
        UpdateDestination();
    }

    private void Update()
    {
        if (!isStopped && !agent.pathPending && agent.remainingDistance < distanceThreshold)
        {
            if (stopTimer <= 0f && guardManager.alertStage != AlertStage.Alerted)
            {
                StartCoroutine(StopAtWaypoint());
            }
        }
        else if (guardManager.alertStage == AlertStage.Alerted)
        {
            // Stop the NavMeshAgent and look at the player
            agent.isStopped = true;
            LookAtPlayer();
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

    private void LookAtPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        }
    }

    public void MoveToLocation(Vector3 location)
    {
        if (agent != null)
        {
            agent.SetDestination(location);
            isStopped = false;
        }
    }

    public void ResumePatrolling()
    {
        if (guardManager.alertStage == AlertStage.Peaceful)
        {
            agent.isStopped = false;
            UpdateDestination();
        }
    }
}

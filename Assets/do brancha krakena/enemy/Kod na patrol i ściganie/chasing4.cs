using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chasing4 : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex = 0;
    Vector3 target;

    [SerializeField] fieldOfView fov;
    public Transform targetObject;

    private bool isChasing = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    private void Update()
    {
        if (!isChasing)
        {
            Patrol();
        }
        else
        {
            Chase();
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }

        if (fov.canSeePlayer && Vector3.Distance(transform.position, targetObject.position) < 10f)
        {
            target = targetObject.position;
            isChasing = true;
        }

        agent.SetDestination(target);
    }

    private void Chase()
    {
        if (!fov.canSeePlayer || Vector3.Distance(transform.position, targetObject.position) > 15f)
        {
            isChasing = false;
            UpdateDestination();
            return;
        }

        target = targetObject.position;
        agent.SetDestination(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    private void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
    }

    private void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}

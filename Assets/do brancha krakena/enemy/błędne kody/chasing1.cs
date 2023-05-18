using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chasing1 : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    [SerializeField] fieldOfView fov;
    public Transform Target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        if (Target != null)
        {
            if (fov.canSeePlayer)
            {
                target = Target.position;
            }
            else
            {
                if (Vector3.Distance(transform.position, target) < 1)
                {
                    IterateWaypointInedx();
                    UpdateDestination();
                }
            }
        }
        agent.SetDestination(target);
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
    }

    void IterateWaypointInedx()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        Target = newTarget;
    }
}

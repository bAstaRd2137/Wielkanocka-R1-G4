using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chasing3 : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex = 0; // wartoœæ waypointIndex zosta³a zainicjowana na 0
    Vector3 target;

    [SerializeField] fieldOfView fov;
    public Transform Target;

    private Vector3 initialPosition; // nowa zmienna przechowuj¹ca pocz¹tkow¹ pozycjê AI

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        initialPosition = transform.position; // zapisanie pocz¹tkowej pozycji AI przy starcie
    }

    void Update()
    {
        if (Target != null)
        {
            if (fov.canSeePlayer && Vector3.Distance(transform.position, Target.position) < 10f)
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
        else // jeœli nie ma celu, to AI patroluje po waypointach
        {
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointInedx();
                UpdateDestination();
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

    private void OnCollisionEnter(Collision collision) // obs³uga kolizji z graczem
    {
        if (collision.gameObject.CompareTag("Player")) // jeœli kolizja jest z graczem
        {
            agent.ResetPath(); // resetowanie trasy
            transform.position = initialPosition; // AI wraca na pocz¹tkow¹ pozycjê
            Target = null; // usuwanie celu
        }
    }
}


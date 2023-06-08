using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovementAndChasing : MonoBehaviour
{
    public Transform[] waypoints;
    public float movementSpeed = 3f;
    public float waitTime = 2f;
     

    private int currentWaypointIndex = 0;
    private Transform currentWaypoint;
    private bool isWaiting = false;

    public fieldOfView fov; // Referencja do skryptu fieldOfView
    private bool isChasingPlayer = false; // Czy NPC œciga gracza

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0];
        }

        fov = GetComponent<fieldOfView>(); // Pobierz komponent fieldOfView z tego samego obiektu
    }

    private void Update()
    {

        if (waypoints.Length == 0 || isWaiting)
            return;

        if (!isChasingPlayer)
        {
            MoveToWaypoint();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void MoveToWaypoint()
    {
        Vector3 direction = currentWaypoint.position - transform.position;
        direction.Normalize();

        transform.Translate(direction * movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.position) <= 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            currentWaypoint = waypoints[currentWaypointIndex];
        }
        else
        {
            // SprawdŸ, czy zobaczono gracza w polu widzenia
            if (fov.canSeePlayer)
            {
                isChasingPlayer = true;
                Debug.Log("Zauwa¿ono gracza! Œciganie rozpoczête.");
            }
        }
    }

    private void ChasePlayer()
    {
        if (fov.canSeePlayer)
        {
            Vector3 direction = fov.playerRef.transform.position - transform.position;
            direction.Normalize();

            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
        else
        {
            isChasingPlayer = false;
            Debug.Log("Gracz nie jest ju¿ widoczny. Œciganie zakoñczone.");
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}

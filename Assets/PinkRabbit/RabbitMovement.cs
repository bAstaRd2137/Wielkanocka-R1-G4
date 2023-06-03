using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    public Transform[] waypoints; // Tablica punktów docelowych (waypoints)
    public float movementSpeed = 3f; // Szybkoœæ ruchu NPC
    public float waitTime = 2f; // Czas zatrzymania na ka¿dym waypointcie

    private int currentWaypointIndex = 0; // Aktualny indeks waypointu
    private Transform currentWaypoint; // Aktualny waypoint
    private bool isWaiting = false; // Czy NPC jest zatrzymany

    private void Start()
    {
        // SprawdŸ, czy tablica waypointów jest pusta
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0];
        }
    }

    private void Update()
    {
        // SprawdŸ, czy tablica waypointów jest pusta lub NPC jest zatrzymany
        if (waypoints.Length == 0 || isWaiting)
            return;

        // Oblicz kierunek ruchu
        Vector3 direction = currentWaypoint.position - transform.position;
        direction.Normalize();

        // Przesuñ NPC w kierunku waypointu
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // SprawdŸ, czy NPC osi¹gn¹³ waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) <= 0.1f)
        {
            // Zatrzymaj NPC na okreœlony czas
            StartCoroutine(WaitAtWaypoint());

            // PrzejdŸ do nastêpnego waypointu
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            currentWaypoint = waypoints[currentWaypointIndex];
        }
    }

    private System.Collections.IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitAnimations : MonoBehaviour
{
    public Animator animator; // Komponent Animator NPC
    public float movementThreshold = 0.1f; // Próg ruchu, poni¿ej którego NPC jest uznawany za stoj¹cego

    private Vector3 previousPosition; // Poprzednia pozycja NPC

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        // Oblicz wektor przemieszczenia NPC
        Vector3 displacement = transform.position - previousPosition;

        // SprawdŸ, czy NPC porusza siê powy¿ej progu ruchu
        if (displacement.magnitude > movementThreshold)
        {
            // Ustaw animator na animacjê chodzenia
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // Ustaw animator na animacjê spoczynku
            animator.SetBool("IsWalking", false);
        }

        previousPosition = transform.position;
    }
}

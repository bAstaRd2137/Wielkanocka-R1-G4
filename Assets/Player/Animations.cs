using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator; // Komponent Animator postaci
    private bool isMoving = false; // Czy postaæ siê porusza

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // SprawdŸ, czy gracz porusza siê
        isMoving = Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f;

        // Ustaw odpowiedni stan animacji
        if (isMoving)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
}

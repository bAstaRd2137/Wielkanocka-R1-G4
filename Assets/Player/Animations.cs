using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator; // Komponent Animator postaci
    private bool isMoving = false; // Czy posta� si� porusza

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Sprawd�, czy gracz porusza si�
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

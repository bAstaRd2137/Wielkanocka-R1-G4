using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    public AudioClip footstepSound;
    public AudioSource audioSource;

    private CharacterController characterController;
    private bool isMoving = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();

        if (movement.magnitude > 0f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (!isMoving)
            {
                isMoving = true;
                PlayFootstepSound();
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                StopFootstepSound();
            }
        }

        characterController.SimpleMove(movement * moveSpeed);
    }

    private void PlayFootstepSound()
    {
        audioSource.clip = footstepSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void StopFootstepSound()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}

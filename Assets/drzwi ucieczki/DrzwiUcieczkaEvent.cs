using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrzwiUcieczkaEvent : MonoBehaviour
{
    public GameObject doors; // Referencja do drzwi
    private bool isPlayerInRange = false; // Flaga wskazuj�ca, czy gracz jest w zasi�gu drzwi
    private bool isKeyCollected = false; // Flaga wskazuj�ca, czy klucz zosta� zebrany

    private void OnEnable()
    {
        PodniesienieKluczaEvent.KeyCollectedEvent += EnableDoors;
    }

    private void OnDisable()
    {
        PodniesienieKluczaEvent.KeyCollectedEvent -= EnableDoors;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && isKeyCollected)
        {
            OpenDoors();
        }
    }

    private void EnableDoors()
    {
        isKeyCollected = true;
    }

    private void OpenDoors()
    {
        doors.SetActive(false);
    }
}

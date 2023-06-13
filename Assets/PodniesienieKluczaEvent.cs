using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PodniesienieKluczaEvent : MonoBehaviour
{
    public GameObject emptyCrateUI; // Referencja do UI pustej kratki
    public GameObject keyCrateUI; // Referencja do UI kratki z kluczem
    public AudioClip collectSound; // D�wi�k zebrania klucza

    public delegate void KeyCollectedEventHandler();
    public static event KeyCollectedEventHandler KeyCollectedEvent; // Event informuj�cy o zebraniu klucza

    private bool isPlayerInRange = false; // Flaga wskazuj�ca, czy gracz jest w zasi�gu klucza
    private bool isKeyCollected = false; // Flaga wskazuj�ca, czy klucz zosta� zebrany

    private AudioSource audioSource; // Komponent AudioSource

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        emptyCrateUI.SetActive(true); // Pocz�tkowo w��czamy UI pustej kratki
        keyCrateUI.SetActive(false); // Pocz�tkowo wy��czamy UI kratki z kluczem
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
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isKeyCollected)
        {
            CollectKey();
        }
    }

    private void CollectKey()
    {
        // Wy��cz obiekt klucza
        gameObject.SetActive(false);

        // Wy��cz obiekt UI pustej kratki
        emptyCrateUI.SetActive(false);

        // W��cz obiekt UI kratki z kluczem
        keyCrateUI.SetActive(true);

        isKeyCollected = true;

        // Odtw�rz d�wi�k zebrania klucza
        audioSource.PlayOneShot(collectSound);

        // Wywo�aj event informuj�cy o zebraniu klucza
        KeyCollectedEvent?.Invoke();
    }
}
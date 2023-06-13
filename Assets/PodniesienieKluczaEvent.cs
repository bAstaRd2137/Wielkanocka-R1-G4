using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PodniesienieKluczaEvent : MonoBehaviour
{
    public GameObject emptyCrateUI; // Referencja do UI pustej kratki
    public GameObject keyCrateUI; // Referencja do UI kratki z kluczem
    public AudioClip collectSound; // DŸwiêk zebrania klucza

    public delegate void KeyCollectedEventHandler();
    public static event KeyCollectedEventHandler KeyCollectedEvent; // Event informuj¹cy o zebraniu klucza

    private bool isPlayerInRange = false; // Flaga wskazuj¹ca, czy gracz jest w zasiêgu klucza
    private bool isKeyCollected = false; // Flaga wskazuj¹ca, czy klucz zosta³ zebrany

    private AudioSource audioSource; // Komponent AudioSource

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        emptyCrateUI.SetActive(true); // Pocz¹tkowo w³¹czamy UI pustej kratki
        keyCrateUI.SetActive(false); // Pocz¹tkowo wy³¹czamy UI kratki z kluczem
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
        // Wy³¹cz obiekt klucza
        gameObject.SetActive(false);

        // Wy³¹cz obiekt UI pustej kratki
        emptyCrateUI.SetActive(false);

        // W³¹cz obiekt UI kratki z kluczem
        keyCrateUI.SetActive(true);

        isKeyCollected = true;

        // Odtwórz dŸwiêk zebrania klucza
        audioSource.PlayOneShot(collectSound);

        // Wywo³aj event informuj¹cy o zebraniu klucza
        KeyCollectedEvent?.Invoke();
    }
}
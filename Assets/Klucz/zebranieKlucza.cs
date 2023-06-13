using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebranieKlucza : MonoBehaviour
{
    public GameObject emptyCrateUI; // Referencja do UI pustej kratki
    public GameObject keyCrateUI; // Referencja do UI kratki z kluczem
    public AudioClip collectSound; // DŸwiêk zebrania klucza
    public GameObject doors; // Referencja do drzwi

    public bool IsKeyCollected { get; private set; } // W³aœciwoœæ umo¿liwiaj¹ca odczytanie wartoœci isKeyCollected

    private bool isPlayerInRange = false; // Flaga wskazuj¹ca, czy gracz jest w zasiêgu klucza
    private AudioSource audioSource; // Komponent AudioSource

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !IsKeyCollected)
        {
            CollectKey();
        }

        if (Input.GetKeyDown(KeyCode.Q) && IsKeyCollected)
        {
            OpenDoors();
        }
    }

    private void CollectKey()
    {
        // Wy³¹cz obiekt klucza
        gameObject.SetActive(false);

        // W³¹cz obiekt UI pustej kratki
        emptyCrateUI.SetActive(false);

        // W³¹cz obiekt UI kratki z kluczem
        keyCrateUI.SetActive(true);

        IsKeyCollected = true;

        // Odtwórz dŸwiêk zebrania klucza
        audioSource.PlayOneShot(collectSound);
    }

    private void OpenDoors()
    {
        // Otwórz drzwi
        doors.SetActive(false);
    }
}
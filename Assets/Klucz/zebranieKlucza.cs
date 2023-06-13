using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebranieKlucza : MonoBehaviour
{
    public GameObject emptyCrateUI; // Referencja do UI pustej kratki
    public GameObject keyCrateUI; // Referencja do UI kratki z kluczem
    public AudioClip collectSound; // D�wi�k zebrania klucza
    public GameObject doors; // Referencja do drzwi

    public bool IsKeyCollected { get; private set; } // W�a�ciwo�� umo�liwiaj�ca odczytanie warto�ci isKeyCollected

    private bool isPlayerInRange = false; // Flaga wskazuj�ca, czy gracz jest w zasi�gu klucza
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
        // Wy��cz obiekt klucza
        gameObject.SetActive(false);

        // W��cz obiekt UI pustej kratki
        emptyCrateUI.SetActive(false);

        // W��cz obiekt UI kratki z kluczem
        keyCrateUI.SetActive(true);

        IsKeyCollected = true;

        // Odtw�rz d�wi�k zebrania klucza
        audioSource.PlayOneShot(collectSound);
    }

    private void OpenDoors()
    {
        // Otw�rz drzwi
        doors.SetActive(false);
    }
}
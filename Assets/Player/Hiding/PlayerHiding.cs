using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public GameObject[] hidingObjects; // Tablica obiekt�w do ukrycia
    private bool isHiding = false; // Czy gracz jest ukryty?

    private Renderer[][] renderers; // Tablica tablic komponent�w Renderer na graczu i jego dzieciach
    private Component[][] components; // Tablica tablic komponent�w na graczu

    private void Start()
    {
        // Inicjalizacja tablic renderers i components
        renderers = new Renderer[hidingObjects.Length][];
        components = new Component[hidingObjects.Length][];

        for (int i = 0; i < hidingObjects.Length; i++)
        {
            // Pobierz wszystkie komponenty Renderer na obiekcie ukrywaj�cym i jego dzieciach
            renderers[i] = hidingObjects[i].GetComponentsInChildren<Renderer>();

            // Pobierz wszystkie komponenty na obiekcie ukrywaj�cym
            components[i] = hidingObjects[i].GetComponents<Component>();
        }
    }

    private void HidePlayer()
    {
        // Wy��cz renderowanie wszystkich komponent�w Renderer na obiektach ukrywaj�cych
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Renderer renderer in renderers[i])
            {
                renderer.enabled = false;
            }
        }

        // Wy��cz wszystkie komponenty na obiektach ukrywaj�cych opr�cz samego skryptu PlayerHiding
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Component component in components[i])
            {
                if (component != this)
                {
                    // Sprawd� typ komponentu i wy��cz odpowiednie w�a�ciwo�ci
                    if (component is Collider collider)
                    {
                        collider.enabled = false;
                    }
                    else if (component is Rigidbody rigidbody)
                    {
                        rigidbody.isKinematic = true;
                    }
                    else if (component is AudioSource audioSource)
                    {
                        audioSource.enabled = false;
                    }
                    // Dodaj inne typy komponent�w, je�li s� obecne w twoim projekcie
                }
            }
        }

        isHiding = true;
    }

    private void ShowPlayer()
    {
        // W��cz ponownie renderowanie wszystkich komponent�w Renderer na obiektach ukrywaj�cych
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Renderer renderer in renderers[i])
            {
                renderer.enabled = true;
            }
        }

        // W��cz ponownie wszystkie wy��czone komponenty na obiektach ukrywaj�cych
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Component component in components[i])
            {
                // Sprawd� typ komponentu i w��cz odpowiednie w�a�ciwo�ci
                if (component is Collider collider)
                {
                    collider.enabled = true;
                }
                else if (component is Rigidbody rigidbody)
                {
                    rigidbody.isKinematic = false;
                }
                else if (component is AudioSource audioSource)
                {
                    audioSource.enabled = true;
                }
                // Dodaj inne typy komponent�w, je�li s� obecne w twoim projekcie
            }
        }

        isHiding = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPlayerNearHidingObject())
            {
                if (isHiding)
                {
                    ShowPlayer();
                }
                else
                {
                    HidePlayer();
                }
            }
        }
    }

    private bool IsPlayerNearHidingObject()
    {
        float distanceThreshold = 2f; // Pr�g odleg�o�ci, poni�ej kt�rego gracz jest uwa�any za "w pobli�u"

        // Sprawd� odleg�o�� mi�dzy graczem a obiektami ukrywaj�cymi
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, hidingObjects[i].transform.position);

            // Je�li odleg�o�� jest mniejsza ni� pr�g, uznaj to za "w pobli�u"
            if (distance < distanceThreshold)
            {
                return true;
            }
        }

        // Je�li �aden obiekt ukrywaj�cy nie jest w pobli�u, uznaj to za "nie w pobli�u"
        return false;
    }
}
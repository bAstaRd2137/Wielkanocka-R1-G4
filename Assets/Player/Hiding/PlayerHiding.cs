using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public GameObject[] hidingObjects; // Tablica obiektów do ukrycia
    private bool isHiding = false; // Czy gracz jest ukryty?

    private Renderer[][] renderers; // Tablica tablic komponentów Renderer na graczu i jego dzieciach
    private Component[][] components; // Tablica tablic komponentów na graczu

    private void Start()
    {
        // Inicjalizacja tablic renderers i components
        renderers = new Renderer[hidingObjects.Length][];
        components = new Component[hidingObjects.Length][];

        for (int i = 0; i < hidingObjects.Length; i++)
        {
            // Pobierz wszystkie komponenty Renderer na obiekcie ukrywaj¹cym i jego dzieciach
            renderers[i] = hidingObjects[i].GetComponentsInChildren<Renderer>();

            // Pobierz wszystkie komponenty na obiekcie ukrywaj¹cym
            components[i] = hidingObjects[i].GetComponents<Component>();
        }
    }

    private void HidePlayer()
    {
        // Wy³¹cz renderowanie wszystkich komponentów Renderer na obiektach ukrywaj¹cych
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Renderer renderer in renderers[i])
            {
                renderer.enabled = false;
            }
        }

        // Wy³¹cz wszystkie komponenty na obiektach ukrywaj¹cych oprócz samego skryptu PlayerHiding
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Component component in components[i])
            {
                if (component != this)
                {
                    // SprawdŸ typ komponentu i wy³¹cz odpowiednie w³aœciwoœci
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
                    // Dodaj inne typy komponentów, jeœli s¹ obecne w twoim projekcie
                }
            }
        }

        isHiding = true;
    }

    private void ShowPlayer()
    {
        // W³¹cz ponownie renderowanie wszystkich komponentów Renderer na obiektach ukrywaj¹cych
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Renderer renderer in renderers[i])
            {
                renderer.enabled = true;
            }
        }

        // W³¹cz ponownie wszystkie wy³¹czone komponenty na obiektach ukrywaj¹cych
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            foreach (Component component in components[i])
            {
                // SprawdŸ typ komponentu i w³¹cz odpowiednie w³aœciwoœci
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
                // Dodaj inne typy komponentów, jeœli s¹ obecne w twoim projekcie
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
        float distanceThreshold = 2f; // Próg odleg³oœci, poni¿ej którego gracz jest uwa¿any za "w pobli¿u"

        // SprawdŸ odleg³oœæ miêdzy graczem a obiektami ukrywaj¹cymi
        for (int i = 0; i < hidingObjects.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, hidingObjects[i].transform.position);

            // Jeœli odleg³oœæ jest mniejsza ni¿ próg, uznaj to za "w pobli¿u"
            if (distance < distanceThreshold)
            {
                return true;
            }
        }

        // Jeœli ¿aden obiekt ukrywaj¹cy nie jest w pobli¿u, uznaj to za "nie w pobli¿u"
        return false;
    }
}
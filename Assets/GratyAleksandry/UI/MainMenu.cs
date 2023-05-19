using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene("GameLevel");
    }
    
    public void QuitGame ()
    {
        Debug.Log("Hasta la vista baby.");
        Application.Quit();
    }
}

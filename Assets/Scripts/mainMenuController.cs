using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CheckersScene"); // Replace with the exact name of your scene
    }

    // Method to quit the application
    public void ExitGame()
    {
        Application.Quit(); // Quits the application
    }
}

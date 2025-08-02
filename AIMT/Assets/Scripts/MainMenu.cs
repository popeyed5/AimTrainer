using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public void OpenGridshot() // Load the game scene
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gridshot");
    }
    public void OpenSettings() // Load the settings scene
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsMenu");
    }
    public void ReturnToMainMenu() // Load the main menu scene
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame() // Quit the application
    {
        Application.Quit();
        #if UNITY_EDITOR // If running in the Unity Editor, stop playing the game
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Method for Start button
    public void StartGame()
    {
        SceneManager.LoadScene("1-1"); // Replace "Page1-1" with the name of your next scene
    }

    // Method for Exit button
    public void ExitGame()
    {
        Application.Quit();
    }
}

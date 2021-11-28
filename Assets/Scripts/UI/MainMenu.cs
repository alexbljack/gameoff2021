using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Levels/Level_1");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}

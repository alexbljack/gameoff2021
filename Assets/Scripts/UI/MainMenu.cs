using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{   
    [SerializeField]
    private bool isDebugMod = false;

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Levels/Level_1");
    }

    public void ExitGame() 
    {
        if (isDebugMod)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }
        Application.Quit();
    }
}

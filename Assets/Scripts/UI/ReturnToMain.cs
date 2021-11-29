using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Scenes/Levels/MainMenu");
    }
}

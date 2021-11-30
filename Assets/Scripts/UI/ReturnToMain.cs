using UnityEngine.SceneManagement;
using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        foreach (var obj in FindObjectsOfType<AudioSource>())
        {
            Destroy(obj);
        }
        SceneManager.LoadScene("Scenes/Levels/MainMenu");
    }
}

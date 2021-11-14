using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.ExitLevel();
    }
}

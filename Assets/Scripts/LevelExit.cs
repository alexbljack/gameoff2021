using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>() != null)
        {
            StartCoroutine(GameManager.Instance.ExitLevel());
        }
    }
}

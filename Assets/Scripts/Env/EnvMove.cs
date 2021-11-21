using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnvMove : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            animator.SetTrigger("env_move");
    }
}

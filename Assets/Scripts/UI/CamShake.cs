using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CamShake : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shake()
    {
        animator?.SetTrigger("shake");
    }
}

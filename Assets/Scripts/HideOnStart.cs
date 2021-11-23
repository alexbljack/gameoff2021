using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HideOnStart : MonoBehaviour
{
    SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _renderer.enabled = false;
    }
}

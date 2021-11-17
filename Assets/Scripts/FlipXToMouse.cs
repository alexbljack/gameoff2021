using UnityEngine;

public class FlipXToMouse : MonoBehaviour
{
    SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _renderer.flipX = (transform.position.x - Utils.MousePosition().x) > 0;
    }
}
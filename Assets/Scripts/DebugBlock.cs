using UnityEngine;

public class DebugBlock : MonoBehaviour
{
    Entity _entity;

    void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    void OnEnable()
    {
        _entity.HitEvent += OnHit;
        _entity.DeadEvent += OnDie;
    }

    void OnDisable()
    {
        _entity.HitEvent -= OnHit;
        _entity.DeadEvent -= OnDie;
    }

    void OnHit()
    {
        GameManager.Instance.SwitchToDebugMode();
    }

    void OnDie()
    {
        Destroy(gameObject);
    }
}

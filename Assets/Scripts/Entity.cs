
using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] int health;
    
    public event Action<int, int> HpChangedEvent;
    public event Action HitEvent;
    public event Action DeadEvent;
    
    int _hp;

    public int Hp => _hp;

    void Start()
    {
        ChangeHp(health);
    }

    public void Heal(int amount)
    {
        ChangeHp(amount);
    }
    
    public void Damage(int amount)
    {
        HitEvent?.Invoke();
        ChangeHp(-amount);
        if (_hp <= 0) { DeadEvent?.Invoke(); }
    }
    
    void ChangeHp(int amount)
    {
        _hp += amount;
        _hp = Mathf.Clamp(_hp, 0, health);
        HpChangedEvent?.Invoke(_hp, health);
    }
}
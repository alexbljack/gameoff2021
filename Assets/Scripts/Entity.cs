
using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] int health;
    
    [Header("Sounds")]
    [SerializeField] AudioClip soundHit;
    [SerializeField] AudioClip soundDeath;
    
    public event Action<int, int> HpChangedEvent;
    public event Action HitEvent;
    public event Action DeadEvent;
    
    public float DeathClipLength => soundDeath != null ? soundDeath.length : 0;

    int _hp;
    AudioSource _audio;

    public int Hp => _hp;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

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
        ChangeHp(-amount);
        if (_hp <= 0)
        {
            PlayClip(soundDeath);
            DeadEvent?.Invoke();
            return;
        }
        PlayClip(soundHit);
        HitEvent?.Invoke();
    }
    
    void ChangeHp(int amount)
    {
        _hp += amount;
        _hp = Mathf.Clamp(_hp, 0, health);
        HpChangedEvent?.Invoke(_hp, health);
    }

    void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            _audio.clip = clip;
            _audio.Play();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIcon : MonoBehaviour
{
    Image _image;
    Entity _entity;
    
    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Init(Entity entity)
    {
        _entity = entity;
        _entity.DeadEvent += MarkDead;
    }

    void OnDisable()
    {
        _entity.DeadEvent -= MarkDead;
    }

    void MarkDead()
    {
        _image.color = Color.black;
    }
}

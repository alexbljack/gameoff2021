using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] LayerMask targetLayer;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (IsObjectInLayer(other.gameObject, targetLayer))
        {
            var entity = other.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                entity.Damage(damage);
            }
        }
    }

    bool IsObjectInLayer(GameObject obj, LayerMask layer)
    {
        return (layer.value & (1 << obj.layer)) > 0;
    }
}

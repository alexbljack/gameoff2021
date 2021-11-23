using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Damageable : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] List<LayerMask> targetLayers;

    void OnCollisionEnter2D(Collision2D other)
    {
        OnCollide(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnCollide(other.gameObject);
    }

    void OnCollide(GameObject obj)
    {
        if (IsObjectInLayer(obj, targetLayers))
        {
            var entity = obj.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                entity.Damage(damage);
            }
        }
    }

    bool IsObjectInLayer(GameObject obj, List<LayerMask> layers)
    {
        return layers.Any(layer => (layer.value & (1 << obj.layer)) > 0) ;
    }
}

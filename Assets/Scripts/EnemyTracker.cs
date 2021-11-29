using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTracker : MonoBehaviour
{
    [SerializeField] GameObject iconPrefab;
    
    void Start()
    {
        foreach (var enemy in GameManager.Instance.Enemies)
        {
            var icon = Instantiate(iconPrefab, transform.position, Quaternion.identity);
            icon.transform.SetParent(transform);
            icon.GetComponent<Image>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;
            icon.GetComponent<EnemyIcon>().Init(enemy.GetComponent<Entity>());
        }
    }
}

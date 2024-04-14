using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Value must be non-negative!");
        
        health -= damage;
        if (health <= 0)
            gameObject.SetActive(false);
        
    }
}

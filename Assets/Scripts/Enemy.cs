using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health { get; private set; }

    public float MaxHealth { get; private set; }

    [SerializeField] private float maxHealth;
    private void Awake()
    {
        Health = MaxHealth = maxHealth;
        EventBus.GameStart.AddListener(Repair);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Value must be non-negative!");

        Health -= damage;
        if (Health <= 0)
            Die();

    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    private void Repair()
    {
        Health = MaxHealth;
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Runner : MonoBehaviour
{
    private const string ObstacleTag = "Obstacle";
    private const string CoinTag = "Coin";
    private const string TerrainTag = "Terrain";
    private const string UpgradeTag = "Upgrate";
    private const string FinishTag = "Finish";

    private const int CoinFromOneCoins = 1;
    private const int DamageFromOneObstacle = 1;
    
    [SerializeField] private int maxHealth = 3;
    [SerializeField] protected float speedForward = 3;
    [SerializeField] protected float speedRotating = 50;
    
    [SerializeField] private int startAttackDamage;
    [SerializeField] private int startAttackSpeed;

    private int _health;

    protected float AttackDamage { get; private set; }
    public int _coins{ get; private set; }
    protected float AttackSpeed { get; private set; }

    public int _shipLevel { get; private set; }
    protected bool GamePaused;
    
    private void Start()
    {
        EventBus.InitHp?.Invoke(maxHealth);
        ClearPlayerData();
        
        EventBus.ClearPlayerData.AddListener(ClearPlayerData);
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hitInfo)
    {
        Debug.Log(hitInfo.gameObject.tag);
        switch (hitInfo.gameObject.tag)
        {
            case ObstacleTag:
                TakeDamage(CoinFromOneCoins);
                break;
            case CoinTag:
                TakeCoins(DamageFromOneObstacle);
                break;
            case TerrainTag:
                TakeDamage(_health);
                break;
            case FinishTag:
                EventBus.LevelСomplete?.Invoke();
                GamePaused = true;
                break;
            case UpgradeTag:
                Upgrade();
                break;
        }
        hitInfo.gameObject.SetActive(false);
    }

    private void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Value must be non-negative!");
        
        _health -= damage;
        EventBus.SetHp?.Invoke(_health);
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        EventBus.GameOver?.Invoke();
        GamePaused = true;
    }

    private void TakeCoins(int coins)
    {
        if (coins <= 0)
            throw new ArgumentOutOfRangeException(nameof(coins), "Value must be non-negative!");
        
        _coins += coins;
        EventBus.SetCoins?.Invoke(_coins);
    }
    
    private void ClearPlayerData()
    {
        _coins = 0;
        _health = maxHealth;
        AttackDamage = startAttackDamage;
        AttackSpeed = startAttackSpeed;
        _shipLevel = 0;
        
        EventBus.SetCoins?.Invoke(_coins);
        EventBus.SetHp?.Invoke(_health);
    }

    private void Upgrade()
    {
        if (transform.position.x < 0)
        {
            AttackDamage += _coins;
        }
        else
        {
            AttackSpeed += _coins;
        }
    }

    public void SpendCoins(int coins)
    {
        if (coins <= 0)
            throw new ArgumentOutOfRangeException(nameof(coins), "Value must be non-negative!");
        
        _coins -= coins;
        EventBus.SetCoins?.Invoke(_coins);
    }

    public void ShipLevelUp()
    {
        _shipLevel++;
    }
}

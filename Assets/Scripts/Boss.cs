using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BossState
{
    RotationToMuve,
    RotationToShoting,
    Muve,
    Shoting,
}
public class Boss : EnemyCannon
{
    [SerializeField] private Transform player;
    
    [SerializeField] private float speed;
    [SerializeField] private Transform[] bulletSpawnPoints;
    [SerializeField] private float speedForward;
    
    private float offset;
    private int SalvoCount;
    
    private Vector3 targetPosition;

    private BossState _bossStatus = BossState.RotationToShoting;
    
    protected override void Update()
    {
        transform.position += speedForward * Time.deltaTime * Vector3.forward;
        
        switch (_bossStatus)
        {
            case BossState.RotationToMuve:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0,0,0)), Time.deltaTime * speed);
                if (transform.rotation.y < 0.1)
                {
                    targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(20, 70));
                    _bossStatus = BossState.Muve;
                }
                break;
            case BossState.Muve:
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
                if (Vector3.Distance(transform.position, targetPosition) < 0.1)
                {
                    _bossStatus = BossState.RotationToShoting;
                }
                break;
            case BossState.RotationToShoting:
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0,90,0)), Time.deltaTime * speed);
                if (Math.Abs(transform.rotation.eulerAngles.y - 90) < 1f)
                {
                    StartCoroutine(WaitPlayer(BossState.Shoting));
                }
                break;
            case BossState.Shoting:
                base.Update();
                break;
        }
    }
    
    protected override void CreateBullet()
    {
        if (SalvoCount > 5 && offset == 0)
        {
            offset = 1;
        }

        if (SalvoCount > 10)
        {
            _bossStatus = BossState.RotationToMuve;
        }
        foreach (var spawnPoint in bulletSpawnPoints)
        {
            var newSpawnPosition = new Vector3(spawnPoint.position.x + offset, spawnPoint.position.y, spawnPoint.position.z);
            var bullet = Instantiate(bulletPrefab,newSpawnPosition , spawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpeed * new Vector3(0,0,-1), ForceMode.Impulse);
        }
        SalvoCount++;
    }

    protected override void Die()
    {
        EventBus.LevelСomplete.Invoke();
        base.Die();
    }

    private IEnumerator WaitPlayer(BossState state)
    {
        float distance = Vector3.Distance(transform.position, player.position);

        while (distance > 60)
        {
            yield return new WaitForSeconds(1);

            distance = Vector3.Distance(transform.position, player.position);

            if (distance <= 60)
            {
                break;
            }
        }
        
        _bossStatus = state;
    }
}

using System.Collections;
using UnityEngine;

public class EnemyCannon : Enemy
{
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Transform bulletPrefab;
    [SerializeField] protected float bulletSpeed = 10;
    [SerializeField] private float attackSpeed;

    private float nextShootTime;
    private void Start()
    {
        nextShootTime = Time.time;
    }

    protected virtual void Update()
    {
        if (Time.time >= nextShootTime) StartCoroutine(Shoot());
    }
    
    private IEnumerator Shoot()
    {
        nextShootTime = Time.time + 1 / attackSpeed;

        CreateBullet();

        yield return new WaitForSeconds(1 / attackSpeed);
    }

    protected virtual void CreateBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpeed * new Vector3(0,0,-1), ForceMode.Impulse);
    }
}

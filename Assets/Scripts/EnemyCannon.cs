using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyCannon : Enemy
{
    [SerializeField]private Transform bulletSpawnPoint;
    [SerializeField]private Transform bulletPrefab;
    [SerializeField]private float bulletSpeed = 10;
    [FormerlySerializedAs("_attackSpeed")] [SerializeField] private float attackSpeed;

    private float nextShootTime;
    private void Start()
    {
        nextShootTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= nextShootTime) StartCoroutine(Shoot());
    }
    
    private IEnumerator Shoot()
    {
        nextShootTime = Time.time + 1 / attackSpeed;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpeed * new Vector3(0,0,-1), ForceMode.Impulse);

        yield return new WaitForSeconds(1 / attackSpeed);
    }
}

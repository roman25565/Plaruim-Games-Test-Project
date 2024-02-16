using UnityEngine;

public class BulletPlayer : Bullet
{
    [HideInInspector] public float bulletDamage;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            var player = other.gameObject.GetComponent<Enemy>();
            player.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
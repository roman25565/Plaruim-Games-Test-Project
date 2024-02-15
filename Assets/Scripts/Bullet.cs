using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }
}
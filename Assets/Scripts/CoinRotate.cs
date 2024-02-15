using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField] private int _rotateSpeed = 2;

    private void Update()
    {
        transform.Rotate(0, _rotateSpeed, 0, Space.World);
    }
}
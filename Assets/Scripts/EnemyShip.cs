using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private float lineRadius = 7;
    
    [SerializeField] private Vector3 _targetPoz;
    private Vector3 _startPoz;
    private void Start()
    {
        _startPoz = transform.position;
        CreateTargetPoz(lineRadius);
    }

    private void Update()
    {
        var directMove = (_targetPoz - transform.position).normalized;
        transform.position += Time.deltaTime * speed * directMove;
        
        if (Vector3.Distance(transform.position, _targetPoz) < 0.1)
        {
            var x = Mathf.Abs(_targetPoz.x - lineRadius) < 0.1f ? -lineRadius : lineRadius;
            CreateTargetPoz(x);
        }
    }

    private void CreateTargetPoz(float x)
    {
        _targetPoz = new Vector3(x, _startPoz.y, _startPoz.z);
    }
}


using System.Collections;
using UnityEngine;

public class Ship : Runner
{
    [SerializeField]private Mesh[] shipMeshes;
    [SerializeField]private MeshFilter playerShipMesh;
    
    [SerializeField]private Transform bulletSpawnPoint;
    [SerializeField]private Transform bulletPrefab;
    [SerializeField]private float bulletSpeed = 10;
    
    [SerializeField]private float rotationSensitivity = 30.0f;
    [SerializeField]private float maxRotationAngle = 45.0f;
    
    private Vector2 _lastTouch;
    private Vector2 _touchStart;
    private Quaternion _targetRotation;
    
    private Vector3 _startPoz;
    private CharacterController _characterController;
    private float nextShootTime;
    

    private void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
        EventBus.GameStart.AddListener(GameStart);
        EventBus.ClearPlayerData.AddListener(() => SetMeshLvl(0));
        
        _startPoz = transform.position;
        nextShootTime = Time.time;
    }

    private void Update()
    {
        if(GamePaused) return;
        var rotatingDirection = GetRotatingDirection();
        Move(rotatingDirection);
        if (NeedSoot() && Time.time >= nextShootTime) StartCoroutine(Shoot());
    }
    
    
    private float GetRotatingDirection()
    {
        var result = 0f;
        var targetRotationAngle = transform.rotation.y - rotationSensitivity / 100;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                _touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                
                var touchDelta = touch.position - _touchStart;
                float normalizedDeltaX = touchDelta.x / Screen.width;
                targetRotationAngle = normalizedDeltaX * rotationSensitivity;
                
                result = touch.position.x - _lastTouch.x == 0 ? 0 : touch.position.x - _lastTouch.x > 0 ? 1 : -1;
                _lastTouch = touch.position;
            }
        }
        _targetRotation = Quaternion.Euler(0.0f,
            Mathf.Clamp(targetRotationAngle, -maxRotationAngle, maxRotationAngle), 0.0f);
        return result;
    }

    private void Move(float rotatingDirection)
    {
        _characterController.Move(Time.deltaTime * speedForward * Vector3.forward);
        _characterController.Move(Time.deltaTime * speedRotating * new Vector3(rotatingDirection, 0, 0));

        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, rotationSensitivity * Time.deltaTime);

        Debug.Log(!_characterController.isGrounded);
        if (!_characterController.isGrounded)
        {
            transform.position += Vector3.down * (4 * Time.deltaTime);
        }
    }

    private bool NeedSoot()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                    return true;
            }
        }
        return false;
    }
    
    private IEnumerator Shoot()
    {
        nextShootTime = Time.time + 1 / AttackSpeed;

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpeed * speedForward * transform.forward, ForceMode.Impulse);
        bullet.GetComponent<BulletPlayer>().bulletDamage = AttackDamage;

        yield return new WaitForSeconds(1 / AttackSpeed);
    }

    protected override void SetMeshLvl(int shipLevel)
    {
        playerShipMesh.mesh = shipMeshes[shipLevel];
    }
    
    private void GameStart()
    {
        gameObject.transform.position = _startPoz;
        StartCoroutine(DelayResumeGame());
    }
    
    private IEnumerator DelayResumeGame()
    {
        yield return new WaitForSeconds(0.2f);
        GamePaused = false;
    }
}
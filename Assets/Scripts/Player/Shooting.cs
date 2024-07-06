using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Setup Force")]
    [SerializeField, Range(1, 1000)] private float _rayDistance;

    [Header("Pool Setup")]
    [SerializeField, Range(0f, 10f)] private float _spawnOffsetY;
    [SerializeField] private int _bulletCount;
    [SerializeField] private bool _autoExpand;
    [SerializeField] private Transform _containerBullet;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnBullet;

    private PlayeMovement _playerMove;

    private Camera _camera;
    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _camera = Camera.main;
        _playerMove = GetComponent<PlayeMovement>();
    }

    private void Start()
    {
        _pool = new ObjectPool<Bullet>(_bulletPrefab.GetComponent<Bullet>(), _containerBullet, _bulletCount);
        _pool.autoExpand = _autoExpand;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _playerMove.IsMoving == false)
        {
            Bullet bullet = CreateBullet();
            
            bullet.Launch(GetEndPoint());
        }
    }

    private Vector3 GetEndPoint()
    {
        Vector3 endPoint;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_camera.transform.position, ray.direction, out RaycastHit hit))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = ray.GetPoint(_rayDistance);
        }
        return endPoint;
    }
    private Bullet CreateBullet()
    {
        var bullet = _pool.GetFreeElement();
        bullet.transform.position = _spawnBullet.position;
        return bullet;
    }
}

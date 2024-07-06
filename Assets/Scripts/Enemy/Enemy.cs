using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyView))]
[RequireComponent(typeof(RaagdollDeath))]
public class Enemy : MonoBehaviour, IDamagable
{
    private const string KEY_CONTAINER_NAME = "DeathEnemy";
    
    [SerializeField] private int _health;
    [SerializeField] private Slider _healthBar;
    
    private EnemyView _view;
    private RaagdollDeath _raagdollDeath;
    
    private Transform _containerDeathEnemy;

    private void Awake()
    {
        InitializeComponents();
        _view.Initialize();
        _raagdollDeath.Initialize();
        _containerDeathEnemy = GameObject.Find(KEY_CONTAINER_NAME).transform;
        _healthBar.maxValue = _health;
        _healthBar.value = _health;
    }

    private void Kill(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdollBehaviour();
        _raagdollDeath.Hit(force, hitPoint);
        transform.parent = _containerDeathEnemy;
        _healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(Vector3 force, Vector3 hitPoint, int damage)
    {
         _health -= damage;
         _healthBar.value = _health;
         if (_health <= 0) Kill(force, hitPoint);
    }

    private void EnableRagdollBehaviour()
    {
        _view.DisableAnimator();
        _raagdollDeath.Enable();
    }

    private void InitializeComponents()
    {
        _view = GetComponent<EnemyView>();
        _raagdollDeath = GetComponent<RaagdollDeath>();
    }
}

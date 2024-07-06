using UnityEngine;

public interface IDamagable
{
    void TakeDamage(Vector3 force, Vector3 hitPoint, int damage);
}

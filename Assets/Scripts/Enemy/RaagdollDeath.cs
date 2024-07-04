using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaagdollDeath : MonoBehaviour
{
    private List<Rigidbody> _rigidbodies;

    public void Initialize()
    {
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        Disable();
    }

    public void Hit(Vector3 force, Vector3 hitPosition)
    {
        Rigidbody injuredRigidbody = _rigidbodies.OrderBy(rigidbody =>
                Vector3.Distance(rigidbody.position, hitPosition)).First();
        injuredRigidbody.AddForceAtPosition(force, hitPosition,ForceMode.Impulse);
    }

    public void Enable()
    {
        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;
    }

    private void Disable()
    {
        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = true;
    }
}

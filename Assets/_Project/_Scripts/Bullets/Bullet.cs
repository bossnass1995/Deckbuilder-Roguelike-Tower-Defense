using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float bossKillingMultiplier = 1.0f;

    protected Transform _target = null;

    public void TargetEnemy(Transform _transform) {
        _target = _transform;
    }
}
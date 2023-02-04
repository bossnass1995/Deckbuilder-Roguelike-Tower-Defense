using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DestroyOnClick : MonoBehaviour
{
    [FormerlySerializedAs("deathParticles")] [SerializeField] private ParticleSystem _deathParticles;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            OnDeath();
        }
    }

    void OnDeath()
    {
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        SplatController.instance.MakeSplat(gameObject.transform);
    }
}

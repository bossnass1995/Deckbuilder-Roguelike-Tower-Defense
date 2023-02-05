using UnityEngine;
using UnityEngine.Serialization;

public class DestroyOnClick : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _deathParticles;
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
        //instantiate particles
        foreach (var particle in _deathParticles)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
        
        //leave decal
        DecalController decalController = GetComponent<DecalController>();
        decalController.MakeDecal(transform);
    }
}

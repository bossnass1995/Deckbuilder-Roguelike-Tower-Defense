using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DecalController : MonoBehaviour
{
    public GameObject[] decals;

    public void MakeDecal(Transform deathSpot)
    {
        GameObject decal = decals[Random.Range(0, decals.Length)];
        Vector3 randomRotation = new Vector3(0, 0, Random.Range(0, 360));
        Instantiate(decal, deathSpot.position, Quaternion.Euler(randomRotation));
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SplatController : MonoBehaviour
{
    public static SplatController instance;

    public GameObject[] splats;

    private void Awake()
    {
        instance = this;
    }

    public void MakeSplat(Transform deathSpot)
    {
        Instantiate(splats[Random.Range(0, splats.Length)], deathSpot.position, transform.rotation);
    }
}

﻿using UnityEngine;
using System.Collections;

public class DestroyAfterDelay : MonoBehaviour {

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update () {
	
	}
}

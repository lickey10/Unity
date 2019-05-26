using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour
{
    float flickerMin		= 0.1f;
    float flickerMax		= 0.3f;
    float flickerCount		= 0.3f;
    float intensityMin		= 0.9f;
    float intensityMax		= 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        flickerCount = Mathf.Clamp(flickerCount - Time.deltaTime, 0, flickerMax);
        if (flickerCount == 0)
        {
            var newIntensity = Random.Range(intensityMin, intensityMax);
            GetComponent<Light>().intensity = newIntensity;
            var newTime = Random.Range(flickerMin, flickerMax);
            flickerCount = newTime;
        }
    }
}

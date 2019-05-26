using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleTrailRotation : MonoBehaviour
{
    Vector3 rotationSpeed;
    bool local	= true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (local)
            transform.Rotate(rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }
}

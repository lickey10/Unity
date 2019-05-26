using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleMoveOverTime : MonoBehaviour
{
    Vector3 movement;
    bool local	= true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (local)
            transform.localPosition += movement * Time.deltaTime;
        else
            transform.position += movement * Time.deltaTime;
    }
}

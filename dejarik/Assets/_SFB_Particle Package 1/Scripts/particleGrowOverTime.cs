using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleGrowOverTime : MonoBehaviour
{
    Vector3 growth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += growth * Time.deltaTime;
    }
}

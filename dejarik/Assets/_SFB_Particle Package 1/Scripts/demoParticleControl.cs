using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoParticleControl : MonoBehaviour
{
    public Vector3 startPosition		= new Vector3(0,0.1f,0);     // Where should this particle start?
    public Vector3 startRotation		= new Vector3(0,0,0);       // Rotation should this particle start?
    public bool shootsTarget		= false;                // If true, particle will shoot forward
    float shootSpeed			= 12.0f;					// Multiplier of Time.deltaTime


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootsTarget)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + shootSpeed * Time.deltaTime);
            //transform.position.z += shootSpeed * Time.deltaTime;
    }
}
